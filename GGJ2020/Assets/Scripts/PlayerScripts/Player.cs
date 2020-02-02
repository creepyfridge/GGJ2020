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
    private Vector3 m_LastVel = new Vector3();
    public float m_Speed;
    public Camera pCamera;
    private float m_Gravity = 10f;
    public CharacterController pController;
    const float SPEED = 8f;
    float m_DashBoost = 0f;
    float m_JumpVelocity = 5.5f;
    
    float dashTimer = 0;
    const float DASH_TIMER = 1.75f;
    //Power up stats
    public int m_AttackPower = 1;
    public int m_Health = 100;
    public bool m_Armour = false;
    public float m_SpeedBoost = 0f;
    public float m_JumpBoost = 0f;

    //
    bool isDashing = false;
    bool m_IgnoreGrounded = false;
    int m_IgnoreFrames = 30;
    int m_FrameCount = 0;

    // For audio
    private AudioClip m_Clip;
    public AudioSource m_Source;
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
        //m_Source = GetComponent(typeof(AudioSource)) as AudioSource;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        if (m_IgnoreGrounded)
        {
            m_FrameCount++;
            if (m_FrameCount > m_IgnoreFrames)
            {
                m_IgnoreGrounded = false;
                m_FrameCount = 0;
            }
        }
        

        switch (m_CurrentState)
        {
            case State.Idle:
                idleState();
                break;
            case State.Moving:
                moveState();
                break;
            case State.Dashing:
                dashState();
                break;
            case State.Jumping:
                jumpState();
                break;
        }

        if (!isGrounded() && m_CurrentState != State.Jumping)
        {
            m_Velocity.y -= m_Gravity * Time.deltaTime;
            pController.Move(m_Velocity * Time.deltaTime);
        }/*
        if (isGrounded())
        {
            //get the cameras forward vector
            Vector3 camForward = pCamera.transform.forward;
            camForward.y = 0;
            camForward = Vector3.Normalize(camForward);
           /* if(m_Velocity != Vector3.zero)
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

        pController.Move(m_Velocity * Time.deltaTime); */
    }


    public void jump()
    {
        if(isGrounded())
        {
            m_IgnoreGrounded = true;
            m_Velocity.y += m_JumpVelocity;
            m_CurrentState = State.Jumping;
            Debug.Log("Changing to Jump State");
        }
        
    }
    public bool isGrounded()
    {
       if( Physics.Raycast(transform.position, Vector3.down, 1.4f) && !m_IgnoreGrounded)
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
            m_CurrentState = State.Dashing;
        }
    }

    private void idleState()
    {
        
        if(InputManager.getDashDown())
        {
            Dash();
            return;
        } else if(InputManager.getJumpDown())
        {
            jump();
            return;
        } else if(InputManager.getMoveForwardDown() || InputManager.getMoveBackDown() || InputManager.getMoveLeftDown() || InputManager.getMoveRightDown())
        {
            m_CurrentState = State.Moving;
        }

        if(!isGrounded())
        {
            m_Velocity.y -= m_Gravity * Time.deltaTime;
        }
        else
        {
            m_Velocity = Vector3.zero;
        }

        pController.Move(m_Velocity * Time.deltaTime);
    }
    private void moveState()
    {

        Vector3 camForward = pCamera.transform.forward;
        camForward.y = 0;
        camForward = Vector3.Normalize(camForward);
        pController.transform.forward = Vector3.Slerp(pController.transform.forward, m_Direction, Time.deltaTime * 5f);
          
        m_Direction = Vector3.zero;
        //Input for Dashing
            
        //Movement Input
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
            
        //Normalize the direction
            

        //Get Velocity of the player
        m_Velocity = m_Direction * (m_Speed + m_SpeedBoost + m_DashBoost);
        if(!isGrounded())
        {
            m_Velocity.y -= m_Gravity * Time.deltaTime;
            m_Velocity.y += m_LastVel.y;
        }
        if (InputManager.getDashDown() && isGrounded())
        {
            Dash();
            Debug.Log("Chaning to Dash State");
            //return;
        }
        if (InputManager.getJumpDown())
        {
            jump();
        }
        //If there is no velocity then the player isn't moving, set to idle state
        if (m_Velocity == Vector3.zero)
        {
            Debug.Log("Chaning to Idle State");
            m_CurrentState = State.Idle;
        }
        
        pController.Move(m_Velocity * Time.deltaTime);
        m_LastVel = m_Velocity;
    }
    private void jumpState()
    {
        Debug.Log(m_Velocity);
        if (isGrounded())
        {
            Debug.Log("Changing to moving State");
            m_CurrentState = State.Moving;
            return;
        }
        else
        {
            m_Velocity.y -= m_Gravity * Time.deltaTime;
          /*  m_Velocity.z /= 1.2f;
            m_Velocity.x /= 1.2f; */
        }

        pController.Move(m_Velocity * Time.deltaTime);
        
    }
    private void dashState()
    {
        m_Velocity = m_Direction * (m_Speed + m_SpeedBoost + m_DashBoost);

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
            Debug.Log("Chaning to Moving State");
            m_CurrentState = State.Moving;
        }
        
        pController.Move(m_Velocity * Time.deltaTime);
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

    public void takeDamage(Vector3 knockbackDir, int amount)
    {
        m_Clip = Resources.Load("Sounds/Pear_Damage") as AudioClip;
        m_Source.PlayOneShot(m_Clip);
        m_Health -= amount;
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
