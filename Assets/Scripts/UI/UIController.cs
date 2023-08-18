using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Indicator indicator;
	#endregion

	#region PublicMethod
	public void DrawUI()
	{
		indicator.gameObject.SetActive(true);
	}
	public void HideUI()
	{
		indicator.gameObject.SetActive(false);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
