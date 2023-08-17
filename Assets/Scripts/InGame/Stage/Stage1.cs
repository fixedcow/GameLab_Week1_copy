using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : BaseStage
{
    #region PublicVariables
    #endregion

    #region PrivateVariables

    [SerializeField]
    Vector3 m_startPoint;

    [SerializeField]
    Vector3 m_platformSize;

    enum Direct
    {
        Right,
        Left,
        Center,
    }

    [SerializeField] float m_time;

    [SerializeField] float m_interval;

    [SerializeField] float m_size;
    
    [SerializeField] float m_quakeRange;

    [SerializeField] float m_quakeTime;


    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    public override void StartStage()
    {
        base.InitStage();
        Platforms = new GameObject { name = "Platforms" };

        MakePlatform(Direct.Center, m_startPoint);
        MakePlatform(Direct.Right, m_startPoint);
        MakePlatform(Direct.Left, m_startPoint);
    }

    void MakePlatform(Direct _direct, Vector3 _startPoint)
    {
        float _time = m_time;

        if (_direct == Direct.Left)
            _startPoint.x *= -1f;

        if (_direct == Direct.Center)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DefaultPlatform");
            GameObject go = Instantiate(prefab);

            go.GetComponent<Transform>().localScale = new Vector3(m_startPoint.x * 2, m_platformSize.y, m_platformSize.z);
            go.GetComponent<Transform>().position = new Vector3(0, m_startPoint.y, m_startPoint.z);
            go.transform.SetParent(Platforms.transform);
            return;
        }
                    
        for (int i = 0; i < m_size; i++)
        {

            GameObject prefab = Resources.Load<GameObject>("Prefabs/DropPlatform");
            GameObject go = Instantiate(prefab);

            go.GetComponent<Platform>().m_delay = _time;
            _time -= m_interval;
			if (_time <= 0)
			{
				Destroy(go);
				return;
			}

            go.GetComponent<Transform>().localScale = m_platformSize;

            Vector3 offset = new Vector3(go.GetComponent<Transform>().localScale.x / 2, 0, 0);
            if (_direct == Direct.Right)
            {
                go.GetComponent<Transform>().position = _startPoint + offset;
                _startPoint.x += m_platformSize.x;
            }
            else if (_direct == Direct.Left)
            {
                go.GetComponent<Transform>().position = _startPoint - offset;
                _startPoint.x -= m_platformSize.x;
            }

            go.GetComponent<Platform>().m_quakeRange = m_quakeRange;
            go.GetComponent<Platform>().m_quakeTime = m_quakeTime;

            go.transform.SetParent(Platforms.transform); 

        }
    }

    

    #endregion

}
