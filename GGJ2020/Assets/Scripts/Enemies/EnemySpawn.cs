using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject _enemyPrefab;

    public GameObject spawn()
    {
        return GameObject.Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);
    }
}
