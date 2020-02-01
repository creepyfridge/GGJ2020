using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject model;
    public Wall _wall;

    [HideInInspector]
    public bool isValidSpawn = true;

    public void hideModel()
    {
        isValidSpawn = false;
        model.SetActive(false);
    }

    public void showModel()
    {
        isValidSpawn = false;
        model.SetActive(true);
    }
}
