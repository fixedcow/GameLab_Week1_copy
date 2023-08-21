using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionPlatform : Platform
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Animator anim;
	[SerializeField] private Collider2D col;

	[SerializeField] private float reswpawnTime;
	#endregion

	#region PublicMethod
	public override void Initialize()
	{

	}
	public override void Touched()
	{
		anim.Play("FadeOut");
	}
	public void Recall()
	{
		anim.Play("FadeIn");
	}
	public void Activate()
	{
		col.enabled = true;
		gameObject.layer = LayerMask.NameToLayer("Ground");
	}
	public void Deactivate()
	{
		col.enabled = false;
		gameObject.layer = LayerMask.NameToLayer("Default");
		reswpawnTime = Mathf.Clamp(reswpawnTime, 3, float.MaxValue);
		Invoke(nameof(Recall), reswpawnTime - 3);
	}
	#endregion

	#region PrivateMethod
	private void OnDisable()
	{
		CancelInvoke(nameof(Recall));
	}
	#endregion
}
