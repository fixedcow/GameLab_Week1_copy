using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Manager : StageManager
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] List<TimeLimitPlatform> platforms = new List<TimeLimitPlatform>();
	#endregion

	#region PublicMethod
	public override void Initialize()
	{
		foreach(TimeLimitPlatform platform in platforms)
		{
			platform.Initialize();
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
