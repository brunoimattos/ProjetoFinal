using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	private GameObject finalRoom;
	
	private int nRooms = 0;
	
	private ResourceManager resourceApi;
	
	private List<Room> leafRooms;

	private GameObject marty;
	
	public override void Init () 
	{
		resourceApi = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
		
		marty = GameObject.FindGameObjectWithTag("Player");

		leafRooms = new List<Room>();

		GenerateLevel();
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
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("TrapRoom"))
			Destroy(room);
		
		Destroy(initialRoom);
		Destroy(finalRoom);
		nRooms = 0;

		leafRooms.Clear();
		
		GenerateLevel();
	}
	
	public void GenerateLevel()
	{		
		GenerateDungeon();
		GenerateGameRooms();
		
		GameRoom finalGameRoom = finalRoom.GetComponent<GameRoom>();
		Transform finalRoomDoor = resourceApi.GetFinalRoomDoor();
		Vector3 finalRoomDoorPosition = Vector3.zero;
		Vector3 finalRoomDoorRotation = Vector3.zero;
		
		
		
		if (finalGameRoom.room.IsConnectedTo(finalGameRoom.room.GetTop()))
		{
			finalRoomDoorPosition = finalGameRoom.doorNorth.transform.position;
			finalRoomDoorPosition += new Vector3(0, 0, finalGameRoom.doorNorth.transform.localScale.z);
		}
		else if (finalGameRoom.room.IsConnectedTo(finalGameRoom.room.GetRight()))
		{
			finalRoomDoorRotation = new Vector3(0, -90, 0);
			finalRoomDoorPosition = finalGameRoom.doorEast.transform.position;
			finalRoomDoorPosition += new Vector3(finalGameRoom.doorEast.transform.localScale.z, 0, 0);
		}
		else if (finalGameRoom.room.IsConnectedTo(finalGameRoom.room.GetBottom()))
		{
			finalRoomDoorPosition = finalGameRoom.doorSouth.transform.position;
			finalRoomDoorPosition -= new Vector3(0, 0, finalGameRoom.doorSouth.transform.localScale.z);
		}
		else if (finalGameRoom.room.IsConnectedTo(finalGameRoom.room.GetLeft()))
		{
			finalRoomDoorRotation = new Vector3(0, 90, 0);
			finalRoomDoorPosition = finalGameRoom.doorWest.transform.position;
			finalRoomDoorPosition -= new Vector3(finalGameRoom.doorWest.transform.localScale.z, 0, 0);
		}
		
		Transform g = GameObject.Instantiate(finalRoomDoor, finalRoomDoorPosition, Quaternion.Euler(finalRoomDoorRotation)) as Transform;
		g.parent = finalRoom.transform;
		g.name = "FinalRoomDoor";
		
		GenerateGameTraps();
		PlaceKeys();
		
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
				if(!room.HasChildren())
				{
					leafRooms.Add(room);
				}
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
			
			if (g.CompareTag("InitialRoom")) 
			{
				initialRoom = g.gameObject;
			}
			if (g.CompareTag("FinalRoom"))
			{
				finalRoom = g.gameObject;
			}
			
			g.name = room.getName();
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
	
	public void PlaceKeys()
	{
		
		int idx = Random.Range(0, leafRooms.Count-1);
		
		Room currentRoom = leafRooms[idx];
		
		Transform finalRoomKey = resourceApi.GetKeyByName("FinalRoomKey");
		
		finalRoomKey = GameObject.Instantiate(finalRoomKey, new Vector3(currentRoom.worldX,1.1f,currentRoom.worldZ),Quaternion.identity) as Transform;
		
		finalRoomKey.name = "FinalRoomKey";
		/* Esta alocado aqui dentro por falta de lugar melhor. */
		finalRoomKey.parent = finalRoom.transform;
	}
		
	public Room AddRoom(Room parent, int x, int y, float width, float height)
	{
		Room room = new Room(parent, x, y, width, height);
		rooms[x,y] = room;
		nRooms++;
		return room;
	}	

}
