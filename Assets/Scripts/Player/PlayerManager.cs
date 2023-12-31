using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	#region PublicVariables
	public Character character;
	#endregion

	#region PrivateVariables
	[SerializeField] private PlayerController controller;
	[SerializeField] private CharacterSpawner spawn;
	[SerializeField] private PlayerData data;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		data.Initialize();
	}
	public void SpawnPlayer(GameObject _characterPrefab)
	{
		Character c = spawn.SpawnPlayer(_characterPrefab);
		controller.SetCharater(c);
		SetCharacter(c);
		c.SetOwner(this);
	}
	public void GameEndDeactivePlayer()
	{
		if(character != null)
		{
			Destroy(character.gameObject);
			ClearPortrait();
		}
	}
	public void ClearPortrait()
	{
		spawn.ClearPortrait();
	}
	public void PlayerDead()
	{
		if (data.SubLife() <= 0)
		{
			GameManager.instance.GameEnd();
			GameManager.instance.GetVictroyScene().SetWinner(GetWinnerCharacter());
		}
		else
		{
			spawn.Respawn(character);
		}
	}
	public void SetControllerDeactive()
	{
		controller.SetCharater(null);
	}
	public void SetFirstSpawnPoint(int _xPos)
	{
		spawn.SetFirstSpawnPoint(_xPos);
	}
	#endregion

	#region PrivateMethod
	private void SetCharacter(Character _character)
	{
		if(character != null)
		{
			Destroy(character.gameObject);
		}
		character = _character;
	}
	private Character GetWinnerCharacter()
	{
		if (this == GameManager.instance.player1)
		{
			return GameManager.instance.player2.character;
		}
		else
		{
			return GameManager.instance.player1.character;
		}
	}
	#endregion
}
