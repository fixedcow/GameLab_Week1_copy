using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelector : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private UISelectScene scene;
	[SerializeField] private PlayerManager selector;
	[SerializeField] private List<UISelectTarget> targets = new List<UISelectTarget>();
	[SerializeField] private int current;
	[SerializeField] private KeyCode left;
	[SerializeField] private KeyCode right;
	[SerializeField] private KeyCode select;
	[SerializeField] private KeyCode cancel;
	private bool selectFinished;
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		current = 0;
		selectFinished = false;
		foreach(UISelectTarget target in targets)
		{
			target.HighlightOff();
		}
		foreach (UISelectTarget target in targets)
		{
			target.LockOut();
		}
		HightlightOn();
	}
	public bool IsFinished() => selectFinished;
	public PlayerManager GetSelector() => selector;
	public GameObject GetResult() => targets[current].GetResult();
	#endregion

	#region PrivateMethod
	private void Start()
	{
		Initialize();
	}
	private void Update()
	{
		if(selectFinished == false)
		{
			if (Input.GetKeyDown(left))
			{
				MoveLeft();
			}
			if (Input.GetKeyDown(right))
			{
				MoveRight();
			}
			if (Input.GetKeyDown(select))
			{
				LockIn();
			}
		}
		if(Input.GetKeyDown(cancel))
		{
			LockOut();
		}
	}
	private void MoveLeft()
	{
		HightlightOff();
		if(current == 0)
		{
			current = targets.Count - 1;
		}
		else
		{
			--current;
		}
		HightlightOn();
	}
	private void MoveRight()
	{
		HightlightOff();
		if (current == targets.Count -1)
		{
			current = 0;
		}
		else
		{
			++current;
		}
		HightlightOn();
	}
	private void HightlightOn()
	{
		targets[current].HighlightOn();
	}
	private void HightlightOff()
	{
		targets[current].HighlightOff();
	}
	private void LockIn()
	{
		selectFinished = true;
		targets[current].LockIn();
		scene.LockIn();
	}
	private void LockOut()
	{
		selectFinished = false;
		targets[current].LockOut();
		scene.LockOut();
	}
	#endregion
}
