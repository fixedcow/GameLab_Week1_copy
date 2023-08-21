using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private PlayerManager owner;
	protected Rigidbody2D rb;
	protected Animator anim;
	[SerializeField] protected ParticleSystem dustTrail;
	[SerializeField] private List<SpriteRenderer> mainColorPart = new List<SpriteRenderer>();
	[SerializeField] private List<SpriteRenderer> subColorPart = new List<SpriteRenderer>();

	[SerializeField] private const float respawnInvicibleTime = 0.6f;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float jumpForce;
	protected bool canMove = true;
	protected bool canJump = true;
	protected bool canAttack = true;
	protected bool canAct = true;
	private bool tryToFall = false;
	private bool isOnAir = false;
	#endregion

	#region PublicMethod
	public void SetOwner(PlayerManager _owner) => owner = _owner;
	public void SetColor(Color32 _main, Color32 _sub)
	{
		foreach(SpriteRenderer sr in mainColorPart)
		{
			sr.color = _main;
		}
		foreach(SpriteRenderer sr in subColorPart)
		{
			sr.color = _sub;
		}
	}
	public bool IsAnimationStateName(string _str)
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		return info.IsName(_str);
	}
	public void SetCanAct()
	{
		canAct = true;
	}
	public void Invincible()
	{
		SetLayerDefault();
		Invoke(nameof(SetLayerCharacter), respawnInvicibleTime);
	}
	public void Move(int _direction)
	{
		if (canMove == false || canAct == false)
			return;
		RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.6f, 1 << LayerMask.NameToLayer("Wall"));
		RaycastHit2D groundR = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.6f, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position, Vector2.right * 0.7f * transform.localScale.x, Color.red);
		if(wall.collider == null && groundR.collider == null)
			transform.Translate(_direction * moveSpeed * Vector2.right * Time.deltaTime);
		transform.localScale = new Vector3(_direction, 1, 1);
	}
	public void Fall()
	{
		tryToFall = true;
	}
	public void Jump()
	{
		if (canJump == false || canAct == false)
			return;

		canJump = false;
		rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
	}
	public virtual void Hit(AttackData from, Vector2 _direction, float _magnitude)
	{
		if(from.GetAttackType() == AttackData.EType.smash)
		{
			CameraController.instance.SmashShake();
			EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.smash, transform.position, Vector2.zero);
		}
		else
		{
			CameraController.instance.AttackShake();
			EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.attack, transform.position, Vector2.zero);
		}
		canAct = false;
		anim.SetTrigger("hit");
		rb.velocity = _magnitude * _direction;
		anim.ResetTrigger("command1");
		anim.ResetTrigger("command2");
		anim.ResetTrigger("command3");
	}
	public void Dead()
	{
		CameraController.instance.DeadShake();
		Vector2 direction = (CameraController.instance.transform.position - transform.position).normalized;
		if(owner == GameManager.instance.player1)
			EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.player1Dead, transform.position, direction);
		else
			EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.player2Dead, transform.position, direction);
		rb.velocity = Vector2.zero;
		owner.PlayerDead();
	}
	public abstract void Command1();
	public abstract void Command2();
	public abstract void Command3();
	public void Bounce(Vector2 direction, float magnitude)
	{
		rb.velocity = direction * magnitude;
	}
	#endregion

	#region PrivateMethod
	protected virtual void Awake()
	{
		TryGetComponent(out rb);
		transform.Find("Body").TryGetComponent(out anim);
	}
	protected virtual void Update()
	{
		CheckPhysics();
	}
	private void CheckPhysics()
	{
		if (rb.velocity.y > 0)
		{
			anim.SetBool("hang", false);
			return;
		}

		bool isGround = false;
		bool isHang = false;

		isGround = CheckGround();
		isHang = CheckCliff();

		ActByPhysics(isGround, isHang);
	}
	private bool CheckGround()
	{
		RaycastHit2D hitGround = Physics2D.Raycast(transform.position - transform.localScale.x * new Vector3(0.15f, 0)
			, Vector2.down, 0.7f, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position - transform.localScale.x * new Vector3(0.15f, 0), Vector2.down * 0.7f, Color.red);

		if (hitGround.collider != null)
		{
			tryToFall = false;
			isOnAir = false;

			if (hitGround.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
			{
				canJump = true;
				if(dustTrail.isEmitting == false)
				{
					dustTrail.Play();
					Debug.Log(dustTrail.isEmitting);
				}
				ResetYVelocityWhileOnGround();
			}

			Platform p;
			if (hitGround.collider.TryGetComponent(out p))
			{
				p.Touched();
			}
			return true;
		}
		else
		{
			if(isOnAir == false)
			{
				isOnAir = true;
				if (dustTrail.isEmitting == true)
				{
					dustTrail.Stop();
					Debug.Log(dustTrail.isEmitting);
				}
				Invoke(nameof(CantJumpForInvoke), 0.1f);
			}
			return false;
		}
	}
	private void ResetYVelocityWhileOnGround()
	{
		if (rb.velocity.y < 0)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}
	}
	private bool CheckCliff()
	{
		if (tryToFall == false)
		{
			RaycastHit2D hitCliff = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.7f, 1 << LayerMask.NameToLayer("Ground"));
			Debug.DrawRay(transform.position, Vector2.right * 0.7f * transform.localScale.x, Color.red);

			if (hitCliff.collider != null)
			{
				Platform p;
				if (hitCliff.collider.TryGetComponent(out p))
				{
					p.Touched();
				}
				return true;
			}
		}
		return false;
	}
	private void ActByPhysics(bool _isGround, bool _isHang)
	{
		if (_isGround == false && _isHang == true)
		{
			anim.SetBool("hang", true);
			anim.ResetTrigger("command1");
			anim.ResetTrigger("command2");
			anim.ResetTrigger("command3");
			if (IsAnimationStateName("Hang"))
			{
				canJump = true;
				rb.velocity = new Vector2(0, Mathf.Clamp(rb.velocity.y, 0, float.MaxValue));
			}
		}
		else
		{
			anim.SetBool("hang", false);
		}
	}
	private void SetLayerDefault()
	{
		gameObject.layer = LayerMask.NameToLayer("Default");
	}
	private void SetLayerCharacter()
	{
		gameObject.layer = LayerMask.NameToLayer("Character");
	}
	private void CantJumpForInvoke()
	{
		canJump = false;
	}
	#endregion
}
