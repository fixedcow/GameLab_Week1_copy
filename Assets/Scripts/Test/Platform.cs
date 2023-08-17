using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region PublicVariables

    [SerializeField] public float m_delay;

    [SerializeField] public float m_quakeRange;

    [SerializeField] public float m_quakeTime;

    #endregion

    #region PrivateVariables

    enum PlatformType
    {
        Default,
        Drop,
        Shorten,
        Rotate,
    }

    [SerializeField] PlatformType m_platformType;

    [SerializeField] float m_shortenSpeed;

    [SerializeField] float m_shortenLimit;

    [SerializeField] float m_RotateSpeed;

    [SerializeField] float m_RotateLimit;

    int m_RotateToggle = 1;

    bool m_isQuake;
    bool m_shortenToggle;

    float m_originSizeY;

    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        m_originSizeY = transform.localScale.y;

        switch (m_platformType)
        {
            case PlatformType.Drop:
                StartCoroutine(DropPaltform(m_delay));
                break;
        }
    }

    private void LateUpdate()
    {

        switch (m_platformType)
        {
            case PlatformType.Drop:
                {
                    QuakePlatform();
                }
                break;
            case PlatformType.Shorten:
                {
                    if (m_shortenToggle == false)
                    {
                        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y - (Time.deltaTime * m_shortenSpeed));

                        if (transform.localScale.y < m_shortenLimit)
                            m_shortenToggle = true;
                    }
                    else
                    { 
                        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + (Time.deltaTime* m_shortenSpeed));
                        
                        if (transform.localScale.y > m_originSizeY)
                            m_shortenToggle = false;
                    }
                }
                break;
            case PlatformType.Rotate:
                {

                    if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z) - 180) < 180 - m_RotateLimit)
                        m_RotateToggle = m_RotateToggle * -1;

                    transform.Rotate(Vector3.forward * Time.deltaTime * m_RotateSpeed * m_RotateToggle);
                }
                break;
        }
    }

    private IEnumerator DropPaltform(float _lifeTime = -1)
    {
        if (_lifeTime < 0)
            yield break;

        yield return new WaitForSeconds(_lifeTime);
        m_isQuake = true;

        yield return new WaitForSeconds(m_quakeTime);
        m_isQuake = false;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Collider2D>().isTrigger = true;

    }

    private void QuakePlatform()
    {
        if (m_isQuake == false)
            return;

        transform.position += new Vector3(Random.Range(m_quakeRange * -1, m_quakeRange), Random.Range(m_quakeRange * -1, m_quakeRange), 0) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadLine"))
            Destroy(gameObject);
    }

    #endregion
}
