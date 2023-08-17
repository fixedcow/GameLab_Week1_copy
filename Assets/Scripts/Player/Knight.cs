using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Character
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Animator counterAnim;
	#endregion

	#region PublicMethod
	public void PrintCounterSuccess()
	{
		counterAnim.Play("sizeUpWithFadeOut");
	}
	public override void Command1()
	{
		if (canAct == false)
			return;
		anim.SetTrigger("command1");
	}
	public override void Command2()
	{
		if (canAct == false)
			return;
		canAct = false;
		anim.SetTrigger("command2");
	}
	public override void Command3()
	{
		if (canAct == false)
			return;
		canAct = false;
		anim.SetTrigger("command3");
	}
	#endregion

	#region PrivateMethod
	#endregion
}
