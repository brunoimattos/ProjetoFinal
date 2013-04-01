using UnityEngine;
using System.Collections.Generic;

public class Dungeon 
{
	private List<ConcreteRoom> dungeonRooms;
	private int initialRoom;
	private int finalRoom;
	private int nRooms;
	private int width;
	private int height;
	
	public Dungeon(int width, int height)
	{
		this.width = width;
		this.height = height;
		this.dungeonRooms = new List<ConcreteRoom>();
		this.nRooms = Random.Range((int) (0.25f * width*height), (int) (0.5f * width*height)); //is this a suitable interval??
		Debug.Log("NROOMS: " + nRooms);
	}
	
	public int getNRooms()
	{
		return this.nRooms;
	}
	
	public void setDungeonRooms(List<ConcreteRoom> rooms)
	{
		this.dungeonRooms = rooms;
	}
	
	public List<ConcreteRoom> getDungeonRooms()
	{
		return this.dungeonRooms;
	}
	
	public InitialRoom getInitialRoom()
	{
		return (InitialRoom) this.dungeonRooms[initialRoom];
	}
	
	public void setInitialRoom(int initialRoomIndex)
	{
		this.initialRoom = initialRoomIndex;	
	}
	
	public override string ToString ()
	{
		 return "N Rooms: " + this.dungeonRooms.Count;
	}
	
	
}

