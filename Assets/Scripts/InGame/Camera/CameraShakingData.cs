using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Shaking Data", fileName ="New Shaking Data")]
public class CameraShakingData : ScriptableObject
{
	public float roughness;
	public float magnitude;
	public float duration;
}
