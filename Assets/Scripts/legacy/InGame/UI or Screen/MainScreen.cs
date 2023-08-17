using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScreen : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private GameObject m_stagePanel;
    [SerializeField] private TextMeshPro m_textMeshPro;
    #endregion

    #region PublicMethod
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            m_stagePanel.SetActive(true);    
        }


    }
    public void HideMainScreen()
    {
        gameObject.SetActive(false);
    }

    public void ShowMainScreen()
    {
        gameObject.SetActive(true);
    }
    #endregion

    #region PrivateMethod
    #endregion
}
