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
    private float m_Gravity = 14f;
    Vector3 lastVelocity = new Vector3();
    public CharacterController pController;
    const float SPEED = 8f;
    float m_DashBoost = 0f;

    float dashTimer = 0;
    const float DASH_TIMER = 3f;
    //Power up stats
    public float m_AttackPower = 1f;
    public int m_Health = 100;
    public bool m_Armour = false;
    public float m_SpeedBoost = 0f;



    State m_CurrentState;

    enum State
    {
        Moving,
        Dashing,
        Idle,
        Jumping
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
        if (isGrounded())
        {
            //get the cameras forward vector
            Vector3 camForward = pCamera.transform.forward;
            camForward.y = 0;
            camForward = Vector3.Normalize(camForward);
            if(lastVelocity != Vector3.zero)
            {
                pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime);
                pController.transform.forward = m_Direction;
            }
            
            Vector3 dir = Vector3.zero;
            // m_Direction = Vector3.zero;
            if (InputManager.getDashDown() && isGrounded())
            {
                Dash();
            }
            if (InputManager.getMoveForward())
            {
                dir += camForward * 1;
            }
            if (InputManager.getMoveLeft())
            {
                dir += (pCamera.transform.right * -1);
            }
            if (InputManager.getMoveBack())
            {
                dir += (camForward * -1);
            }
            if (InputManager.getMoveRight())
            {
                dir += pCamera.transform.right * 1;
            }
            if(dir != Vector3.zero)
            {
                m_Direction = dir;
                m_Velocity = m_Direction * (m_Speed + m_SpeedBoost + m_DashBoost);
            }
            else
            {
                m_Velocity = Vector3.zero;
            }
            
            if (InputManager.getJumpDown())
            {
                m_Velocity.y += 5.5f;
            }
        }
        else
        {
            m_Velocity.y -= m_Gravity * Time.deltaTime;
        }
        if (m_DashBoost > 0)
        {
            m_DashBoost -= 2f;
            if (m_DashBoost < 0)
            {
                m_DashBoost = 0;
            }
        }
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }


        /*switch (m_CurrentState)
        {
            //Idle state so player doesn't rotate
            case State.Idle:
                getInput();
                break;

            case State.Moving:
                if (isGrounded())
                {
                    pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime);
                    pController.transform.forward = m_Direction;
                    Vector3 dir = Vector3.zero;
                    // m_Direction = Vector3.zero;
                    if (InputManager.getDashDown() && isGrounded())
                    {
                        Dash();
                    }
                    if (InputManager.getMoveForward())
                    {
                        dir += camForward * 1;
                    }
                    if (InputManager.getMoveLeft())
                    {
                        dir += (pCamera.transform.right * -1);
                    }
                    if (InputManager.getMoveBack())
                    {
                        dir += (camForward * -1);
                    }
                    if (InputManager.getMoveRight())
                    {
                        dir += pCamera.transform.right * 1;
                    }

                    dir = Vector3.Normalize(dir);
                    
                    m_Direction = dir;
                    m_Velocity = m_Direction * (m_Speed + m_SpeedBoost + m_DashBoost);
                    if(Vector3.Distance(m_Velocity,lastVelocity) < 0.2)
                    {
                        m_CurrentState = State.Idle;
                    }
                    if (InputManager.getJumpDown())
                    {
                        jump();
                    }
                }
                break;
            case State.Dashing:
                if (isGrounded())
                {


                    if (m_DashBoost > 0)
                    {
                        m_DashBoost -= 2f;
                        if (m_DashBoost < 0)
                        {
                            m_DashBoost = 0;
                        }
                    }
                    else
                    {
                        m_CurrentState = State.Moving;
                        Debug.Log("Setting state to Moving");
                    }
                    m_Velocity = m_Direction * (m_Speed + m_SpeedBoost + m_DashBoost);
                }
                break;
            case State.Jumping:
                if(!isGrounded())
                {
                    m_Velocity.y -= m_Gravity * Time.deltaTime;
                   // m_Velocity.y -= lastVelocity.y;
                }
                else
                {
                    m_Velocity.y = 0;
                    m_CurrentState = State.Moving;
                }
                break;
        } */

        pController.Move(m_Velocity * Time.deltaTime);

        
        lastVelocity = m_Velocity;
    }
    public void jump()
    {
        if(isGrounded())
        {
            m_Velocity.y = 0;
            m_Velocity.y += 7.5f;
            m_CurrentState = State.Jumping;
        }
        
    }
    public bool isGrounded()
    {
       // Ray ray = new Ray(transform.position, Vector3.down);
       // Debug.DrawRay(transform.position, new Vector3(0,-1.5f,0),Color.red);
       if( Physics.Raycast(transform.position, Vector3.down, 1.4f))
        {
            return true;
        }
        return false;
    }

    public void getInput()
    {
        if ((InputManager.getMoveForward() == true || InputManager.getMoveLeft() == true || InputManager.getMoveRight() == true || InputManager.getMoveBack() == true) && m_CurrentState != State.Dashing)
        {
            m_CurrentState = State.Moving;
        }
        else if (InputManager.getDashDown() && isGrounded())
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
        else if(InputManager.getJumpDown())
        {
            jump();
        }
        else {
            m_Velocity = Vector3.zero;
            m_CurrentState = State.Idle;
        }
    }

    private void Dash()
    {
        if (dashTimer <= 0)
        {
            dashTimer = DASH_TIMER;
            m_DashBoost =  50f;
            m_CurrentState = State.Dashing;
            Debug.Log("Switching to Dash State");
        }
    }

    public void addSpeed(float speed)
    {
        m_SpeedBoost += speed;
    }

    public void addAttackPower(float attackPower)
    {
        m_AttackPower += attackPower;
    }

    public void addArmour(int amount)
    {

    }

    public void addJumpHeight(float amount)
    {

    }

   /* public void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.GetType() == typeof( EnemyBase))
        {

        }
    } */

}
