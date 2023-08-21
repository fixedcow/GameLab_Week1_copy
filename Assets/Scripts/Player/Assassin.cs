using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Character
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private ParticleSystem dashParticle;
	private bool canDash = true;
	[SerializeField] private float dashDistance;
	[SerializeField] private float dashDuration;
	[SerializeField] private float dashCooldown;
	#endregion

	#region PublicMethod
	public override void Command1()
	{
		if (canAct == false || canAttack == false)
			return;
		anim.SetTrigger("command1");
	}
	public override void Command2()
	{
		if (canAct == false || canAttack == false)
			return;
		canAct = false;
		anim.SetTrigger("command2");
	}
	public override void Command3()
	{
		if (canAct == false || canAttack == false || canDash == false)
			return;
		canAct = false;
		anim.SetTrigger("command3");
		Dash();
	}
	#endregion

	#region PrivateMethod
	private void Dash()
	{
		canDash = false;
		dashParticle.Play();
		float dirMult = transform.localScale.x;
		Vector2 origin = (Vector2)transform.position - Vector2.down * 0.1f;
		Vector2 direction = Vector2.right * dirMult;
		RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, dashDistance
			, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Character"));
		Debug.DrawRay(origin, direction * dashDistance, Color.red);
		foreach (RaycastHit2D h in hits)
		{
			Debug.Log(h.collider.gameObject);
		}
		if(hits.Length > 1)
		{
			transform.position = hits[1].point - Vector2.right * dirMult;
		}
		else
		{
			transform.position = (Vector2)transform.position + dirMult * Vector2.right * dashDistance;
		}

		//TODO!!!!!!!!!!!!!!! 대쉬 만들기
		//rb.bodyType = RigidbodyType2D.Kinematic;
		//rb.velocity = transform.localScale.x * Vector2.right * dashPower;
		Invoke(nameof(DashFinish), dashDuration);
		Invoke(nameof(DashCooldown), dashCooldown);
	}
	private void DashFinish()
	{
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.velocity = Vector2.zero;
		dashParticle.Stop();
	}
	private void DashCooldown()
	{
		canDash = true;
	}
	#endregion
}
