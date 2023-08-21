using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISelectTarget : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] protected GameObject data;
	#endregion

	#region PublicMethod
	public GameObject GetResult() => data;
	public abstract void HighlightOn();
	public abstract void HighlightOff();
	public abstract void LockIn();
	public abstract void LockOut();
	#endregion

	#region PrivateMethod
	#endregion
}
