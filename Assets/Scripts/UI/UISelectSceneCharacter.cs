using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectSceneCharacter : UISelectScene
{
	#region PublicVariables
	public static UISelectSceneCharacter instance;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public override void SelectSceneEnd()
	{
		foreach(UISelector selector in selectors)
		{
			selector.GetSelector().SpawnPlayer(selector.GetResult());
			selector.GetSelector().Initialize();
		}
		GameManager.instance.StartBattle();
		base.SelectSceneEnd();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
	#endregion
}
