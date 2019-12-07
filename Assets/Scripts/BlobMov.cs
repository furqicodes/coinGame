using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMov : MonoBehaviour
{
    private float speed = 5.0f;

    private double m_MovX;
    private double m_MovY;

    private Vector3 m_moveHorizontal;
    private Vector3 m_movVertical;

    private Vector3 m_velocity;
    private Rigidbody m_Rigid;

    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_MovX = (gameObject.GetComponent<Blob>().o1 - 0.5d) * 2;
        m_MovY = (gameObject.GetComponent<Blob>().o2 - 0.5d) * 2;

        m_moveHorizontal = transform.right * (float)m_MovX;
        m_movVertical = transform.forward * (float)m_MovY;

        m_velocity = (m_moveHorizontal + m_movVertical).normalized * speed;

        if (m_velocity != Vector3.zero && gameObject.GetComponentInParent<Blob>().coins.Count > 0)
        {
            m_Rigid.MovePosition(m_Rigid.position + m_velocity * Time.fixedDeltaTime);
        }
    }
}
