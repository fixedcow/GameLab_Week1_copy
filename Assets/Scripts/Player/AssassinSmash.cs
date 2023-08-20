using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinSmash : AttackData
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float effectPositionX;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void SmashEnemyInArea()
	{
		if (CheckAttack() == true)
		{
			float dirMult = transform.parent.localScale.x;
			Vector2 drawPosition = (Vector2)transform.position + dirMult * effectPositionX * Vector2.right;
			EffectManager.instance.CallEffect(EffectManager.EEffectType.absoluteKill, drawPosition, dirMult);
		}
	}
	#endregion
}
