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
    private Vector3 m_Velocity = new Vector3();
    public float m_Speed;
    public Camera pCamera;
    private float m_Gravity = 9.4f;
    Vector3 lastDirection = new Vector3();
    public CharacterController pController;
    const float SPEED = 5f;
    float m_SpeedBoost = 0f;

    float dashTimer = 0;
    const float DASH_TIMER = 3f;

    State m_CurrentState;

    enum State
    {
        Moving,
        Dashing,
        Idle
    };
    // Start is called before the first frame update
    void Start()
    {
        m_Direction = Vector3.zero;
        m_CurrentState = State.Idle;
        m_Velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //get the cameras forward vector
        Vector3 camForward = pCamera.transform.forward;
        camForward.y = 0;
        camForward = Vector3.Normalize(camForward);
 
        
        switch (m_CurrentState)
        {
            case State.Idle:
                Debug.Log("Idle State");

                m_Velocity = Vector3.zero;
                m_Direction = Vector3.zero;
                getInput();
                break;

            case State.Moving:
                pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime);
                pController.transform.forward = m_Direction;
                m_Direction = Vector3.zero;
                if (InputManager.getDashDown())
                {
                    Dash();
                }
                if (InputManager.getMoveForward())
                {
                    m_Direction += camForward * 1;
                }
                if (InputManager.getMoveLeft())
                {
                    m_Direction += (pCamera.transform.right * -1);
                }
                if (InputManager.getMoveBack())
                {
                    m_Direction += (camForward * -1);
                }
                if (InputManager.getMoveRight())
                {
                    m_Direction += pCamera.transform.right * 1;
                }
                if(m_Direction == Vector3.zero)
                {
                    m_CurrentState = State.Idle;
                }

                lastDirection = m_Direction;
                break;
            case State.Dashing:
                pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime);
                pController.transform.forward = m_Direction;
                if (m_SpeedBoost > 0)
                {
                    m_SpeedBoost -= 0.75f;
                    if (m_SpeedBoost < 0)
                    {
                        m_SpeedBoost = 0;
                    }
                }
                else
                {
                    m_CurrentState = State.Moving;
                    Debug.Log("Setting state to Moving");
                }
                break;
        }
        
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        m_Velocity = m_Direction * (m_Speed + m_SpeedBoost);
        m_Velocity.y -= m_Gravity * Time.deltaTime;
        if (isGrounded())
        {
            m_Velocity.y = 0;
        }
        pController.Move(m_Velocity * Time.deltaTime);
    }

    public bool isGrounded()
    {

       if( Physics.Raycast(transform.position, Vector3.down, 0.4f))
        {
            return true;
        }
        return false;
    }

    public void getInput()
    {
        if((InputManager.getMoveForward() == true || InputManager.getMoveLeft() == true || InputManager.getMoveRight() == true || InputManager.getMoveBack() == true) && m_CurrentState != State.Dashing)
        {
            m_CurrentState = State.Moving;
        }
        else if (InputManager.getDashDown())
        {
            if(m_Direction == Vector3.zero)
            {
                // m_Direction = pController.transform.forward;
                Vector3 direction = pCamera.transform.forward;
                direction.y = 0;
                m_Direction = direction;
            }
            Dash();
        }
        else
        {
            m_CurrentState = State.Idle;
        }
    }

    private void Dash()
    {
        if (dashTimer <= 0)
        {
            dashTimer = DASH_TIMER;
            m_SpeedBoost = m_Speed * 5;
            m_CurrentState = State.Dashing;
            Debug.Log("Switching to Dash State");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.GetType() == typeof( EnemyBase))
        {

        }
    }

}
