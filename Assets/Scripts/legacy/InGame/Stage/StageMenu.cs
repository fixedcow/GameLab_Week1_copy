using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables

    Vector3 m_tutorialPos1 = new Vector3(-4f, 35, 0);
    Vector3 m_tutorialPos2 = new Vector3(4f, 35, 0);

    Vector3 m_playerPos1 = new Vector3(-5f, 5f, 0);
    Vector3 m_playerPos2 = new Vector3(5f, 5f, 0);

    [SerializeField] private GameObject m_stagePanel;
    [SerializeField] private BaseStage m_stage1;
    [SerializeField] private BaseStage m_stage2;
    [SerializeField] private BaseStage m_stage3;

    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    public void ChooseTutorial()
    {
        GameManager.instance.TutorialStart();
        SpawnInTutorial();
        PlayerManager.instance.InitLife();
        PlayerManager.instance.isTutorialPlayed = true;
        HideStagePanel();
        CallHideMainScreen();
        RunTimeScale();
    }

    public void ChooseStage1()
    {
        m_stage1.GetComponent<Stage1>().StartStage();
        SpawnInStage();
        GameManager.instance.GameStart();
        PlayerManager.instance.InitLife();
        HideStagePanel();
        CallHideMainScreen();
        RunTimeScale();
    }

    public void ChooseStage2()
    {
        m_stage2.GetComponent<Stage2>().StartStage();
        SpawnInStage();
        GameManager.instance.GameStart();
        PlayerManager.instance.InitLife();
        HideStagePanel();
        CallHideMainScreen();
        RunTimeScale();
    }

    public void ChooseStage3()
    {
        m_stage3.GetComponent<Stage3>().StartStage();
        SpawnInStage();
        GameManager.instance.GameStart();
        PlayerManager.instance.InitLife();
        HideStagePanel();
        CallHideMainScreen();
        RunTimeScale();
    }

    public void SpawnInTutorial()
    {
        PlayerManager.instance.m_player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        PlayerManager.instance.m_player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        PlayerManager.instance.m_player1.transform.position = m_tutorialPos1;
        PlayerManager.instance.m_player2.transform.position = m_tutorialPos2;
    }

    private void SpawnInStage()
    {
        PlayerManager.instance.m_player1.transform.position = m_playerPos1;
        PlayerManager.instance.m_player2.transform.position = m_playerPos2;

        PlayerManager.instance.m_player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        PlayerManager.instance.m_player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void HideStagePanel()
    {
        m_stagePanel.SetActive(false);
    }

    public void CallHideMainScreen()
    {
        GameManager.instance.OffMainScreen();
    }


    public void RunTimeScale()
    {
        ESCMenu.g_pause = false;
    }
    #endregion


}
