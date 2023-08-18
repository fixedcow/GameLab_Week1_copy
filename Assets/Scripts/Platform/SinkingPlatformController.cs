using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPlatformController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void Start()
	{
		CameraController.instance.EarthquakeShake();
	}
	#endregion
}
