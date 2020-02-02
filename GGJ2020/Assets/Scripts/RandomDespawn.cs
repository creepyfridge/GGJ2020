using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDespawn : MonoBehaviour
{
    public float _percentChanceToDespawn = 50;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0.0f, 100.0f) < _percentChanceToDespawn)
            gameObject.SetActive(false);

    }
}
