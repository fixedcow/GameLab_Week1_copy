using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitPlatform : Platform
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float lifeTime;
	private Animator anim;
	private Rigidbody2D rb;
	private Collider2D col;
	private Vector2 originPosition;
	#endregion

	#region PublicMethod
	public override void Initialize()
	{
		rb.bodyType = RigidbodyType2D.Kinematic;
		transform.position = originPosition;
		gameObject.layer = LayerMask.NameToLayer("Ground");
		col.enabled = true;
		Invoke(nameof(WarnToDrop), lifeTime);
	}
	public void WarnToDrop()
	{
		anim.Play("DropToWarn");
	}
	public void Drop()
	{
		rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.layer = LayerMask.NameToLayer("Default");
		col.enabled = false;
        Invoke(nameof(Deactive), 3f);
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
        TryGetComponent(out anim);
		TryGetComponent(out rb);
		TryGetComponent(out col);
        originPosition = transform.position;
    }
	private void Deactive()
	{
		gameObject.SetActive(false);
	}
	#endregion
}
