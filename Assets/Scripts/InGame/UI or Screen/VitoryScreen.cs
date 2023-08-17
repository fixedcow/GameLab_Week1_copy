using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VitoryScreen : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] GameObject m_victoryPlayer1;
    [SerializeField] GameObject m_victoryPlayer2;
    [SerializeField] TextMeshPro m_textMeshPro;

    public bool m_player1IsDead = false;
    public bool m_player2IsDead = false;
    #endregion

    #region PublicMethod

    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.instance.OffVictoryScreen();
        }
    }
    public void ShowVictoryPlayer()
    {
        InitPlayerDead();
        GetVictoryPlayer();

        if (m_player1IsDead)
        {
            m_victoryPlayer2.SetActive(true);
        }
        else if (m_player2IsDead)
        {
            m_victoryPlayer1.SetActive(true);
        }

        FillVictoryName();
    }

    public void HideVictoryPlayer()
    {
        m_victoryPlayer1.SetActive(false);
        m_victoryPlayer2.SetActive(false);
    }

    public void OnVictoryScreen()
    {   
        ShowVictoryPlayer();
        gameObject.SetActive(true);
    }

    public void OffVictoryScreen()
    {
        HideVictoryPlayer();
        gameObject.SetActive(false);
    }
    #endregion

    #region PrivateMethod
    private void GetVictoryPlayer()
    {
        m_player1IsDead = PlayerManager.instance.m_player1.GetComponent<Player>().m_isDead;
        m_player2IsDead = PlayerManager.instance.m_player2.GetComponent<Player>().m_isDead;
    }

    private void FillVictoryName()
    {
        string s = "";

        if (m_player1IsDead == true)
        {
            s = "Player 2 WIN!!";
        }
        else
        {
            s = "Player 1 WIN!!";
        }

        m_textMeshPro.text = s;
    }

    private void InitPlayerDead()
    {
        m_player1IsDead = false;
        m_player2IsDead = false;
    }
    #endregion
}
