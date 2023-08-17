using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	#region PublicVariables
	#endregion
	#region PrivateVariables
	[SerializeField] private float m_xAmount;
	[SerializeField] private float m_yAmount;
	#endregion
	#region PublicMethod
	public void ParallaxMove(Vector2 _camPosition)
	{
		transform.position = new Vector2(_camPosition.x * m_xAmount, _camPosition.y * m_yAmount);
	}

	#endregion
	#region PrivateMethod
	#endregion
}
