using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIVictroyScene : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private TextMeshPro text;
	[SerializeField] private Character winner;
	#endregion

	#region PublicMethod
	public void SetWinner(Character _winner, string _str)
	{
		winner = _winner;
		text.text = _str + " Victory!";
		PrintWinner();
	}
	public void PrintWinner()
	{
		gameObject.SetActive(true);
		if(winner != null)
		{
			winner.MakeWinnerStatue();
		}
	}
	#endregion

	#region PrivateMethod
	private void Update()
	{
		if (winner == null)
			return;

		if(Input.anyKeyDown)
		{
			Destroy(winner.gameObject);
			winner = null;
			GameManager.instance.Initialize();
			gameObject.SetActive(false);
		}
	}
	#endregion
}
