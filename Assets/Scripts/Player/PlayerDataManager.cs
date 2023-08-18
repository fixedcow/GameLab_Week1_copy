using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private int lifeCount;
	private int initLifeCount = 5;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		lifeCount = initLifeCount;
	}
	public void SubLife()
	{
		--lifeCount;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
