using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	[SerializeField] private float bounceMult;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out anim);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == null)
			return;

		Character c = collision.GetComponent<Character>();
		if (c != null)
		{
			Vector2 direction = (c.transform.position - transform.position).normalized;
			anim.Play("Bounce");
			c.Bounce(direction, bounceMult);
		}
	}
	#endregion
}
