using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private List<ParticleSystem> m_ps = new List<ParticleSystem>();
	#endregion

	#region PublicMethod
	public void PlaySnow()
	{
		foreach (ParticleSystem ps in m_ps)
		{
			ps.Play();
		}
	}
	public void StopSnow()
	{
		foreach (ParticleSystem ps in m_ps)
		{
			ps.Stop();
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
