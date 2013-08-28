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
	
	private DungeonFactory dFactory;
	private Dungeon dun;
	
	void Awake()
	{
		//This is ugly. 
		//This is in awake so it won't lose to the start in LevelSetup.
		//Rethink!
		dFactory = new DungeonFactory();
	}
	
	//Uses the ConcreteRoom to spawn the Rooms with their own Room Prefab
	void spawnRooms(List<ConcreteRoom> createdRooms)
	{
		Transform roomPrefab;
		Transform concreteRoom;
		Transform leftWallPrefab;
		Transform concreteWall;
		Vector3	leftWallPosition;
		Vector3 instPosition;
		
		foreach(ConcreteRoom room in createdRooms)
		{
			roomPrefab = room.getRoomPrefab();
			instPosition = new Vector3(roomPrefab.localScale.x * room.x, roomPrefab.localScale.y * room.y, 0);
			concreteRoom = GameObject.Instantiate(roomPrefab, instPosition, Quaternion.identity) as Transform;
			
			leftWallPrefab = room.getWallPrefab();
			leftWallPosition = new Vector3(roomPrefab.localScale.x * room.x, roomPrefab.localScale.y * room.y, 0);
			concreteWall = GameObject.Instantiate(leftWallPrefab, leftWallPosition, Quaternion.identity) as Transform;
		}	
		
		if(pivotPrefab != null)
		{
			instPosition = new Vector3(0, 0, 0);
			concreteRoom = GameObject.Instantiate(pivotPrefab, instPosition, Quaternion.identity) as Transform;
		}
		
		
	}
	
	void clearDungeonMap()
	{		
		GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
		
		foreach(GameObject room in rooms)
		{
			GameObject.Destroy(room);
		}
	}
	
	public Dungeon generateNewDungeon()
	{
		
		clearDungeonMap();
		
		dun = dFactory.createDungeon(gridWidth, gridHeight, linearProb);
		
		foreach(ConcreteRoom cRoom in dun.getDungeonRooms())
		{
			
			cRoom.setRoomPrefab(roomManagerScript);
			cRoom.setWallPrefab(roomManagerScript);
		}
		
		spawnRooms(dun.getDungeonRooms());
		
		return dun;
	}
	
	public Dungeon MakeDungeon()
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
		
		dun = this.dFactory.createDungeon(gridWidth, gridHeight, linearProb);
		
		List<ConcreteRoom> rooms = dun.getDungeonRooms();
				
		foreach(ConcreteRoom cRoom in dun.getDungeonRooms())
		{	
			cRoom.setRoomPrefab(roomManagerScript);
			cRoom.setWallPrefab(roomManagerScript);
		}
		
		spawnRooms(dun.getDungeonRooms());
		
		return dun;
	}


}
