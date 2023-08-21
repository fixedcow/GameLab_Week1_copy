using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Assassin : Character
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private ParticleSystem dashParticle;
	[SerializeField] private float dashDistance;
	[SerializeField] private float dashCooldown;

	private bool canDash = true;

	private const float DASH_DURATION = 0.1f;
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
		Vector2 destination = transform.position;
		RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, dashDistance
			, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Character"));
		Debug.DrawRay(origin, direction * dashDistance, Color.red);
		if(hits.Length > 1)
		{
			destination = hits[1].point - Vector2.right * dirMult;
		}
		else
		{
			destination = (Vector2)transform.position + dirMult * Vector2.right * dashDistance;
		}
		rb.bodyType = RigidbodyType2D.Kinematic;
		rb.velocity = Vector2.zero; // transform.localScale.x * Vector2.right;
		StartCoroutine(nameof(IE_DashFinish), destination);
		Invoke(nameof(DashCooldown), dashCooldown);
	}
	private IEnumerator IE_DashFinish(Vector2 _destination)
	{
		yield return new WaitForSeconds(DASH_DURATION);
		transform.position = _destination;
		rb.bodyType = RigidbodyType2D.Dynamic;
		yield return new WaitForSeconds(DASH_DURATION);
		dashParticle.Stop();
	}
	private void SetParticleDeactive()
	{
		dashParticle.Stop();
	}
	private void DashCooldown()
	{
		canDash = true;
	}
	#endregion
}
