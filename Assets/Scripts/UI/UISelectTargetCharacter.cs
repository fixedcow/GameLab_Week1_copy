using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectTargetCharacter : UISelectTarget
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Animator anim;
	[SerializeField] private int dirMult = 1;
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
		gameObject.transform.localScale = new Vector3(dirMult * 1.2f, 1.2f, 1);
		anim.enabled = false;
	}

	public override void LockOut()
	{
		gameObject.transform.localScale = new Vector3(dirMult, 1, 1);
		anim.enabled = true;
	}
	#endregion

	#region PrivateMethod
	#endregion

}
