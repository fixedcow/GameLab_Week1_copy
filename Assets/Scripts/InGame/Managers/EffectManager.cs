using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	#region PublicVariables
	public static EffectManager instance;
	public enum EEffectType
	{
		smash = 0
	}
	#endregion
	#region PrivateVariables
	[SerializeField] private List<EffectBundle> effects = new List<EffectBundle>();
	#endregion
	#region PublicMethod
	public void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	public void CallEffect(EEffectType type, Vector2 position, float _rotMult)
	{
		GameObject current = GetEffectFromList(type);
		EffectBundle bundle = effects[(int)type];

		if(current == null)
		{
			current = Instantiate(bundle.prefab, position, Quaternion.identity, transform) as GameObject;
			bundle.list.Add(current);
            current.transform.localScale = new Vector3(_rotMult, 1, 1);
        }
		else
		{
			current.transform.position = position;
            current.transform.localScale = new Vector3(_rotMult, 1, 1);
            current.SetActive(true);
		}
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
