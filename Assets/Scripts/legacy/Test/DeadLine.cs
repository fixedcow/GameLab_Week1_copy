using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnTriggerEnter2D(Collider2D other)
	{
		Player p = other.GetComponent<Player>();
		if (p != null)
		{
			p.Dead();
		}
	}
	#endregion
}
