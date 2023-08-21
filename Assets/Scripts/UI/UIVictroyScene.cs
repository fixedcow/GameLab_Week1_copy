using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVictroyScene : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Character winner;
	private bool readyForInput = false;
	#endregion

	#region PublicMethod
	public void SetWinner(Character _winner)
	{
		Invoke(nameof(ReadyForInput), 1f);
		winner = _winner;
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

		if(readyForInput == true && Input.anyKeyDown)
		{
			Destroy(winner.gameObject);
			winner = null;
			GameManager.instance.Initialize();
			gameObject.SetActive(false);
		}
	}
	private void ReadyForInput()
	{
		readyForInput = true;
	}
	#endregion
}
