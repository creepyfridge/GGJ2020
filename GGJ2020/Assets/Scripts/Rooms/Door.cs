using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_Source = gameObject.AddComponent<AudioSource>();
      
    }
    private AudioSource m_Source;
    public GameObject model;
    public Wall _wall;

    public bool _locked = false;
    public float _shutSpeed = 0.3f;
    public Transform _door;
    public Transform _doorOpen;
    public Transform _doorClosed;

    float _timer = 0.0f;

    [HideInInspector]
    public bool isValidSpawn = true;

    public void hideModel()
    {
        isValidSpawn = false;
        model.SetActive(false);
    }

    public void toggleLockDoor()
    {
        AudioClip clip = Resources.Load("Sounds/Door_Close") as AudioClip;
        m_Source.PlayOneShot(clip, 0.05f);
        _timer = 0.0f;
        _locked = !_locked;
    }

    public void showModel()
    {
        isValidSpawn = false;
        model.SetActive(true);
    }

    public void Update()
    {
        if (_timer < _shutSpeed)
            _timer += Time.deltaTime;

        if (_locked)
            _door.position = Vector3.Lerp(_door.position, _doorClosed.position, _timer / _shutSpeed);
        else
            _door.position = Vector3.Lerp(_door.position, _doorOpen.position, _timer / _shutSpeed);

    }
}
