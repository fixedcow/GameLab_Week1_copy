using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectTargetStage : UISelectTarget
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public override void HighlightOff()
	{
		gameObject.SetActive(false);
	}

	public override void HighlightOn()
	{
		gameObject.SetActive(true);
	}

	public override void LockIn()
	{

	}

	public override void LockOut()
	{

	}
	#endregion

	#region PrivateMethod
	#endregion
}
