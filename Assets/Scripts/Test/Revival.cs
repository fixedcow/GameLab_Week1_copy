using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    #region PublicVariables
    #endregion
    #region PrivateVariables

    [SerializeField] int m_tryCounter = 5;
    [SerializeField] float m_maxDistance = 15;
    [SerializeField] float m_randomRange = 15;
    [SerializeField] float m_spawnPosY = 20;

    #endregion
    #region PublicMethod
	public void Revive()
	{
		for (int i = 0; i < m_tryCounter; i++)
		{
			transform.position = Vector3.up * 10 + Vector3.right * Random.Range(-m_randomRange, m_randomRange);

			Debug.DrawRay(transform.position, Vector2.down * m_maxDistance, Color.green, 0.3f);
			if (Physics2D.Raycast(transform.position, Vector2.down, m_maxDistance, LayerMask.GetMask("Ground")))
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				return;
			}
		}
		transform.position = Vector3.up * m_spawnPosY;
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
    #endregion
    #region PrivateMethod
    #endregion


}
