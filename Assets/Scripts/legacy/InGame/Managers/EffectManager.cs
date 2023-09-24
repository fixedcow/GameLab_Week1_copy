using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	#region PublicVariables
	public static EffectManager instance;
	public enum EEffectType
	{
		smash = 0,
		absoluteKill = 1,
		hit = 2,
		ripple = 3
	}
	public enum EParticleEffectType
	{
		attack = 0,
		smash = 1,
		player1Dead = 2,
		player2Dead = 3,
	}
	#endregion
	#region PrivateVariables
	[SerializeField] private List<EffectBundle> effects = new List<EffectBundle>();
	[SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();
	#endregion
	#region PublicMethod
	public void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	public void CallEffect(EEffectType _type, Vector2 _position, float _rotMult, float _scaleMult = 1)
	{
		GameObject current = GetEffectFromList(_type);
		EffectBundle bundle = effects[(int)_type];

		if(current == null)
		{
			current = Instantiate(bundle.prefab, _position, Quaternion.identity, transform) as GameObject;
			bundle.list.Add(current);
            current.transform.localScale = new Vector3(_rotMult * _scaleMult, _scaleMult, _scaleMult);
        }
		else
		{
			current.transform.position = _position;
			current.transform.localScale = new Vector3(_rotMult * _scaleMult, _scaleMult, _scaleMult);
			current.SetActive(true);
		}
	}
	public void CallParticleEffect(EParticleEffectType _type, Vector2 _position, Vector2 _direction)
	{
		ParticleSystem current = particles[(int)_type];
		current.gameObject.transform.position = _position;
		current.transform.rotation = Quaternion.FromToRotation(Vector3.up, CameraController.instance.transform.position - (Vector3)_position);
		current.Play();
	}
	#endregion
	#region PrivateMethod
	private GameObject GetEffectFromList(EEffectType type)
	{
		GameObject current = null;
		List<GameObject> currentList = effects[(int)type].list;
		for(int i = 0; i < currentList.Count; ++i)
		{
			if(currentList[i].activeSelf == false)
			{
				current = currentList[i];
				break;
			}
		}
		return current;
	}
	#endregion
}
