using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public abstract void Initialize();
	public abstract void Touched();
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		Initialize();
	}
	#endregion
}
