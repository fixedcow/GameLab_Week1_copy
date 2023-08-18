using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	#region PublicVariables
	public Camera main;
	#endregion
	#region PrivateVariables
	private enum Etype
	{
		attack = 0,
		smash = 1,
		dead = 2
	}
	[SerializeField] List<CameraShakingData> datas = new List<CameraShakingData>();
	#endregion
	#region PublicMethod
	public void AttackShake()
	{
		StartCoroutine(nameof(IE_Shake), datas[(int)Etype.attack]);
	}
	public void SmashShake()
	{
		StartCoroutine(nameof(IE_Shake), datas[(int)Etype.smash]);
	}
	public void DeadShake()
	{
        StartCoroutine(nameof(IE_Shake), datas[(int)Etype.dead]);
    }
	#endregion
	#region PrivatecMethod
	private IEnumerator IE_Shake(CameraShakingData data)
	{
		float halfDuration = data.duration / 2;
		float tick = Random.Range(-10f, 10f);
		float timer = 0f;

		while (timer < data.duration)
		{
			timer += Time.deltaTime / halfDuration;

			tick += Time.deltaTime * data.roughness;
			transform.localPosition = new Vector3((Mathf.PerlinNoise(tick, 0) - 0.5f) * data.magnitude * Mathf.PingPong(timer, halfDuration)
				, (Mathf.PerlinNoise(0, tick) - 0.5f) * data.magnitude * Mathf.PingPong(timer, halfDuration)
				, -10f);
			

			yield return null;
		}
		Vector3 finalPos = Vector3.zero;
		finalPos.z = -10f;
		transform.localPosition = finalPos;
	}
	#endregion
}
