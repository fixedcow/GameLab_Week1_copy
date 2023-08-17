using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    #region PublicVariables
    public GameObject m_player;
    #endregion

    #region PrivateVariables
    private bool isHit = false;
    private BoxCollider2D m_boxCollider;
    private Vector2 m_colliderOffset = new Vector2(0, 0.2865495f);
    private Vector2 m_colliderWeakAttackOffeset = new Vector2(0, 0.1949511f);
    private Vector2 m_colliderStrongAttackOffeset = new Vector2(0, 0.1436397f);

    private Vector2 m_colliderSize = new Vector2(1, 0.6323969f);
    private Vector2 m_colliderWeakAttackSize = new Vector2(1, 1.140774f);
    private Vector2 m_colliderStrongAttackSize = new Vector2(2.18f, 1.506497f);
    #endregion

    #region PublicMethod
    void Start()
    {
        m_boxCollider = GetComponent<BoxCollider2D>();
    }

    public void WeakAttack(float _weakAttackCoolTime)
    {
        Invoke("SetSwordTagWeakAttack", _weakAttackCoolTime / 3 * 1);
        Invoke("SetSwordTagNormal", _weakAttackCoolTime / 2 * 1);
    }

    public void StrongAttack(float _strongAttackCoolTime)
    {
        Invoke("SetSwordTagStrongAttack", _strongAttackCoolTime / 12 * 6);
        Invoke("SetSwordTagNormal", _strongAttackCoolTime / 12 * 9);
    }

    public void StopAttack()
    {
        SetSwordTagNormal();
        isHit = true;

        Invoke("ResetIsHit", 1f);
    }
    #endregion

    #region PrivateMethod
    private void SetSwordTagNormal()
    {
        transform.tag = "Normal";
        m_boxCollider.offset = m_colliderOffset;
        m_boxCollider.size = m_colliderSize;
    }

    private void SetSwordTagWeakAttack()
    {
        if (isHit == true)
        {
            isHit = false;
            return;
        }

        transform.tag = "WeakAttack";
        m_boxCollider.offset = m_colliderWeakAttackOffeset;
        m_boxCollider.size = m_colliderWeakAttackSize;
    }

    private void SetSwordTagStrongAttack()
    {
        if (isHit == true)
        {
            isHit = false;
            return;
        }

        transform.tag = "StrongAttack";
        m_boxCollider.offset = m_colliderStrongAttackOffeset;
        m_boxCollider.size = m_colliderStrongAttackSize;
    }

    private void ResetIsHit()
    {
        isHit = false;
    }
    #endregion
}
