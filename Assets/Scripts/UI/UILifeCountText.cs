using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILifeCountText : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private TextMeshPro txt;
	private Animator anim;
	#endregion

	#region PublicMethod
	public void Print(int count, bool playAnimation = true)
	{
		if(playAnimation == true)
			anim.Play("LifeCountDamage");
		txt.text = "x" + count.ToString();
	}
	public void Print(int count)
	{
		anim.Play("LifeCountDamage");
		txt.text = "x" + count.ToString();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out txt);
		TryGetComponent(out anim);
	}
	#endregion
}
