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
		GenerateGameTraps();
		
		setPlayerAndCamera();
	}
	
	void setPlayerAndCamera()
	{
		Camera.main.transform.position = new Vector3(initialRoom.transform.position.x, 10.0f, initialRoom.transform.position.z);
		
		//FIXME: Tirar o hard coded.
		marty.transform.position = new Vector3(initialRoom.transform.position.x, initialRoom.transform.position.y + 0.9f, initialRoom.transform.position.z);
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
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("InitialRoom"))
			Destroy(room);
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("TrapRoom"))
			Destroy(room);
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("FinalRoom"))
			Destroy(room);
		
		Destroy(initialRoom);
		nRooms = 0;
		
		GenerateDungeon();
		GenerateGameRooms();
		GenerateGameTraps();
		
		setPlayerAndCamera();
	}
	
	public void GenerateDungeon()
	{
		// Create room structure
		rooms = new Room[DUNGEON_SIZE_X,DUNGEON_SIZE_Y];
		
		// Create our first room at a random position
		int roomX = Random.Range (0, DUNGEON_SIZE_X);
		int roomY = Random.Range (0, DUNGEON_SIZE_Y);
		
		Room firstRoom = AddRoom(null, roomX,roomY, ROOM_SIZE_X, ROOM_SIZE_Z); // null parent (first node)
		
		// Generate childrens
		firstRoom.GenerateChildren();
		
		//Debug.Log("# rooms: " + nRooms);
		
		if((nRooms < 5) || (nRooms > (int)(0.8f * DUNGEON_SIZE_X * DUNGEON_SIZE_Y)))
		{
			nRooms = 0;
			GenerateDungeon();
		}
			
	}
	
	void GenerateGameRooms()
	{
		Transform instRoom;
		bool finalRoomCreated = false;
		string roomTag = "";
		
		// For each room in our matrix generate a 3D Model from Prefab
		foreach (Room room in rooms)
		{
			if (room == null) continue;
			
			if (room.IsFirstNode()) // if Initial Room
			{
				instRoom = resourceApi.getRandomInitialRoom();
				roomTag = "InitialRoom";
			} else if (!room.HasChildren() && !finalRoomCreated) // if Final Room
			{
				instRoom = resourceApi.getRandomFinalRoom();
				finalRoomCreated = true;
				roomTag = "FinalRoom";
			} else // if Trap Room
			{
				instRoom = resourceApi.getRandomTrapRoom();
				roomTag = "TrapRoom";
			}
			
			Transform g = GameObject.Instantiate(instRoom, new Vector3(room.worldX,0,room.worldZ),Quaternion.identity) as Transform;
			room.setRoomTransform(g);
			
			// Add the room info to the GameObject main script
			GameRoom gameRoom = g.gameObject.GetComponent<GameRoom>();

			gameRoom.room = room;
			gameRoom.createDoorConfig();
			g.tag = roomTag;
			
			if (room.IsFirstNode()) 
			{
				initialRoom = g.gameObject;
			}
			g.name = "Room: " + room.x + " " + room.y;
		}
	}
	
	public void GenerateGameTraps()
	{
		Transform instTrap;
		Transform trap;
		Room room;
		GameObject traps;
		GameRoom gameRoomApi;
		Vector3 trapFlipping = Vector3.zero;
		
		foreach(GameObject gameRoom in GameObject.FindGameObjectsWithTag("TrapRoom"))
		{
			gameRoomApi = gameRoom.gameObject.GetComponent<GameRoom>();
			room = gameRoomApi.room;
			instTrap = resourceApi.getRandomTrap(gameRoomApi.getDoorConfig(), ref trapFlipping);
			
			traps = new GameObject("Traps");
			traps.transform.parent = gameRoom.transform;
			
			trap = GameObject.Instantiate(instTrap, new Vector3(room.worldX,1.1f,room.worldZ),Quaternion.identity) as Transform;
			
			foreach(Transform t in trap)
			{
				
				t.localPosition = Vector3.Scale(t.localPosition, trapFlipping);
				
			}
			
			trap.transform.parent = traps.transform;
			
			
			trap.gameObject.SetActive(false);
		}
	}
		
	public Room AddRoom(Room parent, int x, int y, float width, float height)
	{
		Room room = new Room(parent, x, y, width, height);
		rooms[x,y] = room;
		nRooms++;
		return room;
	}	
}
