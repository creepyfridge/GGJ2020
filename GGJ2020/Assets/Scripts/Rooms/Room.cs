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

    public List<EnemySpawn> enemySpawns;

    public Transform[] _corners;
    Rect _footprint;
    
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
        _walls.RemoveAt(wallID);
    }
    /*
    private void Update()
    {
        Vector3 corner1 = new Vector3(_footprint.x, 0, _footprint.y);
        Vector3 corner2 = new Vector3(_footprint.x + _footprint.width, 0, _footprint.y + _footprint.height);

        Debug.DrawLine(corner1, corner1 + new Vector3(_footprint.width, 0, 0), Color.red);
        Debug.DrawLine(corner1, corner1 + new Vector3(0, 0, _footprint.height), Color.red);
        Debug.DrawLine(corner2, corner2 - new Vector3(_footprint.width, 0, 0), Color.red);
        Debug.DrawLine(corner2, corner2 - new Vector3(0, 0, _footprint.height), Color.red);
    }
    */
    public void removeDoors()
    {
        for (int i = 0; i < _walls.Count; i++)
        {
            _walls[i].removeDoors();
        }
    }
}
