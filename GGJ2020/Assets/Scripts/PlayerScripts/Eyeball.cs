using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeball : MonoBehaviour
{
    float _timer = -2f;

    // Update is called once per frame
    Vector3 newRot;
    void Update()
    {
        if (_timer < 0)
        {
            newRot = new Vector3(Random.Range(-40, 20), Random.Range(-50, 0), Random.Range(-80, 30));
            _timer = 2f;
        }

        transform.forward = Vector3.Slerp(transform.forward, newRot, Time.deltaTime);
        _timer -= Time.deltaTime;
    }
}
