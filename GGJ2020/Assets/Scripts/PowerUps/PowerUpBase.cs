using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base class for power ups
public class PowerUpBase : MonoBehaviour
{
    float _timer = -1f;

    // Update is called once per frame
    Vector3 newPos;
    Vector3 newRot;

    void Update()
    {
        if (_timer < 0)
        {
            
            newPos = new Vector3(transform.position.x, Random.Range(-1, 2), transform.position.z);
            _timer = 1f;
        }
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
        _timer -= Time.deltaTime;


    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            addPowerUp(player.GetComponent(typeof(PlayerPowers)) as PlayerPowers);
        }
    }

    public virtual void addPowerUp(PlayerPowers player)
    {

    }
}
