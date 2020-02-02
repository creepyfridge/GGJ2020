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
    public float _lerpMove;
    public float _lerpLook;
    public float _mouseSensitivity = 1.0f;

    public float _cameraCollisionOfset = 0.3f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pivot = _cameraPivot.rotation.eulerAngles;
        pivot.x = 0.0f;
        pivot.z = 0.0f;
        pivot.y += InputManager.getMouse().x * _mouseSensitivity * Time.deltaTime;
        _cameraPivot.rotation = Quaternion.Euler(pivot);
       
        _cameraPivot.transform.position = _player.transform.position;

        float smooth = 1.0f - Mathf.Pow(_lerpMove, Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _cameraTarget.position, smooth);

        smooth = smooth = 1.0f - Mathf.Pow(_lerpLook, Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, (_player.position - transform.position).normalized, smooth);

        RaycastHit hit;
        if(Physics.Raycast(_player.position, (transform.position - _player.position), out hit, (_cameraTarget.position - _player.position).magnitude))
        {
            transform.position = hit.point + ((_player.position - transform.position) * _cameraCollisionOfset);
        }
    }
}
