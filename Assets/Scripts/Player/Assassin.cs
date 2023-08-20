using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Character
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
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
		anim.SetTrigger("command2");
	}
	public override void Command3()
	{
		if (canAct == false || canAttack == false)
			return;
		anim.SetTrigger("command3");
	}
	#endregion

	#region PrivateMethod
	#endregion
}
