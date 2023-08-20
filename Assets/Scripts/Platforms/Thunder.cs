using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	[SerializeField] private float bounceMagnitude;
	[SerializeField] private float radius;
	#endregion

	#region PublicMethod
	public void HitGround()
	{
		transform.localPosition = new Vector2(Random.Range(-1f, 1f), 0);
		anim.Play("Thunder");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Character"));

		if(colliders.Length != 0)
		{
			Character c;
			foreach(Collider2D collider in colliders)
			{
				collider.TryGetComponent(out c);
				Vector2 direction = (c.transform.position - transform.position).normalized;
				c.Bounce(direction, bounceMagnitude);
			}
		}
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out anim);
	}
	#endregion
}
