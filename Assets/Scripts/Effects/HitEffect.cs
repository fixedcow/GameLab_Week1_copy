using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitEffect : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private SpriteRenderer sr;
	[SerializeField] private float targetScaleMin;
	[SerializeField] private float targetScaleMax;
	[SerializeField] private float duration;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		transform.localScale = Vector3.zero;
		sr.material.SetFloat("_FadeAmount", 0);
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out sr);
		Initialize();
		EffectStart();
	}
	private void EffectStart()
	{
		transform.eulerAngles = new Vector3(0, 0, Random.Range(-90f, 90f));
		transform.DOScale(Random.Range(targetScaleMin, targetScaleMax), duration);
		sr.material.DOFloat(1, "_FadeAmount", duration);
		Invoke(nameof(DeactiveSelf), duration);
	}
	private void DeactiveSelf()
	{
		gameObject.SetActive(false);
	}
	#endregion
}
