using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Made by Zach Dubuc 
 * 
 */
public class Player : MonoBehaviour
{
    private Vector3 m_Direction = new Vector3();
    public float m_Speed;
    private float m_Velocity;
    public Camera pCamera;
    private float m_Gravity = 3.4f;
    Vector3 lastDirection = new Vector3();
    public CharacterController pController;
    // Start is called before the first frame update
    void Start()
    {
        m_Direction = Vector3.zero;
        m_Velocity = 0f;
        m_Speed = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        m_Direction = Vector3.zero;
        Vector3 camForward = pCamera.transform.forward;
        camForward.y = 0;
        camForward = Vector3.Normalize(camForward);
        if (Input.GetKey(KeyCode.W))
        {
            m_Direction += camForward *1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Direction += -(pCamera.transform.right) * 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Direction += -(camForward * 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Direction += pCamera.transform.right * 1;
        }

        m_Direction = Vector3.Normalize(m_Direction);

        m_Direction.y -= m_Gravity * Time.deltaTime;
        m_Direction.y += lastDirection.y;
        if(isGrounded())
        {
            m_Direction.y = 0;
        }
        pController.Move((m_Direction * m_Speed) * Time.deltaTime);
        lastDirection = m_Direction;
    }


    public bool isGrounded()
    {

       if( Physics.Raycast(transform.position, Vector3.down, 0.7f))
        {
            return true;
        }
        return false;
    }
}
