using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
	#region PublicVariables
	public static CameraController instance;
	public Camera main;
	public CameraShaker shaker;
	#endregion
	#region PrivateVariables
	[SerializeField] private float m_camSpeed;
	[SerializeField] private FieldData m_currentField;
	[SerializeField] private GameObject m_player1;
	[SerializeField] private GameObject m_player2;
	[SerializeField] private List<Parallax> m_parallaxes = new List<Parallax>();
	private float m_camX => main.orthographicSize * ((float)Screen.width / Screen.height);
	private float m_camY => main.orthographicSize;
	private const float ZOOM_MIN = 4f;
	private const float ZOOM_MAX = 10f;
	[SerializeField] private FieldData m_tutorialRoomFieldData;
	[SerializeField] private FieldData m_mainStageFieldData;
	[SerializeField] private bool m_chasePlayer = false;
	#endregion
	#region PublicMethod
	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	public void Start()
	{
		shaker.main = this.main;
	}
	public void Update()
	{
		if(m_chasePlayer == true)
			SetCameraState();
	}
	public void PlayerChasingStart()
	{
		m_chasePlayer = true;
	}
	public void PlayerChasingEnd()
	{
		m_chasePlayer = false;
		transform.position = Vector2.zero;
		main.orthographicSize = 6f;
	}
	public void SetFieldData(FieldData _data)
	{
		m_currentField = _data;
	}
	public void AttackShake()
	{
		shaker.AttackShake();
	}
	public void SmashShake()
	{
		shaker.SmashShake();
	}
	public void GoTutorialRoom()
	{
		m_currentField = m_tutorialRoomFieldData;
	}
	public void GoMainStage()
	{
		m_currentField = m_mainStageFieldData;
	}
	#endregion
	#region PrivateMethod
	private void SetCameraState()
	{
		main.orthographicSize = GetCameraZoomLevel();
		Vector2 destination = GetCameraDestination();
		transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * m_camSpeed);
		foreach (Parallax p in m_parallaxes)
		{
			p.ParallaxMove(main.transform.position);
		}
	}
	private Vector3 GetCameraDestination()
	{
		Vector3 centerPosition = Vector3.Lerp(m_player1.transform.position, m_player2.transform.position, 0.5f);
		Vector2 fieldCenter = m_currentField.center;
		float limitXMin = fieldCenter.x - m_currentField.width / 2;
		float limitXMax = fieldCenter.x + m_currentField.width / 2;
		float limitYMin = fieldCenter.y - m_currentField.height / 2;
		float limitYMax = fieldCenter.y + m_currentField.height / 2;
		centerPosition.x = Mathf.Clamp(centerPosition.x, limitXMin + m_camX, limitXMax - m_camX);
		centerPosition.y = Mathf.Clamp(centerPosition.y, limitYMin + m_camY, limitYMax - m_camY);
		Vector3 result = new Vector3(centerPosition.x, centerPosition.y, -10);
		return result;
	}
	private float GetCameraZoomLevel()
	{
		float distance = Vector2.Distance(m_player1.transform.position, m_player2.transform.position);
		float result = Mathf.Clamp(ZOOM_MIN + distance / 4, ZOOM_MIN, ZOOM_MAX);
		return result;
	}
	#endregion
}