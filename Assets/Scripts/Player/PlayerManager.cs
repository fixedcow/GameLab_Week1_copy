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
	public void PlayerDead()
	{
		if (data.SubLife() <= 0)
		{
			// GameManager.instance.GameEnd();
		}
		else
		{
			spawn.Respawn(character);
		}	
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
	#endregion
}
