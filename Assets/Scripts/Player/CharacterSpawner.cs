using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
	#region PublicVariables
	public static CharacterSpawner instance;
	// TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	public GameObject TEST;
	// TESTEND!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject player1;
	[SerializeField] private GameObject player2;

	private Color32 player1ColorMain = new Color32(123, 9, 9, 255);
	private Color32 player1ColorSub = new Color32(72, 17, 17, 255);
	private Color32 player2ColorMain = new Color32(35, 104, 140, 255);
	private Color32 player2ColorSub = new Color32(9, 65, 98, 255);

	private const int MAX_TRY_COUNT = 10;
	private const int MAP_WIDTH = 20;
	private const int MAP_HEIGHT = 20;
	#endregion

	#region PublicMethod
	public void SetPlayer1(GameObject _target)
	{
		if (player1 != null)
		{
			Destroy(player1.gameObject);
		}
		player1 = _target;
		Character c = Instantiate(player1, new Vector2(-5, MAP_HEIGHT), Quaternion.identity).GetComponent<Character>();
		c.SetColor(player1ColorMain, player1ColorSub);
		PlayerController.instance.SetPlayer1(c);
	}
	public void SetPlayer2(GameObject _target)
	{
		if (player2 != null)
		{
			Destroy(player2.gameObject);
		}
		player2 = _target;
		Character c = Instantiate(player2, new Vector2(5, MAP_HEIGHT), Quaternion.identity).GetComponent<Character>();
		c.SetColor(player2ColorMain, player2ColorSub);
		PlayerController.instance.SetPlayer2(c);
		c.transform.localScale = new Vector3(-1, 1, 1);
	}
	public void Respawn(Character _target)
	{
		Vector2 position = GetRandomSafePositionToSpawn();
		_target.transform.position = position;
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
	private void Update()
	{
		// TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if(Input.GetKeyDown(KeyCode.P))
		{
			SetPlayer1(TEST);
			SetPlayer2(TEST);
		}
		// TESTEND!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	}
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
