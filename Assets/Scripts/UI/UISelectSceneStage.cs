using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectSceneStage : UISelectScene
{
	#region PublicVariables
	public static UISelectSceneStage instance;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public override void SelectSceneEnd()
	{
		GameManager.instance.SetStage(selectors[0].GetResult());
		UISelectSceneCharacter.instance.SelectSceneStart();
		base.SelectSceneEnd();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if(instance == null)
			instance = this;
	}
	#endregion
}
