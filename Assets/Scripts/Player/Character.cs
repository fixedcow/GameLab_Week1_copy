using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	protected Rigidbody2D rb;
	protected Animator anim;
	[SerializeField] protected ParticleSystem dustTrail;
	[SerializeField] private List<SpriteRenderer> mainColorPart = new List<SpriteRenderer>();
	[SerializeField] private List<SpriteRenderer> subColorPart = new List<SpriteRenderer>();

	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float jumpForce;
	protected bool canMove = true;
	protected bool canJump = true;
	protected bool canAttack = true;
	protected bool canAct = true;
	#endregion

	#region PublicMethod
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
	public void Jump()
	{
		if (canJump == false || canAct == false)
			return;

		canJump = false;
		dustTrail.Play();
		rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
	}
	public virtual void Hit(Vector2 _direction, float _magnitude)
	{
		canAct = false;
		anim.SetTrigger("hit");
		rb.velocity = _magnitude * _direction;
		anim.ResetTrigger("command1");
		anim.ResetTrigger("command2");
		anim.ResetTrigger("command3");
	}
	public void Dead()
	{
		rb.velocity = Vector2.zero;
		CharacterSpawner.instance.Respawn(this);
	}
	public abstract void Command1();
	public abstract void Command2();
	public abstract void Command3();
	#endregion

	#region PrivateMethod
	protected virtual void Awake()
	{
		TryGetComponent(out rb);
		transform.Find("Body").TryGetComponent(out anim);
	}
	protected virtual void Update()
	{
		CheckGround();
	}
	private void CheckGround()
	{
		if (rb.velocity.y > 0)
			return;

		bool isGround = false;
		bool isHang = false;

		RaycastHit2D hitGround = Physics2D.Raycast(transform.position - transform.localScale.x * new Vector3(0.15f, 0), Vector2.down, 0.7f, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position - transform.localScale.x * new Vector3(0.15f, 0), Vector2.down * 0.7f, Color.red);

		if (hitGround.collider != null)
		{
			isGround = true;
			if (hitGround.collider.gameObject.layer == LayerMask.NameToLayer("Slime"))
			{
				canJump = false;
			}
			else if (hitGround.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
			{
				canJump = true;
			}
		}
		else
		{
			Invoke("CantJumpForInvoke", 0.1f);
			isGround = false;
		}

		RaycastHit2D hitCliff = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.85f, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position, Vector2.right * 0.8f * transform.localScale.x, Color.red);

		if(hitCliff.collider != null)
		{
			isHang = true;
		}
		else
		{
			isHang = false;
		}

		if(isGround == false && isHang == true)
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
	private void CantJumpForInvoke()
	{
		canJump = false;
	}
	#endregion
}
