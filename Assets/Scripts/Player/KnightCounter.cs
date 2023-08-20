using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCounter : AttackData
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public void CounterAttack(Character _source)
	{
		Attack(_source, _source.transform.localScale.x);
	}
	#endregion

	#region PrivateMethod
	protected override void Attack(Character _target, float dirMult)
	{
		_target.Hit(this, -dirMult * direction.normalized, magnitude);
		EffectManager.instance.CallParticleEffect(EffectManager.EParticleEffectType.attack, _target.transform.position, direction.normalized);
	}
	#endregion
}
