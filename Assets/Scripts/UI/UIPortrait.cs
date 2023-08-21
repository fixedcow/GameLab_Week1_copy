using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPortrait : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private List<SpriteRenderer> mainColorPart = new List<SpriteRenderer>();
	[SerializeField] private List<SpriteRenderer> subColorPart = new List<SpriteRenderer>();
	#endregion

	#region PublicMethod
	public void Coloring(Color32 _main, Color32 _sub)
	{
		foreach(SpriteRenderer r in mainColorPart)
		{
			r.color = _main;
		}
		foreach(SpriteRenderer r in subColorPart)
		{
			r.color = _sub;
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
