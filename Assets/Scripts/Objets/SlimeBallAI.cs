using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBallAI : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private enum EState
	{
		normal = 0,
		rage = 1
	}
	private EState state;
	private GameObject eye;
	private GameObject eyebrow;
	private SpriteRenderer mainColorPart;
	private SpriteRenderer subColorPart;
	private Color32 normalColorMain = new Color32(96, 128, 86, 255);
	private Color32 normalColorSub = new Color32(49, 87, 40, 255);
	private Color32 rageColorMain = new Color32(128, 87, 86, 255);
	private Color32 rageColorSub = new Color32(87, 40, 44, 255);

	[SerializeField] private Character targetCharacter;

	[SerializeField] private float bounceMult;
	[SerializeField] private float normalSpeed;
	[SerializeField] private float rageSpeed;

	private const float MAX_EYE_TRAKING_RADIUS = 0.35f;
	private const float MAX_TRACKING_DISTANCE = 10f;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		NormalMode();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		eye = transform.Find("eye").gameObject;
		eyebrow = transform.Find("eye/eyebrow").gameObject;
		transform.Find("main color part").TryGetComponent(out mainColorPart);
		transform.Find("sub color part").TryGetComponent(out subColorPart);
	}
	private void Start()
	{
		
	}
	private void Update()
	{
		if(targetCharacter != null)
		{
			EyeTracking();
			ChaseTargetCharacter();
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == null)
			return;

		Character c = collision.GetComponent<Character>();
		if (c != null)
		{
			Debug.Log("bounce");
			Vector2 direction = (c.transform.position - transform.position).normalized;
			c.Bounce(direction, bounceMult);
		}
	}
	private void EyeTracking()
	{
		Vector2 direction = (targetCharacter.transform.position - transform.position).normalized;
		float distance = Vector2.Distance(targetCharacter.transform.position, transform.position);
		distance = Mathf.Clamp(distance, 0, MAX_TRACKING_DISTANCE);
		float position = Mathf.Lerp(0, MAX_EYE_TRAKING_RADIUS, distance / MAX_TRACKING_DISTANCE);
		eye.transform.localPosition = direction * position;
	}
	private void ChaseTargetCharacter()
	{

	}
	private void NormalMode()
	{
		mainColorPart.color = normalColorMain;
		subColorPart.color = normalColorSub;
		eyebrow.SetActive(false);
		state = EState.normal;
	}
	private void RageMode()
	{
		mainColorPart.color = rageColorMain;
		subColorPart.color = rageColorSub;
		eyebrow.SetActive(true);
		state = EState.rage;
	}
	#endregion
}
