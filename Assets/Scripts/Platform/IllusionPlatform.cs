using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionPlatform : Platform
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	private Collider2D col;
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
		Invoke(nameof(Recall), 3f);
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out col);
		TryGetComponent(out anim);
	}
	#endregion
}
