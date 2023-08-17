using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeart : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Animator animator;
	#endregion

	#region PublicMethod
	public void Pop()
	{
		animator.SetBool("pop", true);
	}
	public void Initialize()
	{
		animator.SetBool("pop", false);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
