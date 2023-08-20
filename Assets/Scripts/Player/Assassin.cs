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
	[SerializeField] private float dashPower;
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
		transform.position += Vector3.right * 3;
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
