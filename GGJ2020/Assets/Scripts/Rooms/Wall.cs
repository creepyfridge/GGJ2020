﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public List<Door> _doors;
    public GameObject _blankWall;

    public bool hasPotentialDoor()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            if (_doors[i].isValidSpawn)
                return true;
        }
        return false;
    }

    public void setActiveDoor(int id)
    {
        for(int i = 0; i < _doors.Count; i++)
        {
            if (i != id)
                _doors[i].hideModel();
            else
                _doors[i].showModel();
        }
        if(_blankWall!=null)
            _blankWall.SetActive(false);
    }

    public int getPotentialDoor()
    {
        return Random.Range(0, _doors.Count);
    }

    public void removeDoors()
    {
        for(int i = 0; i < _doors.Count; i++)
        {
            if(_doors[i].isValidSpawn)
                _doors[i].hideModel();
        }
    }

    public void toggleDoorLocks()
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            _doors[i].toggleLockDoor();
        }
    }
}
