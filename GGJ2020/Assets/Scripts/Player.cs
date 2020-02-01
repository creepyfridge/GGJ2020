using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 m_Direction;
    private float m_Speed;
    private float m_Velocity;

    public CharacterController pController;
    // Start is called before the first frame update
    void Start()
    {
        m_Direction = Vector3.zero;
        m_Velocity = 0;
        m_Speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Direction += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_Direction += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_Direction += Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_Direction += Vector3.right;
        }

        pController.Move(m_Direction * m_Speed * Time.deltaTime);
    }
}
