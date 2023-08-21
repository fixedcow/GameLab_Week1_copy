using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	#region PublicVariables
	public static GameManager instance;
	public PlayerManager player1;
	public PlayerManager player2;
	public UnityEvent onGameTutorial;
	public UnityEvent onGameStart;
	public UnityEvent onGameEnd;

	private GameObject stage;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void Awake()
	{
		if (instance == null)
			instance = this;
	}
	public void StartBattle()
	{
		StagePrimeManager.instance.StartStage(stage);
		CameraController.instance.SetPlayer(player1.character.transform, player2.character.transform);
		Indicator.instance.SetPlayer(player1.character.transform, player2.character.transform);
		GameStart();
	}
	public void TutorialStart()
	{
		onGameTutorial.Invoke();
	}
	public void GameStart()
	{
		onGameStart.Invoke();
	}
	public void GameEnd()
	{
		onGameEnd.Invoke();
    }
	public void SetStage(GameObject _stage) => stage = _stage;
	#endregion

	#region PrivateMethod
	private void Start()
	{
		UIController.instance.HideUI();
		player1.SetFirstSpawnPoint(-5);
		player2.SetFirstSpawnPoint(5);
		player1.Initialize();
		player2.Initialize();
		UISelectSceneStage.instance.SelectSceneStart();
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			GameEnd();
			UISelectSceneStage.instance.SelectSceneStart();
		}
	}
	#endregion
}
