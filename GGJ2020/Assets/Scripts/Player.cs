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
    const float SPEED = 5f;
    float m_SpeedBoost = 0f;

    bool isDashing = false;
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
        m_Velocity = 0f;
        m_CurrentState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        m_Direction = Vector3.zero;
        Vector3 camForward = pCamera.transform.forward;
        camForward.y = 0;
        camForward = Vector3.Normalize(camForward);
        m_Direction.y -= m_Gravity * Time.deltaTime;
        m_Direction.y += lastDirection.y;
        switch (m_CurrentState)
        {
            case State.Idle:
                m_Direction = Vector3.zero;
                
                
                getInput();
                break;

            case State.Moving:

                m_Direction = Vector3.Normalize(m_Direction);
                
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
                    m_Direction += -(pCamera.transform.right) * 1;
                }
                if (InputManager.getMoveBack())
                {
                    m_Direction += -(camForward * 1);
                }
                if (InputManager.getMoveRight())
                {
                    m_Direction += pCamera.transform.right * 1;
                }
                pController.Move((m_Direction * (m_Speed + m_SpeedBoost) * Time.deltaTime));
                break;
            case State.Dashing:
                Debug.Log("In the dashing state");
                m_Direction = lastDirection;
                if (m_SpeedBoost > 0)
                {
                    m_SpeedBoost -= 0.5f;
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
                pController.Move((m_Direction * (m_Speed + m_SpeedBoost) * Time.deltaTime));
                break;
        }
        
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        if (isGrounded())
        {
            m_Direction.y = 0;
        }
        
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

    public void getInput()
    {
        if(InputManager.getMoveForward() == true || InputManager.getMoveLeft() == true || InputManager.getMoveRight() == true || InputManager.getMoveBack() == true)
        {
            m_CurrentState = State.Moving;
        }
        if (InputManager.getDashDown())
        {
            if (m_Direction == Vector3.zero)
            {
                m_Direction = transform.forward;
                lastDirection = m_Direction;
            }
            Dash();
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

}
