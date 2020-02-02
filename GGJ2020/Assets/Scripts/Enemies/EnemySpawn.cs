using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public static GameObject _player = null;

    public GameObject _enemyPrefab;

    public GameObject spawn()
    {
        GameObject enemy = GameObject.Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);
        enemy.GetComponent<EnemyBase>()._player = _player.transform;
        return enemy;
    }
}
