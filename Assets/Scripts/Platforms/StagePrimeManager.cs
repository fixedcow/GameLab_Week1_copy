using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePrimeManager : MonoBehaviour
{
	#region PublicVariables
	public static StagePrimeManager instance;
	#endregion

	#region PrivateVariables
	[SerializeField] List<StageManager> stages = new List<StageManager>();
	#endregion

	#region PublicMethod
	public void Initialize()
	{
		foreach(StageManager stage in stages)
		{
			stage.gameObject.SetActive(false);
		}
	}
	public void StartStage(GameObject _stage)
	{
		Initialize();
		StageManager stage;
		_stage.TryGetComponent(out stage);
		if(stage != null)
		{
			stage.Initialize();
			stage.gameObject.SetActive(true);
		}
	}
	public void RemoveStage()
	{
		Initialize();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
	#endregion
}
