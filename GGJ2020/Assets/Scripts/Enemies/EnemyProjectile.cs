using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent("Player") as Player;
            
            if(player != null)
            {
                Vector3 dir = transform.position - player.transform.position;
                dir = Vector3.Normalize(dir);
                player.takeDamage(-dir);
            }
        }
    }
}
