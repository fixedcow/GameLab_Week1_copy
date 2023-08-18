using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrawer : MonoBehaviour
{
	#region PublicVariables
	[Range(-1, 1)] public float xAxisMult;
	[Range(-1, 1)] public float yAxisMult;
	public bool xFlip;
	#endregion

	#region PrivateVariables
	private static Camera m_main;
	private static float m_width => m_main.orthographicSize * ((float)Screen.width / Screen.height);
	private static float m_height => m_main.orthographicSize;
	private static float m_scaleMult => m_main.orthographicSize / SCALE_MULT_DIVIDE;
	private const float SCALE_MULT_DIVIDE = 6;
	#endregion

	#region PublicMethod
	public void Awake()
	{
		if (m_main == null)
			m_main = Camera.main;
	}
	public void LateUpdate()
	{
		Draw();
	}
	#endregion

	#region PrivateMethod
	private void Draw()
	{
		int xFlipMult = xFlip == true ? -1 : 1;
		transform.position = new Vector2(m_main.transform.position.x + (xAxisMult * m_width), m_main.transform.position.y + (yAxisMult * m_height));
		transform.localScale = m_scaleMult * new Vector3(xFlipMult, 1, 1);
	}
	#endregion
}
