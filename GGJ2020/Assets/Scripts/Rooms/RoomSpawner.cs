using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        do
        {
            spawnRoom();
        } while (_rooms.Count < _numRoomsInFloor);

        spawnJobsRoom();

        removeDoors();
    }

    void removeDoors()
    {
        for(int i = 0; i < _rooms.Count; i++)
        {
            _rooms[i].removeDoors();
        }
    }

    public List<Room> _rooms;
    public GameObject _pivot;
    public List<GameObject> _roomPrefabs;
    public GameObject _jobsRoom;
    public int _numRoomsInFloor;


    void spawnJobsRoom()
    {
        bool isColliding = false;
        GameObject pivot = null;
        int roomId = -1;
        int wallID = -1;
        int doorID = -1;
        Room newRoom = null;
        int newWallID = -1;
        int newDoorID = -1;
        do
        {
            //pick spawnDoor
            roomId = pseudoRandomID();
            wallID = _rooms[roomId].getPotentialWall();
            doorID = _rooms[roomId]._walls[wallID].getPotentialDoor();

            if (pivot == null)
            {
                //pick new Room
                GameObject newRoomObject = GameObject.Instantiate(_jobsRoom);
                newRoomObject.transform.rotation = Quaternion.Euler(Vector3.zero);

                newRoom = newRoomObject.GetComponentInChildren<Room>();

                newWallID = newRoom.getPotentialWall();
                newDoorID = newRoom._walls[newWallID].getPotentialDoor();

                pivot = GameObject.Instantiate(_pivot);


                //move pivot to door
                pivot.transform.position = newRoom._walls[newWallID]._doors[newDoorID].transform.position;
                //make room child of pivot
                newRoomObject.transform.parent = pivot.transform;
            }
            //place/rotate the room

            for (int i = 0; i < 360; i += 90)
            {
                pivot.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (float)i, 0.0f));
                pivot.transform.position = _rooms[roomId]._walls[wallID]._doors[doorID].transform.position;
                newRoom.calculateFootPrint();
                isColliding = false;

                for (int j = 0; j < _rooms.Count && !isColliding; j++)
                {
                    if (newRoom.isColliding(_rooms[j]))
                    {
                        isColliding = true;
                    }
                }

                if (!isColliding)
                    break;
            }

            if (isColliding)
            {                
                _rooms[roomId]._walls[wallID]._doors[doorID].hideModel();
            }
        }
        while (isColliding);

        _rooms.Add(newRoom);
        _rooms[roomId].setActiveDoor(wallID, doorID);
        newRoom.setActiveDoor(newWallID, newDoorID);
        sortRooms();
    }

    void spawnRoom()
    {
        bool isColliding = false;
        GameObject pivot = null;
        int roomId = -1;
        int wallID = -1;
        int doorID = -1;
        Room newRoom = null;
        int newWallID = -1;
        int newDoorID = -1;
        do
        {
            //pick spawnDoor
            roomId = pseudoRandomID();
            wallID = _rooms[roomId].getPotentialWall();
            doorID = _rooms[roomId]._walls[wallID].getPotentialDoor();
 
            if (pivot == null)
            {
                //pick new Room
                GameObject newRoomObject = GameObject.Instantiate(_roomPrefabs[UnityEngine.Random.Range(0, _roomPrefabs.Count)]);
                newRoomObject.transform.rotation = Quaternion.Euler(Vector3.zero);

                newRoom = newRoomObject.GetComponentInChildren<Room>();

                newWallID = newRoom.getPotentialWall();
                newDoorID = newRoom._walls[newWallID].getPotentialDoor();

                pivot = GameObject.Instantiate(_pivot);


                //move pivot to door
                pivot.transform.position = newRoom._walls[newWallID]._doors[newDoorID].transform.position;
                //make room child of pivot
                newRoomObject.transform.parent = pivot.transform;
            }
            //place/rotate the room
            
            for (int i = 0; i < 360; i += 90)
            {
                pivot.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (float)i, 0.0f));
                pivot.transform.position = _rooms[roomId]._walls[wallID]._doors[doorID].transform.position;
                newRoom.calculateFootPrint();
                isColliding = false;

                for (int j = 0; j < _rooms.Count && !isColliding; j++)
                {
                    if (newRoom.isColliding(_rooms[j]))
                    {
                        isColliding = true;
                    }
                }

                if(!isColliding)
                    break;
            }

            if(isColliding)
            {
                _rooms[roomId]._walls[wallID]._doors[doorID].hideModel();
            }
        }
        while (isColliding);

        _rooms.Add(newRoom);
        _rooms[roomId].setActiveDoor(wallID, doorID);
        newRoom.setActiveDoor(newWallID, newDoorID);
        sortRooms();
    }
    void sortRooms()
    {
        int inPlace = 0;
        while (_rooms.Count - inPlace > 1)
        {
            for (int i = 1; i < _rooms.Count - inPlace - 1; i++)
            {
                if ((_rooms[0].transform.position - _rooms[i].transform.position).magnitude >
                    (_rooms[0].transform.position - _rooms[i + 1].transform.position).magnitude)
                {
                    Room temp = _rooms[i];
                    _rooms[i] = _rooms[i + 1];
                    _rooms[i + 1] = temp;
                }
            }
            inPlace++;
        }
    }    

    int pseudoRandomID()
    {
        return UnityEngine.Random.Range((int)(_rooms.Count/3.0f), _rooms.Count);
    }
}
