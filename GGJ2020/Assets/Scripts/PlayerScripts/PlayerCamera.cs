﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform _player;
    public Transform _cameraPivot;
    public Transform _cameraTarget;
    public float _lerpAmount = 1.75f;
    public float _pivotLerp = 11.0f;

    public float _mouseSensitivity = 1.0f;

    // Update is called once per frame
    void Update()
    {
        _cameraPivot.transform.position = _cameraPivot.transform.position + ((_player.transform.position - _cameraPivot.transform.position)
            * _pivotLerp * Time.deltaTime);

        transform.position = transform.position + ((_cameraTarget.position - transform.position) * _lerpAmount * Time.deltaTime);
        transform.forward = transform.forward + ((_player.position - transform.position) * _lerpAmount * Time.deltaTime);

        Vector3 pivot = _cameraPivot.rotation.eulerAngles;
        pivot.x = 0.0f;
        pivot.z = 0.0f;

        pivot.y += InputManager.getMouse().x * _mouseSensitivity * Time.deltaTime;
        _cameraPivot.rotation = Quaternion.Euler(pivot);
        

        
    }
}
