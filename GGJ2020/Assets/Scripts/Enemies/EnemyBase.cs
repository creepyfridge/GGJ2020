using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _agent.SetDestination(_player.position);
        _lastPlayerPos = _player.position;
    }

    enum states
    {
        Default,
        Idle,
        ChargingAttack,
        Attacking
    }

    states _state = states.Default;

    public Transform _player;
    Vector3 _lastPlayerPos;

    public float _minTimeBetweenAttacks = 1.0f;
    float _timer = 0.0f;

    public float _attackTime = 0.5f;
    public int _damage = 10;
    public int _health = 10;

    public Transform _saw;
    public Transform _sawRest;
    public Transform _sawAttack;

    public float _attackRadius = 0.5f;
    public float _attackChargeTime = 0.3f;

    public Material _normalMat;
    public Material _flashMat;
    public Renderer _renderer;

    public NavMeshAgent _agent;

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0)
        {
            Destroy(this.gameObject);
        }
        Debug.Log(_state);
        switch(_state)
        {
            case states.Default:
                stateDefault();
                break;
            case states.Idle:
                stateIdle();
                break;
            case states.ChargingAttack:
                stateChargingAttack();
                break;
            case states.Attacking:
                stateAttacking();
                break;
        }
    }

    void stateDefault()
    {
        bool inAttackRadius = (_lastPlayerPos - transform.position).magnitude < _attackRadius;
        _timer += Time.deltaTime;
        
        if (isPlayerVisible())
        {
            Debug.Log("player isVisible");

            _lastPlayerPos = _player.position;
            _agent.SetDestination(_lastPlayerPos);

            if (inAttackRadius)
            {

                if (_timer > _minTimeBetweenAttacks)
                {
                    _state = states.ChargingAttack;
                    _timer = 0.0f;
                }
            }
        }
        else
        {
            Debug.Log("player is not Visible");
            if (inAttackRadius)
            {
                _state = states.Idle;
            }
        }
    }

    void stateIdle()
    {
        if (isPlayerVisible())
        {
            _state = states.Default;
        }
    }

    void stateChargingAttack()
    {
        _agent.SetDestination(transform.position);

        _timer += Time.deltaTime;
        if (_timer > _attackChargeTime)
        {
            _timer = 0.0f;
            _state = states.Attacking;
            gameObject.GetComponent<Renderer>().materials[0] = _normalMat;
        }
        else
        {
            float flashInterval = _attackChargeTime / 5.0f;
            int temp = (int)(_timer / (flashInterval));
            if (temp%2 == 0)
            {
                _renderer.material = _normalMat;
                Debug.Log("set normal color");
            }
            else
            {
                _renderer.material = _flashMat;
                Debug.Log("set flash color");
            }
        }
    }

    void stateAttacking()
    {
        _agent.SetDestination(transform.position);

        _timer += Time.deltaTime;
        if (_timer > _attackTime)
        {
            _state = states.Default;
            _saw.position = _sawRest.position;
        }

        if(_timer < _attackTime)
            _saw.position = _sawRest.position + ((_sawAttack.position - _sawRest.position) * (_timer/_attackTime));



    }

    bool isPlayerVisible()
    {
        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, _player.position - transform.position, out hit);
        Debug.DrawLine(transform.position, hit.point);
        return didHit;
    }

    public void takeDamage(int amount)
    {
        _health -= amount;
    }

    public void OnCollisionEnter(Collision collision)
    {

            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent("Player") as Player;

                if (player != null)
                {
                takeDamage(player.dealDamage());
                }
            }
        

    }
}
