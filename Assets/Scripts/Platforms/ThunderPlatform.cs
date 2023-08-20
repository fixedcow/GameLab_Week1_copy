using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPlatform : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Thunder thunder;
	#endregion

	#region PublicMethod
	public void Initialize()
	{

	}
	public void Thunder()
	{
		thunder.HitGround();
	}
	#endregion

	#region PrivateMethod
	#endregion
}
