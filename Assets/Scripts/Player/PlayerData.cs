using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private UILifeCountText text;
	private int life;
	private int initLifeCount = 5;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		life = initLifeCount;
		text.Print(life, false);
	}
	public int SubLife()
	{
		--life;
		text.Print(life);
		return life;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
