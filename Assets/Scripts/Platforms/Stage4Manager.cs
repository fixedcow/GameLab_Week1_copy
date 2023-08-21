using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Manager : StageManager
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Animator animLigthening;
	[SerializeField] private List<ThunderPlatform> platforms = new List<ThunderPlatform>();
	[SerializeField] private float lighteningThunderDelay;
	[SerializeField] private float thunderCooldown;
	[SerializeField] private int thunderCount;

	private float thunderTimer = 0;
	#endregion

	#region PublicMethod
	public override void Initialize()
	{
		thunderTimer = 0;
	}
	#endregion

	#region PrivateMethod
	private void Update()
	{
		thunderTimer += Time.deltaTime;
		if(thunderTimer > thunderCooldown )
		{
			thunderTimer = 0;
			Lightening();
		}
	}
	private void Lightening()
	{
		animLigthening.Play("Lightening");
		Invoke(nameof(ThunderRandomPlatforms), lighteningThunderDelay);
	}
	private void ThunderRandomPlatforms()
	{
		List<ThunderPlatform> p = ShuffleList(platforms);
		thunderCount = Mathf.Clamp(thunderCount, 0, platforms.Count);
		for(int i = 0; i < thunderCount; ++i)
		{
			p[i].Thunder();
		}
		CameraController.instance.SmashShake();
	}
	private List<ThunderPlatform> ShuffleList(List<ThunderPlatform> list)
	{
		int random1, random2;
		ThunderPlatform temp;

		for (int i = 0; i < list.Count; ++i)
		{
			random1 = Random.Range(0, list.Count);
			random2 = Random.Range(0, list.Count);

			temp = list[random1];
			list[random1] = list[random2];
			list[random2] = temp;
		}

		return list;
	}
	#endregion
}
