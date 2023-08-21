using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Character character;
	[SerializeField] private KeyCode leftMove;
	[SerializeField] private KeyCode rightMove;
	[SerializeField] private KeyCode fall;
	[SerializeField] private KeyCode jump;
	[SerializeField] private KeyCode command1;
	[SerializeField] private KeyCode command2;
	[SerializeField] private KeyCode command3;
	#endregion

	#region PublicMethod
	public void SetCharater(Character c) => character = c;
	#endregion

	#region PrivateMethod
	private void Update()
	{
		PlayerInputControl();
	}
	private void PlayerInputControl()
	{
		if (character == null)
			return;
		if (Input.GetKey(leftMove))
		{
			character.Move(-1);
		}
		if (Input.GetKey(rightMove))
		{
			character.Move(1);
		}
		if(Input.GetKeyDown(fall))
		{
			character.Fall();
		}
		if (Input.GetKey(jump))
		{
			character.Jump();
		}
		if (Input.GetKeyDown(command1))
		{
			character.Command1();
		}
		if (Input.GetKeyDown(command2))
		{
			character.Command2();
		}
		if (Input.GetKeyDown(command3))
		{
			character.Command3();
		}
	}
	#endregion
}
