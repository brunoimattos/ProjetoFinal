using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{

	public List<Transform> prefabRooms;
		
	void Awake()
	{
		if(prefabRooms == null) Debug.LogError("There are no Room Prefabs assigned to the Room Manager!");
		
	}
				
	public Transform getRandomRoomTransform()
	{
		int i = Random.Range(0, prefabRooms.Count);
		
		return prefabRooms[i];
	}
	
	public Transform getRoomTransform(int index)
	{
		return prefabRooms[index];
	}
}
