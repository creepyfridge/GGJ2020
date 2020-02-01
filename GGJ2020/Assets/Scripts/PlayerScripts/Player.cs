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
    private float m_Gravity = 10f;
    Vector3 lastVelocity = new Vector3();
    public CharacterController pController;
    const float SPEED = 8f;
    float m_DashBoost = 0f;
    float m_JumpVelocity = 10f;
    float dashTimer = 0;
    const float DASH_TIMER = 3f;
    //Power up stats
    public float m_AttackPower = 1f;
    public int m_Health = 100;
    public bool m_Armour = false;
    public float m_SpeedBoost = 0f;
    public float m_JumpBoost = 0f;

    bool isDashing = false;

    private State m_CurrentState;
    enum State
    {
        Moving, 
        Dashing,
        Jumping,
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
        if (isGrounded())
        {
            //get the cameras forward vector
            Vector3 camForward = pCamera.transform.forward;
            camForward.y = 0;
            camForward = Vector3.Normalize(camForward);
            if(m_Velocity != Vector3.zero)
            {
                pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime);
                pController.transform.forward = m_Direction;
            }

            //Vector3 dir = Vector3.zero;
            if(!isDashing)
            {
                m_Direction = Vector3.zero;
            }
             
            if (InputManager.getDashDown() && isGrounded())
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
            m_Direction = Vector3.Normalize(m_Direction);

            m_Velocity = m_Direction * (m_Speed + m_SpeedBoost + m_DashBoost);
            m_Velocity.y = 0;

            if (InputManager.getJumpDown())
            {
                m_Velocity.y += 5.5f + m_JumpBoost;
            }
            
        }
        else
        {
            m_Velocity.y -= m_Gravity * Time.deltaTime;
        }
        Debug.Log(m_Velocity.y);
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
            isDashing = false;
        }
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }

        pController.Move(m_Velocity * Time.deltaTime);
    }
    public void jump()
    {
        if(isGrounded())
        {
            m_Velocity.y = 0;
            m_Velocity.y += m_JumpVelocity;
        }
        
    }
    public bool isGrounded()
    {
       if( Physics.Raycast(transform.position, Vector3.down, 1.4f))
        {
            return true;
        }
        return false;
    }

  
    private void Dash()
    {
        if (dashTimer <= 0)
        {
            isDashing = true;
            if (m_Velocity == Vector3.zero)
            {
                Vector3 direction = pCamera.transform.forward;
                direction.y = 0;
                m_Direction = direction;
            }
            dashTimer = DASH_TIMER;
            m_DashBoost =  50f;
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
        m_JumpBoost += amount;
    }

   /* public void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.GetType() == typeof( EnemyBase))
        {

        }
    } */

}
