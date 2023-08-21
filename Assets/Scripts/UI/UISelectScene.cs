using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISelectScene : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] UISelectScene pre;
	[SerializeField] UISelectScene post;
	[SerializeField] protected List<UISelector> selectors = new List<UISelector>();
	[SerializeField] private List<GameObject> components = new List<GameObject>();
	private int endCount = 0;
	#endregion

	#region PublicMethod
	public virtual void Initialize()
	{
		foreach(GameObject comp in components)
		{
			comp.SetActive(true);
		}
		foreach (UISelector selector in selectors)
		{
			selector.Initialize();
		}
		endCount = 0;
	}
	public void LockIn()
	{
		++endCount;
		if(endCount >= selectors.Count)
		{
			endCount = selectors.Count;
			SelectSceneEnd();
		}
	}
	public void LockOut()
	{
		--endCount;
	}
	public virtual void SelectSceneStart()
	{
		Initialize();
	}
	public virtual void SelectSceneEnd()
	{
		ExitThisSelectScene();
	}
	public void ExitThisAndLoadPreScene()
	{
		if (pre != null)
		{
			ExitThisSelectScene();
			pre.SelectSceneStart();
		}
	}
	#endregion

	#region PrivateMethod
	private void ExitThisSelectScene()
	{
		foreach (GameObject comp in components)
		{
			comp.SetActive(false);
		}
	}
	#endregion
}
