using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPortraitContainer : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject portrait;
	#endregion

	#region PublicMethod
	public void SetPortrait(GameObject _portrait, Color32 _main, Color32 _sub)
	{
		portrait = Instantiate(_portrait, transform) as GameObject;
		UIPortrait p;
		portrait.TryGetComponent(out p);
		p.Coloring(_main, _sub);
	}
	public void ClearPortrait()
	{
		Destroy(portrait);
		portrait = null;
	}
	#endregion

	#region PrivateMethod
	#endregion
}