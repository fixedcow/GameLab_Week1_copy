using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Character self;
	[SerializeField] private Vector3 pointA;
	[SerializeField] private Vector3 pointB;

	[SerializeField] private Vector2 direction;
	[SerializeField] private float magnitude;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void AttackEnemyInArea()
	{
		float dirMult = transform.parent.localScale.x;
		Collider2D col = Physics2D.OverlapArea(transform.position + (dirMult * pointA)
			, transform.position + (dirMult * pointB), 1 << LayerMask.NameToLayer("Character"));
		if (col != null)
		{
			Character c;
			col.TryGetComponent(out c);
			if(c != null)
			{
				CameraController.instance.AttackShake();
				if(c.IsAnimationStateName("Counter") == false)
				{
					c.Hit(dirMult * direction.normalized, magnitude);
					EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.attack, c.transform.position);
				}
				else
				{
					Knight k;
					c.TryGetComponent(out k);
					k.PrintCounterSuccess();
					self.Hit(-dirMult * direction.normalized, magnitude);
					EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.attack, self.transform.position);
				}
			}
		}
	}
	#endregion
}
