using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject characterPrefab;

	[SerializeField] private Color32 colorMain;
	[SerializeField] private Color32 colorSub;

	private int firstSpawnPointX;
	private int localXScale;
	private const int MAX_TRY_COUNT = 10;
	private const int MAP_WIDTH = 20;
	private const int MAP_HEIGHT = 15;
	#endregion

	#region PublicMethod
	public Character SpawnPlayer(GameObject _target)
	{
		characterPrefab = _target;
		Character c = Instantiate(characterPrefab, new Vector2(firstSpawnPointX, MAP_HEIGHT), Quaternion.identity).GetComponent<Character>();
		c.SetColor(colorMain, colorSub);
		c.transform.localScale = new Vector3(localXScale, 1, 1);
		return c;
	}
	public void Respawn(Character _target)
	{
		Vector2 position = GetRandomSafePositionToSpawn();
		_target.transform.position = position;
		_target.Invincible();
	}
	public void SetFirstSpawnPoint(int xPos)
	{
		firstSpawnPointX = xPos;
		localXScale = xPos > 0 ? -1 : 1;
	}
	#endregion

	#region PrivateMethod
	private Vector2 GetRandomSafePositionToSpawn()
	{
		Vector2 result = new Vector2(0, MAP_HEIGHT);
		for(int i = 0; i < MAX_TRY_COUNT; ++i)
		{
			result.x = Random.Range(-MAP_WIDTH, MAP_WIDTH);
			RaycastHit2D hit = Physics2D.Raycast(result, Vector2.down, MAP_HEIGHT * 2, 1 << LayerMask.NameToLayer("Ground"));
			if(hit.collider != null)
			{
				return result;
			}
		}
		return result;
	}
	#endregion
}
