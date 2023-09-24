using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UILifeCountText : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private TextMeshPro txt;
	[SerializeField] private Animator anim;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		txt.DOColor(Color.white, 0f);
		txt.DOScale(1f, 0f);
	}
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
	#endregion
}
