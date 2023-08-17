using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables

    [SerializeField] Transform m_camera;
    [SerializeField] Transform m_player1;
    [SerializeField] Transform m_player2;

    [SerializeField] GameObject m_indicator1;
    [SerializeField] GameObject m_indicator2;

    bool m_isActive;

    float m_posX;
    float m_posY;

    [SerializeField]
    float m_offset;

    float m_cameraBound = 0.5f;

    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void Update()
    {
        IndicatePlayer(m_player1, m_indicator1);
        IndicatePlayer(m_player2, m_indicator2);
    }
    private void IndicatePlayer(Transform _player, GameObject _indicator)
    {
        Vector3 targetPoint = Camera.main.WorldToViewportPoint(_player.position);
        Vector3 carmeraToTarget = targetPoint - Camera.main.WorldToViewportPoint(m_camera.position);

        if (m_cameraBound <= carmeraToTarget.y)
        {
            m_isActive = true;

            m_posY = m_cameraBound;
            m_posX = (carmeraToTarget.x * m_posY) / carmeraToTarget.y;
            Vector3 curr = new Vector3(m_posX + m_cameraBound, m_posY + m_cameraBound, 0);

            if (m_cameraBound <= carmeraToTarget.x)
            {
                Vector3 dest = new Vector3(1f, 1f, 0);

                if (_indicator.transform.position.x < 1)
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(Vector3.Lerp(curr, dest, Time.deltaTime));
                else
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));
                _indicator.transform.position += new Vector3(-1, -1, 0) * m_offset;
            }
            else if (carmeraToTarget.x <= -m_cameraBound)
            {
                Vector3 dest = new Vector3(0f, 1f, 0);

                if (_indicator.transform.position.x > -1)
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(Vector3.Lerp(curr, dest, Time.deltaTime));
                else
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0));
                _indicator.transform.position += new Vector3(1, -1, 0) * m_offset;
            }
            else
            {
                _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(m_posX + m_cameraBound, m_posY + m_cameraBound, 0));
                _indicator.transform.position += Vector3.down * m_offset;
            }

            _indicator.transform.position = new Vector3(_indicator.transform.position.x, _indicator.transform.position.y, 0);
        }
        else if (-m_cameraBound >= carmeraToTarget.y)
        {
            m_isActive = true;

            m_posY = -m_cameraBound;
            m_posX = (carmeraToTarget.x * m_posY) / carmeraToTarget.y;
            Vector3 curr = new Vector3(m_posX + m_cameraBound, m_posY + m_cameraBound, 0);

            if (m_cameraBound <= carmeraToTarget.x)
            {
                Vector3 dest = new Vector3(1f, 0f, 0);

                if (_indicator.transform.position.x < 1)
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(Vector3.Lerp(curr, dest, Time.deltaTime));
                else
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0));
                _indicator.transform.position += new Vector3(-1, 1, 0) * m_offset;
            }
            else if (carmeraToTarget.x  <= -m_cameraBound)
            {
                Vector3 dest = new Vector3(0f, 0f, 0);

                if (_indicator.transform.position.x > 0)
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(Vector3.Lerp(curr, dest, Time.deltaTime));
                else
                    _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0));
                _indicator.transform.position += new Vector3(1, 1, 0) * m_offset;
            }
            else
            {
                _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(m_posX + m_cameraBound, m_posY + m_cameraBound, 0));
                _indicator.transform.position += Vector3.up * m_offset;
            }

            _indicator.transform.position = new Vector3(_indicator.transform.position.x, _indicator.transform.position.y, 0);
        }
        else if (m_cameraBound <= carmeraToTarget.x)
        {
            m_isActive = true;

            m_posX = m_cameraBound;
            m_posY = (carmeraToTarget.y * m_posX) / carmeraToTarget.x;

            _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(m_posX + m_cameraBound, m_posY + m_cameraBound, 0));
            _indicator.transform.position += Vector3.left * m_offset;
            _indicator.transform.position = new Vector3(_indicator.transform.position.x, _indicator.transform.position.y, 0);
        }
        else if (-m_cameraBound >= carmeraToTarget.x)
        {
            m_isActive = true;

            m_posX = -m_cameraBound;
            m_posY = (carmeraToTarget.y * m_posX) / carmeraToTarget.x;

            _indicator.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(m_posX + m_cameraBound, m_posY + m_cameraBound, 0));
            _indicator.transform.position += Vector3. right * m_offset;
            _indicator.transform.position = new Vector3(_indicator.transform.position.x, _indicator.transform.position.y, 0);
        }
        else
            m_isActive = false;

		#endregion

		GameObject dirObject = _indicator.transform.Find("direction").gameObject;

		Vector2 direction = GetPlayerDirection(_player, _indicator.transform);
		DirectionTowardsPlayer(dirObject, direction);

		_indicator.SetActive(m_isActive);
    }

	private Vector2 GetPlayerDirection(Transform _player, Transform _indicator)
	{
		return new Vector2(_player.position.x - _indicator.position.x, _player.position.y - _indicator.position.y);
	}
	private void DirectionTowardsPlayer(GameObject _indicatorDirection, Vector2 direction)
	{
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
		Quaternion rotation = Quaternion.Slerp(_indicatorDirection.transform.rotation, angleAxis, 5 * Time.deltaTime);
		_indicatorDirection.transform.localRotation = rotation;
	}
}
