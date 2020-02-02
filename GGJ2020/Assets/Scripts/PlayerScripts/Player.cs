using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public CharacterController pController;
    const float SPEED = 8f;
    float m_DashBoost = 0f;
    float m_JumpVelocity = 10f;
    float dashTimer = 0;
    const float DASH_TIMER = 1.5f;
    //Power up stats
    public int m_AttackPower = 1;
    public int m_Health = 100;
    public bool m_Armour = false;
    public float m_SpeedBoost = 0f;
    public float m_JumpBoost = 0f;
    private float m_VelUp;
    bool isDashing = false;
    bool m_IgnoreGrounded = false;
    int m_IgnoreFrames = 30;
    int m_FrameCount = 0;
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
        if(m_Health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        if (isGrounded())
        {
            //get the cameras forward vector
            Vector3 camForward = pCamera.transform.forward;
            camForward.y = 0;
            camForward = Vector3.Normalize(camForward);
            if(m_Velocity != Vector3.zero)
            {
                pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime * 5f);
                m_VelUp += Time.deltaTime;
                if (m_VelUp > 2)
                {
                    m_VelUp = 2;
                }
            }
          

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
            if(!m_IgnoreGrounded)
            {
                m_Velocity.y = 0;
            }
            else
            {
                m_FrameCount++;
                if(m_FrameCount > m_IgnoreFrames)
                {
                    m_IgnoreGrounded = false;
                    m_FrameCount = 0;
                }
            } 

            if (InputManager.getJumpDown() && !isDashing)
            {
                m_Velocity.y += 5.5f + m_JumpBoost;
                m_Velocity.z = (m_Velocity.z / 1.75f);
                m_Velocity.x = (m_Velocity.x / 1.75f);

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
            m_IgnoreGrounded = true;
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
            m_DashBoost =  30f;
        }
    }
    public void knockback(Vector3 knockbackDir)
    {
        float timer = 2f;
        m_Velocity = knockbackDir * 1.25f ;
        m_Velocity.y += 0.25f;
        while (timer > 0)
        {
            
            pController.Move(m_Velocity * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        
    }

    public void addSpeed(float speed)
    {
        m_SpeedBoost += speed;
    }

    public void addAttackPower(int attackPower)
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

    public void takeDamage(Vector3 knockbackDir)
    {
        m_Health -= 12;
        knockback(knockbackDir);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (isDashing)
        {
            isDashing = false;
            m_DashBoost = 0;
            if (other.CompareTag("Enemy"))
            {
            
                
                EnemyBase enemy = other.GetComponent("EnemyBase") as EnemyBase;

                if (enemy != null)
                {
                    enemy.takeDamage(m_AttackPower);
                    Debug.Log("Did " + m_AttackPower + " Damage!");
                }
            }
        }
        
    } 

}
