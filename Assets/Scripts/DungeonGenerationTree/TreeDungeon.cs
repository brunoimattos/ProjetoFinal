using UnityEngine;
using System.Collections;

public class TreeDungeon : MonoSingleton<TreeDungeon> 
{
	// Dungeon Rooms
	public int DUNGEON_SIZE_X = 10;
	public int DUNGEON_SIZE_Y = 10;
	
	// Size of 3D Model Prefab in World Space
	public int ROOM_SIZE_X = 14; 
	public int ROOM_SIZE_Z = 9;
	
	// Demo Room Prefab
	//public GameObject RoomBasicPrefab; 
	
	// Room structure
	public Room[,] rooms;
	
	private GameObject initialRoom;
	
	private int nRooms = 0;
	
	private ResourceManager resourceApi;
	
	private GameObject marty;
	
	public override void Init () 
	{
		resourceApi = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
		
		marty = GameObject.FindGameObjectWithTag("Player");
		
		
		GenerateDungeon();
		GenerateGameRooms();
		
		setPlayerAndCamera();
	}
	
	void setPlayerAndCamera()
	{
		Camera.main.transform.position = new Vector3(initialRoom.transform.position.x, 10.0f, initialRoom.transform.position.z);
		
		//FIXME: Tirar o hard coded.
		marty.transform.position = new Vector3(initialRoom.transform.position.x, initialRoom.transform.position.y + 1.1f, initialRoom.transform.position.z);
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			RegenerateDungeon();	
		}
	}
	
	public void RegenerateDungeon()
	{
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
		{
			Destroy(room);
		}
		nRooms = 0;
		GenerateDungeon();
		GenerateGameRooms();
	}
	
	public void GenerateDungeon()
	{
		// Create room structure
		rooms = new Room[DUNGEON_SIZE_X,DUNGEON_SIZE_Y];
		
		// Create our first room at a random position
		int roomX = Random.Range (0, DUNGEON_SIZE_X);
		int roomY = Random.Range (0, DUNGEON_SIZE_Y);
		
		Room firstRoom = AddRoom(null, roomX,roomY); // null parent because it's the first node
		
		// Generate childrens
		firstRoom.GenerateChildren();
		
		Debug.Log("# rooms: " + nRooms);
		
		if((nRooms < 5) || (nRooms > (int)(0.8f * DUNGEON_SIZE_X * DUNGEON_SIZE_Y)))
		{
			Debug.Log("Too much or Too many Rooms: " + nRooms);
			nRooms = 0;
			GenerateDungeon();
		}
			
	}
	
	void GenerateGameRooms()
	{
		// For each room in our matrix generate a 3D Model from Prefab
		foreach (Room room in rooms)
		{
			if (room == null) continue;
			
			// Real world position
			float worldX = room.x * ROOM_SIZE_X;
			float worldZ = room.y * ROOM_SIZE_Z;
			
			Transform instRoom = resourceApi.getRandomRegularRoom();
			
			//Debug.Log("Nome sala: " + instRoom.name);
			
			Transform g = GameObject.Instantiate(instRoom, new Vector3(worldX,0,worldZ),Quaternion.identity) as Transform;
			
			// Add the room info to the GameObject main script
			GameRoom gameRoom = g.gameObject.GetComponent<GameRoom>();
			gameRoom.room = room;
			
			if (room.IsFirstNode()) 
			{
				initialRoom = g.gameObject;
				g.name = "Initial Room";
			}
			else g.name = "Room " + room.x + " " + room.y;
		}
	}
	
	// Helper Methods
	
	
	public Room AddRoom(Room parent, int x, int y)
	{
		Room room = new Room(parent, x, y);
		rooms[x,y] = room;
		nRooms++;
		return room;
	}	
}
