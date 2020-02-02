using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Room _room;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _room.spawnEnemies();
            gameObject.SetActive(false);
        }
    }
}
