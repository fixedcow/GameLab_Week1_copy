using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Platform
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	[SerializeField] private float speed = 1f;
	#endregion

	#region PublicMethod
	public override void Initialize()
	{
		anim.SetFloat("speedMult", speed);
	}
	public override void Touched()
	{
	
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out anim);
	}
	#endregion
}
