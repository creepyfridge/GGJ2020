using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        calculateFootPrint();        
    }

    List<Room> _adjacentRooms;
    public List<Wall> _walls;
    bool _playerIsInside = false;

    public List<EnemySpawn> _enemySpawns;
    int _enemyCount = 0;

    public Transform[] _corners;
    Rect _footprint;

    bool _doorsLocekd = false;

    public void calculateFootPrint()
    {
        _footprint = new Rect(_corners[0].position.x, _corners[0].position.z,
                              _corners[1].position.x - _corners[0].position.x, _corners[1].position.z - _corners[0].position.z);
    }

    public bool hasPotentialDoor()
    {
        for(int i = 0; i < _walls.Count; i++)
        {
            if (_walls[i].hasPotentialDoor())
                return true;
        }
        return false;
    }

    public Rect getFootprint()
    {
        return _footprint;
    }

    public bool playerIsInside()
    {
        return _playerIsInside;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _playerIsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInside = false;
        }
    }

    public void killEnemy()
    {
        _enemyCount--;
        if (_enemyCount == 0)
        {
            toggleDoorLocks();
        }
    }

    public bool isColliding(Room other)
    {
        return _footprint.Overlaps(other.getFootprint(), true);
    }

    public int getPotentialWall()
    {
        return Random.Range(0, _walls.Count);
    }

    public void setActiveDoor(int wallID, int doorID)
    {
        _walls[wallID].setActiveDoor(doorID);
    }
    
    private void Update()
    {
        
    }
    
    public void removeDoors()
    {
        for (int i = 0; i < _walls.Count; i++)
        {
            _walls[i].removeDoors();
        }
    }

    public void toggleDoorLocks()
    {
        _doorsLocekd = !_doorsLocekd;
        for (int i = 0; i < _walls.Count; i++)
        {
            _walls[i].toggleDoorLocks();
        }
    }

    public void spawnEnemies()
    {
        for (int i = 0; i < _enemySpawns.Count; i++)
        {
            _enemySpawns[i].spawn(this);
            _enemyCount++;
        }
        
        if(!_doorsLocekd)
            toggleDoorLocks();
    }
}
