using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] Character player1;
	[SerializeField] Character player2;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void Update()
	{
		Player1InputControl();
		Player2InputControl();
	}
	private void Player1InputControl()
	{
		if (Input.GetKey(KeyCode.A))
		{
			player1.Move(-1);
		}
		if (Input.GetKey(KeyCode.D))
		{
			player1.Move(1);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			player1.Jump();
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			player1.Command1();
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			player1.Command2();
		}
		if (Input.GetKeyDown(KeyCode.J))
		{
			player1.Command3();
		}
	}
	private void Player2InputControl()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			player2.Move(-1);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			player2.Move(1);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			player2.Jump();
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			player2.Command1();
		}
		if (Input.GetKeyDown(KeyCode.Semicolon))
		{
			player2.Command2();
		}
		if (Input.GetKeyDown(KeyCode.Quote))
		{
			player2.Command3();
		}
	}
	#endregion
}
