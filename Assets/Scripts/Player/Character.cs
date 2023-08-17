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

	[SerializeField] private float moveSpeed;
	[SerializeField] private float jumpForce;
	protected bool canMove = true;
	protected bool canJump = true;
	protected bool canAct = true;
	#endregion

	#region PublicMethod
	public bool IsAnimationStateName(string str)
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		return info.IsName(str);
	}
	public void SetCanAct()
	{
		canAct = true;
	}
	public void Move(int _direction)
	{
		if (canMove == false || canAct == false)
			return;

		transform.Translate(_direction * moveSpeed * Vector2.right * Time.deltaTime);
		transform.localScale = new Vector3(_direction, 1, 1);
	}
	public void Jump()
	{
		if (canJump == false || canAct == false)
			return;

		canJump = false;
		rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
	}
	public virtual void Hit(Vector2 direction, float magnitude)
	{
		canAct = false;
		anim.SetTrigger("hit");
		rb.velocity = magnitude * direction;
		anim.ResetTrigger("command1");
		anim.ResetTrigger("command2");
		anim.ResetTrigger("command3");
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

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.red);

		if (hit.collider == null)
			return;

		if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Slime"))
		{
			canJump = false;
		}
		else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			canJump = true;
		}
	}
	#endregion
}
