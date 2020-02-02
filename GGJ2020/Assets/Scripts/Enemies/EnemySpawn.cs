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

    public GameObject spawn(Room room)
    {
        GameObject enemy = GameObject.Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);
        EnemyBase ebase = enemy.GetComponent<EnemyBase>();
        ebase._player = _player.transform;
        ebase._Room = room;
        return enemy;
    }
}
