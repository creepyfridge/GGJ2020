using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base class for power ups
public class PowerUpBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
