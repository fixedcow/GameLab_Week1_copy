using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Knight : Character
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Animator counterAnim;
	[SerializeField] private KnightCounter counterData;
	#endregion

	#region PublicMethod
	public void PrintCounterSuccess()
	{
		counterAnim.Play("sizeUpWithFadeOut");
	}
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
		if (canAct == false || canAttack == false)
			return;
		canAct = false;
		anim.SetTrigger("command3");
	}
	public override void Hit(AttackData from, Vector2 _direction, float _magnitude)
	{
		if(IsAnimationStateName("Command3") == true && from.GetAttackType() == AttackData.EType.attack)
		{
			PrintCounterSuccess();
			counterData.CounterAttack(from.GetSource());
		}
		else
			base.Hit(from, _direction, _magnitude);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
