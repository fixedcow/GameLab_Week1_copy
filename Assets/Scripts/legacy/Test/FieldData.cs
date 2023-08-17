using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData : MonoBehaviour
{
	#region PublicVariables
	[HideInInspector] public float width;
	[HideInInspector] public float height;
	[HideInInspector] public Vector2 center;

	#endregion
	#region PrivateVariables
	#endregion
	#region PublicMethod
	public void Start()
	{
		width = transform.localScale.x;
		height = transform.localScale.y;
		center = transform.position;

	}
	#endregion
	#region PublicMethod
	#endregion

}
