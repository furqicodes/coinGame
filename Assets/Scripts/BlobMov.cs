using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMov : MonoBehaviour
{
    private float speed = 5.0f;
    public float turnSpeed = 50f;

    private double m_MovX;
    private double m_MovY;

    private double forward;
    private double turn;

    private Blob blob;

    void Start()
    {
        blob = gameObject.GetComponent<Blob>();
    }

    void Update()
    {
        //m_MovX = (blob.getoN(0) - 0.5d) * 2;
        //m_MovY = (blob.getoN(1) - 0.5d) * 2;
        ////more basic movement function added
        //transform.position += new Vector3(speed * (float)m_MovX * Time.deltaTime, 0, speed * (float)m_MovY * Time.deltaTime);

        forward = (blob.getoN(0) - 0.5d) * 2;
        turn = (blob.getoN(0) - 0.5d) * 2;

        transform.Rotate(0, turnSpeed * (float)turn * Time.deltaTime, 0);
        transform.Translate(transform.forward * (float)forward * speed * Time.deltaTime);
        
    }
}
