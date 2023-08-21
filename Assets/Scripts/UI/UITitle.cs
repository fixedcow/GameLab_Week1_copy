using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void Update()
	{
		if(Input.anyKeyDown)
		{
			gameObject.SetActive(false);
			GameManager.instance.Initialize();
		}
	}
	#endregion
}
