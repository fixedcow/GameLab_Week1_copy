using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialRoom : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables

	[SerializeField] private FieldData m_field;
	[SerializeField] private GameObject m_floor;

	[SerializeField] private List<TutorialButton> m_buttons = new List<TutorialButton>();
	[SerializeField] private BaseStage m_stageTrigger;
	[SerializeField] private int m_count = 0;
	#endregion
	#region PublicMethod
	public void OnEnable()
	{
		Initialize();
	}
	public void Initialize()
	{
		m_floor.SetActive(true);
		m_count = 0;
		foreach(TutorialButton button in m_buttons)
		{
			button.Initialize();
		}
	}
	public void ButtonChecked()
	{
		if (PlayerManager.instance.isTutorialPlayed == false)
			return;

		++m_count;
		if(m_count >= m_buttons.Count)
		{
			TutorialEnd();
		}
	}
	#endregion
	#region PrivateMethod
	private void TutorialEnd()
	{
		CameraController.instance.GoMainStage();
        GameManager.instance.GameStart();
		m_floor.SetActive(false);
		Invoke("RegeneratePlatform", 3f);
		m_stageTrigger.StartStage();
    }
	private void RegeneratePlatform()
	{
		m_floor.SetActive(true);
	}
	#endregion
}
