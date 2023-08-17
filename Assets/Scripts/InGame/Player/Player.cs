using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum TutorialInput {
    Jump, Left, Right, WeakAttack, StrongAttack, Counter, LinkAttack
};


public class Player : MonoBehaviour
{
    #region PublicVariables
    public List<TutorialButton> m_tutorialKeyInput = new List<TutorialButton>();
    public int m_life = 5;

    public bool m_isDead = false;
    #endregion

    #region PrivateVariables
    private int m_dir = 1;
    private float m_speed = 5.0f;
    private float m_jumpPower = 7.0f;
    private float m_weakAttackCoolTime = 0.5f;
    private float m_strongAttackCoolTime = 1.083f;
    private float m_counterCoolTime = 0.7f;
    private float m_hitCoolTime = 0.667f;
    private float m_shieldTime = 0.3f;
    
    private Color m_playerColor = new Color(1f,1f,1f);

    private bool m_isGround = true;
    private bool m_canMove = true;
    private bool m_isCounter = false;
    private bool m_isShield = false;
    private bool m_isKnockBack = false;
    private bool m_isWeakAttack = false;
    

    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;

    [SerializeField] private PlayerSword m_sword;
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject m_body;
    [SerializeField] private Animator m_counterEffect;
	[SerializeField] private UIHeartContainer m_heartContainer;
    [SerializeField] private ParticleSystem m_hitParticle;
    [SerializeField] private ParticleSystem m_smashParticle;
    [SerializeField] private Revival m_revival;
    #endregion

    #region PublicMethod
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();

        m_dir = (int)transform.localScale.x;
    }

    public void Move(int _dir)
    {
        if (m_canMove == false)
            return;

        if(_dir == -1)
        {
            if(m_tutorialKeyInput[(int)TutorialInput.Left].isCheck() == false)
            {
                m_tutorialKeyInput[(int)TutorialInput.Left].Check();
            }
        }
        else
        {
            if (m_tutorialKeyInput[(int)TutorialInput.Right].isCheck() == false)
            {
                m_tutorialKeyInput[(int)TutorialInput.Right].Check();
            }
        }

        transform.Translate(new Vector3(m_speed * _dir * Time.deltaTime, 0, 0));
        
        if(m_dir != _dir)
        {
            m_dir = _dir;
            transform.localScale = new Vector3(_dir, 1f, 1f);
        }
    }
    
    public void Jump()
    {
        if (m_canMove == false)
            return;

        if (m_isGround)
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpPower, ForceMode2D.Impulse);

            m_isGround = false;

            if (m_tutorialKeyInput[(int)TutorialInput.Jump].isCheck() == false)
            {
                m_tutorialKeyInput[(int)TutorialInput.Jump].Check();
            }
        }
    }

    public void WeakAttack()
    {
        if (m_isWeakAttack == true || m_canMove == false)
        {
            return;
        }

        m_isWeakAttack = true;
        m_animator.SetTrigger("WeakAttack");

        m_sword.WeakAttack(m_weakAttackCoolTime);

        if (m_tutorialKeyInput[(int)TutorialInput.WeakAttack].isCheck() == false)
        {
            m_tutorialKeyInput[(int)TutorialInput.WeakAttack].Check();
        }
        if (m_tutorialKeyInput[(int)TutorialInput.LinkAttack].isCheck() == false)
        {
            m_tutorialKeyInput[(int)TutorialInput.LinkAttack].Check();
        }

        Invoke("SetIsWeakAttack", m_weakAttackCoolTime);
    }

    public void StrongAttack()
    {
        if (m_canMove == false || m_isWeakAttack == true)
            return;

        m_canMove = false;
        //m_animator.SetBool("StrongAttack", true);
        m_animator.SetTrigger("StrongAttack");

        if (m_tutorialKeyInput[(int)TutorialInput.StrongAttack].isCheck() == false)
        {
            m_tutorialKeyInput[(int)TutorialInput.StrongAttack].Check();
        }

        Invoke("SetMovable", m_strongAttackCoolTime);

        m_sword.StrongAttack(m_strongAttackCoolTime);
    }

    public void Counter()
    {
        if (m_canMove == false)
            return;

        m_canMove = false;
        m_isCounter = true;

        if (m_tutorialKeyInput[(int)TutorialInput.Counter].isCheck() == false)
        {
            m_tutorialKeyInput[(int)TutorialInput.Counter].Check();
        }

        m_animator.SetTrigger("Counter");
        Invoke("SetMovable", m_counterCoolTime);
        Invoke("SetUncountable", m_counterCoolTime);
    }

    public void Hit()
    {
        if (m_isShield == true)
            return;

        m_canMove = false;
        m_isShield = true;

        m_animator.SetTrigger("Hit");

        StartCoroutine(HitChangeBodyColor());
        Invoke("SetMovable", m_hitCoolTime);
        Invoke("SetShieldFalse", m_shieldTime);

        m_sword.StopAttack();
        
    }

    public void CounterHit()
    {
        if (m_isShield == true)
            return;

        m_canMove = false;
        m_isShield = true;

        m_animator.SetTrigger("CounterHit");

        StartCoroutine(HitChangeBodyColor());
        Invoke("SetMovable", m_hitCoolTime);
        Invoke("SetShieldFalse", m_shieldTime);
        CameraController.instance.AttackShake();
        m_hitParticle.transform.position = transform.position;
        m_hitParticle.Play();

        m_sword.StopAttack();
        WeakKnockBack(m_dir * -1);
    }

    public int GetDirection()
    {
        return m_dir;
    }

	public void Dead()
	{
        m_life--;

        if (m_life <= 0)
        {
            m_isDead = true;
            m_life = 5;
        }

		if(m_heartContainer.PopAndReturnRevivalPossibility() == true)
			m_revival.Revive();
	}
    #endregion

    #region PrivateMethod
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGround = true;
        }   
    }

    private void SetMovable()
    {
        m_canMove = true;
    }

    private void SetUncountable()
    {
        m_isCounter = false;
    }

    private void ResetSwordTag()
    {
        m_sword.tag = "Normal";
    }

    private void SetIsWeakAttack()
    {
        m_isWeakAttack = false;
    }

    private void SetShieldFalse()
    {
        m_isShield = false;
    }

    private void WeakKnockBack(int _dir)
    {
        if (m_isKnockBack == true)
            return;

        m_isKnockBack = true;

        StartCoroutine(ShowWeakKnockBack(_dir));
    }

    private void StrongKnockBack(int _dir)
    {
        if (m_isKnockBack == true)
            return;

        m_isKnockBack = true;

        float knockBackSpeed = 13f;
        float knockBackJump = 5f;
        float mass = m_rigidbody.mass;

        m_rigidbody.AddForce(Vector3.up * knockBackJump, ForceMode2D.Impulse);
        m_rigidbody.AddForce(Vector3.right * knockBackSpeed * _dir, ForceMode2D.Impulse);
        m_rigidbody.drag = 1f;

        StartCoroutine(ShowStrongKnockBack());
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        

        if (collision.CompareTag("WeakAttack"))
        {
            GameObject hitplayer = collision.GetComponent<PlayerSword>().m_player;

            if (m_isCounter == true)
            {
                hitplayer.GetComponent<Player>().CounterHit();
                m_counterEffect.Play("sizeUpWithFadeOut");
            }
            else
            {
                if (m_isShield == false)
                {
                    WeakKnockBack(hitplayer.GetComponent<Player>().GetDirection());
                    m_hitParticle.transform.position = collision.transform.position;
                    m_hitParticle.Play();
                }
                Hit();
                
                

                CameraController.instance.AttackShake();

                
            }
        }

        if (collision.CompareTag("StrongAttack"))
        {
            GameObject hitplayer = collision.GetComponent<PlayerSword>().m_player;

            if (m_isShield == false)
            {
                EffectManager.instance.CallEffect(EffectManager.EEffectType.smash, collision.transform.position, hitplayer.transform.localScale.x);
                m_smashParticle.transform.position = collision.transform.position;
                m_smashParticle.Play();
            }

            Hit();

            CameraController.instance.SmashShake();

            

            StrongKnockBack(hitplayer.GetComponent<Player>().GetDirection());
        }
    }

    IEnumerator HitChangeBodyColor()
    {
        m_body.GetComponent<SpriteRenderer>().color = new Color(1f, 132f/255f, 132f/255f);
        m_playerColor = new Color(1f, 132f / 255f, 132f / 255f);
        yield return new WaitForSeconds(0.1f);

        m_body.GetComponent<SpriteRenderer>().color = Color.white;
        m_playerColor = Color.white;

        StartCoroutine(ShowBodyShield());
    }



    IEnumerator ShowBodyShield()
    {
        int cnt = 0;
        int limit = 6;
        float mul = -0.5f;

        while (cnt < limit)
        {
            m_playerColor.a += mul;
            mul *= -1;
            m_body.GetComponent<SpriteRenderer>().color = m_playerColor;
            cnt++;

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ShowWeakKnockBack(int _dir)
    {
        float knockBackSpeed = 15f;
        float timer = 0f;
        while (true)
        {
            transform.Translate(new Vector3(Mathf.Abs(knockBackSpeed) * _dir * Time.deltaTime, 0, 0));
            knockBackSpeed -= 0.1f;
            timer += Time.deltaTime;
            yield return null;

            if(timer > 0.3f)
            {
                break;
            }
        }
        m_isKnockBack = false;
    }

    IEnumerator ShowStrongKnockBack()
    {
        yield return new WaitForSeconds(1.0f);

        m_isKnockBack = false;
        m_rigidbody.drag = 0f;
    }

    
    #endregion
}
