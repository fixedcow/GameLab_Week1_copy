using System.Collections;
using System.Collections.Generic;
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

	//TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	public GameObject characterPrefab;
	//TESTEND!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void Awake()
	{
		if (instance == null)
			instance = this;
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
	#endregion

	#region PrivateMethod
	private void Start()
	{
		player1.SetFirstSpawnPoint(-5);
		player2.SetFirstSpawnPoint(5);
		player1.Initialize();
		player2.Initialize();
	}
	private void Update()
	{
		//TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if (Input.GetKeyDown(KeyCode.P))
		{
			player1.SpawnPlayer(characterPrefab);
			player2.SpawnPlayer(characterPrefab);
			player1.Initialize();
			player2.Initialize();
			CameraController.instance.SetPlayer(player1.character.transform, player2.character.transform);
			Indicator.instance.SetPlayer(player1.character.transform, player2.character.transform);
			GameStart();
		}
		//TESTEND!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	}
	#endregion
}
