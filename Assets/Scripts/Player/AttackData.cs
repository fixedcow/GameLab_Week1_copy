using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackData : MonoBehaviour
{
	#region PublicVariables
	public enum EType
	{
		attack = 0,
		smash = 1
	}
	#endregion

	#region PrivateVariables
	[SerializeField] protected EType type;
	[SerializeField] protected Character source;
	[SerializeField] protected Vector3 pointA;
	[SerializeField] protected Vector3 pointB;

	[SerializeField] protected Vector2 direction;
	[SerializeField] protected float magnitude;
	#endregion

	#region PublicMethod
	public EType GetAttackType() => type;
	public Character GetSource() => source;
	#endregion

	#region PrivateMethod
	protected virtual void CheckAttack()
	{
		float dirMult = transform.parent.localScale.x;
		Collider2D[] cols = Physics2D.OverlapAreaAll(transform.position + (dirMult * pointA)
			, transform.position + (dirMult * pointB), 1 << LayerMask.NameToLayer("Character"));
		if (cols.Length != 0)
		{
			foreach (Collider2D col in cols)
			{
				Character c;
				col.TryGetComponent(out c);
				if (c != null)
				{
					Attack(c, dirMult);
				}
			}
		}
	}
	protected virtual void Attack(Character _target, float dirMult)
	{
		_target.Hit(this, dirMult * direction.normalized, magnitude);
	}
		#endregion
	}
