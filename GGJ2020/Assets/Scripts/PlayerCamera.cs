using System.Collections;
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

    public float _mouseSensitivity = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + ((_cameraTarget.position - transform.position) * _lerpAmount * Time.deltaTime);
        transform.forward = _player.position - transform.position;

        Debug.Log(InputManager.getMouse());
    }
}
