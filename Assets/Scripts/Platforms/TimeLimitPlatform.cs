using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitPlatform : Platform
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float lifeTime;
	[SerializeField] private Animator anim;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Collider2D col;
	[SerializeField] private Vector2 originPosition;
	#endregion

	#region PublicMethod
	public override void Initialize()
	{
		rb.bodyType = RigidbodyType2D.Kinematic;
		rb.angularVelocity = 0;
		transform.rotation = Quaternion.Euler(0, 0, 0);
		transform.position = originPosition;
		gameObject.layer = LayerMask.NameToLayer("Ground");
		col.enabled = true;
		Invoke(nameof(WarnToDrop), lifeTime);
	}
	public override void Touched()
	{

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
		rb.angularVelocity = Random.Range(-30, 30);
        Invoke(nameof(Deactive), 3f);
	}
	#endregion

	#region PrivateMethod
	private void Deactive()
	{
		gameObject.SetActive(false);
	}
	private void OnDisable()
	{
		CancelInvoke(nameof(WarnToDrop));
		CancelInvoke(nameof(Deactive));
	}
	#endregion
}
