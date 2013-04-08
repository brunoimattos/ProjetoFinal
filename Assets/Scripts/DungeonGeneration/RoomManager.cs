using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{

	public List<Transform> regularRooms;
	public List<Transform> initialRooms;
	public List<Transform> finalRooms;
		
	void Awake()
	{
		if(regularRooms == null) Debug.LogError("There are no Room Prefabs assigned to the Room Manager!");
		
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

}
