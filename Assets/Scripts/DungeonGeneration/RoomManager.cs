using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{

	public List<Transform> regularRooms;
	public List<Transform> initialRooms;
	public List<Transform> finalRooms;
	public Transform roomWall;
		
	void Awake()
	{
		if(regularRooms == null) Debug.LogError("There are no regularRoom Prefabs assigned to the Room Manager!");
		if(initialRooms == null) Debug.LogError("There are no initialRoom Prefabs assigned to the Room Manager!");
		if(finalRooms == null) Debug.LogError("There are no finalRoom Prefabs assigned to the Room Manager!");
		if(roomWall == null) Debug.LogError("There is no roomWall Prefab assigned to the Room Manager!");
		
	}
	
	public Transform getRandomInitialRoom()
	{
		int i = Random.Range(0, initialRooms.Count);
		return initialRooms[i];
	}
	
	public Transform getRandomFinalRoom()
	{
		int i = Random.Range(0, finalRooms.Count);
		return finalRooms[i];
	}
				
	public Transform getRandomRegularRoom()
	{
		int i = Random.Range(0, regularRooms.Count);
		return regularRooms[i];
	}
	
	public Transform getRoomWall()
	{
		return roomWall;
	}

}
