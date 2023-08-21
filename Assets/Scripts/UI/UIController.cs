using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	#region PublicVariables
	public static UIController instance;
	#endregion

	#region PrivateVariables
	[SerializeField] private List<GameObject> lifeUIs = new List<GameObject>();
	[SerializeField] private Indicator indicator;
	#endregion

	#region PublicMethod
	public void DrawUI()
	{
		foreach(GameObject life in lifeUIs)
		{
			life.SetActive(true);
		}
		indicator.gameObject.SetActive(true);
	}
	public void HideUI()
	{
		foreach (GameObject life in lifeUIs)
		{
			life.SetActive(false);
		}
		indicator.gameObject.SetActive(false);
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if(instance == null)
			instance = this;
	}
	#endregion
}
