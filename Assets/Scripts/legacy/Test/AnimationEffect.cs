using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffect : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	[SerializeField] private GameObject main;
	#endregion
	#region PublicMethod
	public void EffectEnd()
	{
		main.SetActive(false);
	}
	#endregion
	#region PrivateMethod
	#endregion
}
