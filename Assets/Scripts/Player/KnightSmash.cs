using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSmash : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Vector3 pointA;
	[SerializeField] private Vector3 pointB;

	[SerializeField] private Vector2 direction;
	[SerializeField] private float magnitude;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void SmashEnemyInArea()
	{
		float dirMult = transform.parent.localScale.x;
		Collider2D col = Physics2D.OverlapArea(transform.position + (dirMult * pointA)
			, transform.position + (dirMult * pointB), 1 << LayerMask.NameToLayer("Player"));
		if (col != null)
		{
			Character c;
			col.TryGetComponent(out c);
			if (c != null)
			{
				CameraController.instance.SmashShake();
				EffectManager.instance.CallEffect(EffectManager.EEffectType.smash, transform.position + Vector3.right * dirMult * 3, dirMult);
				EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.smash, c.transform.position);
				c.Hit(new Vector2(dirMult * direction.x, direction.y).normalized, magnitude);
			}
		}
	}
	#endregion
}
