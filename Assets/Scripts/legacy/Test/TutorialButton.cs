using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	[SerializeField] TutorialRoom m_room;

	[SerializeField] Animator m_animator;

	private bool m_isCheck = false;
	#endregion
	#region PublicMethod
	public void OnEnable()
	{
		Initialize(); 
	}
	public void Check()
	{
		m_animator.SetBool("check", true);
		m_isCheck = true;
		m_room.ButtonChecked();
	}
	public void Initialize()
	{
		m_animator.SetBool("check", false);
		m_isCheck = false;
	}

	public bool isCheck()
	{
		return m_isCheck;
	}
	#endregion
	#region PrivateMethod
	#endregion
}
