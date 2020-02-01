using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        do
        {
            spawnRoom();
            //i++;
        } while (_rooms.Count < _numRoomsInFloor && i < 2);
    }

    public List<Room> _rooms;
    public GameObject _pivot;
    public List<GameObject> _roomPrefabs;
    public int _numRoomsInFloor;

    void spawnRoom()
    {
        //pick spawnDoor
        int roomId = Random.Range(0, _rooms.Count);
        int wallID = _rooms[roomId].getPotentialWall();
        int doorID = _rooms[roomId]._walls[wallID].getPotentialDoor();

        //pick new Room
        GameObject newRoomObject = GameObject.Instantiate(_roomPrefabs[Random.Range(0, _roomPrefabs.Count)]);
        newRoomObject.transform.rotation = Quaternion.Euler(Vector3.zero);

        Room newRoom = newRoomObject.GetComponentInChildren<Room>();

        int newWallID = newRoom.getPotentialWall();
        int newDoorID = newRoom._walls[newWallID].getPotentialDoor();

        GameObject pivot = GameObject.Instantiate(_pivot);


        //move pivot to door
        pivot.transform.position = newRoom._walls[newWallID]._doors[newDoorID].transform.position;
        //make room child of pivot
        newRoomObject.transform.parent = pivot.transform;

        //place/rotate the room
        bool isColliding = false;
        for (int i = 0; i < 360; i+=90)
        {
            pivot.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (float)i, 0.0f));
            pivot.transform.position = _rooms[roomId]._walls[wallID]._doors[doorID].transform.position;
            newRoom.calculateFootPrint();
            isColliding = false;

            for (int j = 0; j < _rooms.Count && !isColliding; j++)
            {
                if(newRoom.isColliding(_rooms[j]))
                {
                    isColliding = true;
                }
            }
        }

        if(isColliding)
        {
            GameObject.Destroy(pivot);
        }
        else
        {
            _rooms.Add(newRoom);
            _rooms[roomId].setActiveDoor(wallID, doorID);
        }
    }
}
