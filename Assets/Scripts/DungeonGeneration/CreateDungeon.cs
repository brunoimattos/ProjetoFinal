using UnityEngine;
using System.Collections.Generic;

public class CreateDungeon : MonoBehaviour
{

	public int gridWidth;
	public int gridHeight;
	public float linearProb;
	
	public Transform pivotPrefab;
		
	private int[,] dungeonMap;
	private List<ConcreteRoom> createdRooms;
	private Transform roomManager;
	private RoomManager roomManagerScript;
	
	void Start()
	{
		/*
		roomManager = GameObject.FindGameObjectWithTag("RoomManager").transform;
		
		if(roomManager == null)
		{
			Debug.LogError("There is no RoomManager in the scene. Level can't be built.");	
		}
		else
		{
			roomManagerScript = roomManager.GetComponent<RoomManager>();
			
			if (roomManagerScript == null) Debug.LogError("No RoomManager script attached to the RoomManager");
		}
		
		createdRooms = new List<ConcreteRoom>();		
				
		dungeonMap = new int[gridWidth, gridHeight];
		
		generateDungeon(ref dungeonMap, gridWidth, gridHeight);
		
		//spawnRooms(dungeonMap, roomPrefab);
		spawnRooms();
		*/
	}
	
	List<Vector2> updateNeighbors(List<Vector2> candidateNeighbors, List<Vector2> newNeighbors)
	{
		List<Vector2> insertedNeighbors = new List<Vector2>();
		
		foreach(Vector2 neighbor in newNeighbors)
		{
			if(!candidateNeighbors.Contains(neighbor))
			{
				candidateNeighbors.Add(neighbor);
				insertedNeighbors.Add(neighbor);
			}
		}
		
		return insertedNeighbors;
	}
	
	private Vector2 getRandomNeighbor(List<Vector2> candidateNeighbors, List<Vector2> lastInsertedNeighbors)
	{
		Vector2 rndNeighbor;
		int rnd;	

		if(Random.Range(0, 11) >= linearProb && lastInsertedNeighbors.Count > 0){
			rnd = Random.Range(0, lastInsertedNeighbors.Count);
			rndNeighbor = lastInsertedNeighbors[rnd];
		} else {
			rnd = Random.Range(0, candidateNeighbors.Count - lastInsertedNeighbors.Count);
			rndNeighbor = candidateNeighbors[rnd];
		}
		
		candidateNeighbors.Remove(rndNeighbor);
		
		return rndNeighbor;
	}
	
	private void generateDungeon(ref int[,] map, int width, int height)
	{
		//TIRAR HARDCODED
		int nRooms = Random.Range(8, 14);
		Debug.Log("nRooms: " + nRooms);
		
		List<Vector2> lastInsertedNeighbors = new List<Vector2>();
		List<Vector2> candidateNeighbors = new List<Vector2>();
		
		Vector2 pivot;
		ConcreteRoom auxRoom = new ConcreteRoom(width/2, height/2, width, height);
		auxRoom.SetRoomPrefab(roomManagerScript.getRandomRoomTransform());
		
		map[auxRoom.x, auxRoom.y] = 1;
		
		//adding to created room list
		createdRooms.Add(auxRoom);
		
		//adding the neighbors to the neighbors list
		lastInsertedNeighbors = updateNeighbors(candidateNeighbors, auxRoom.neighbors);
		
		nRooms--;
				
		while(nRooms > 0)
		{
			pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors);	
			auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, width, height);
			
			//don't allow duplicate room creation
			while(createdRooms.IndexOf(auxRoom) >= 0)
			{
				pivot = getRandomNeighbor(candidateNeighbors, lastInsertedNeighbors);
				auxRoom = new ConcreteRoom((int)pivot.x, (int)pivot.y, width, height);
			}
			
			auxRoom.SetRoomPrefab(roomManagerScript.getRandomRoomTransform());
			
			createdRooms.Add(auxRoom);
			lastInsertedNeighbors = updateNeighbors(candidateNeighbors, auxRoom.neighbors);
			nRooms--;
			map[auxRoom.x, auxRoom.y] = 1;
		}		
	}
	
	void debugMap(int[,] map)
	{
		string line = "";
		for(int j = 0; j < gridHeight; j++)
		{
			for(int i = 0; i < gridWidth; i++)
			{
				line += map[i,j].ToString();
			}	
			Debug.Log(line);
			Debug.Log("\n");
			line = "";
		}
	}
	
	//Uses the map to spawn the Rooms with a default roomPrefab
	void spawnRooms(int[,] map, Transform roomPrefab, Transform defaultPrefab)
	{
		Transform concreteRoom;
		Vector3 instPosition;
		
		for(int j = 0; j < gridHeight; j++)
		{
			for(int i = 0; i < gridWidth; i++)
			{
				if(map[i,j] == 1)
				{
					instPosition = new Vector3(roomPrefab.localScale.x * i, roomPrefab.localScale.y * j, 0);
					concreteRoom = GameObject.Instantiate(roomPrefab, instPosition, Quaternion.identity) as Transform;
				}
				else if(map[i,j] == 0)
				{
					instPosition = new Vector3(defaultPrefab.localScale.x * i, defaultPrefab.localScale.y * j, 0);
					concreteRoom = GameObject.Instantiate(defaultPrefab, instPosition, Quaternion.identity) as Transform;
				}
			}	
		}
	}
	
	//Uses the ConcreteRoom to spawn the Rooms with their own Room Prefab
	void spawnRooms()
	{
		Transform roomPrefab;
		Transform concreteRoom;
		Vector3 instPosition;
		
		foreach(ConcreteRoom room in createdRooms)
		{
			roomPrefab = room.getRoomPrefab();
			instPosition = new Vector3(roomPrefab.localScale.x * room.x, roomPrefab.localScale.y * room.y, 0);
			concreteRoom = GameObject.Instantiate(roomPrefab, instPosition, Quaternion.identity) as Transform;
		}	
		
		if(pivotPrefab != null)
		{
			instPosition = new Vector3(0, 0, 0);
			concreteRoom = GameObject.Instantiate(pivotPrefab, instPosition, Quaternion.identity) as Transform;
		}
		
		
	}
	
	void clearDungeonMap(ref int[,] map)
	{
		map = new int[gridWidth, gridHeight];
		
		createdRooms.Clear();
		
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		
		foreach(GameObject room in rooms)
		{
			GameObject.Destroy(room);
		}
	}
	
	public List<ConcreteRoom> generateNewDungeon()
	{
		clearDungeonMap(ref dungeonMap);
		
		generateDungeon(ref dungeonMap, gridWidth, gridHeight);
		
		//spawnRooms(dungeonMap, roomPrefab, defaultPrefab);
		spawnRooms();
		
		return createdRooms;
	}
	
	public List<ConcreteRoom> MakeDungeon()
	{
		roomManager = GameObject.FindGameObjectWithTag("RoomManager").transform;
		
		if(roomManager == null)
		{
			Debug.LogError("There is no RoomManager in the scene. Level can't be built.");	
		}
		else
		{
			roomManagerScript = roomManager.GetComponent<RoomManager>();
			
			if (roomManagerScript == null) Debug.LogError("No RoomManager script attached to the RoomManager");
		}
		
		createdRooms = new List<ConcreteRoom>();		
				
		dungeonMap = new int[gridWidth, gridHeight];
		
		generateDungeon(ref dungeonMap, gridWidth, gridHeight);
		
		//spawnRooms(dungeonMap, roomPrefab, defaultPrefab);
		spawnRooms();
		
		return createdRooms;
	}


}
