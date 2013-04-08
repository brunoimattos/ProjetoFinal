using UnityEngine;
using System.Collections.Generic;

public class Dungeon 
{
	private List<ConcreteRoom> _DungeonRooms;
	private int _InitialRoom;
	private int _FinalRoom;
	private int _nRooms;
	private int _Width;
	private int _Height;
		
	public int width
	{ 
		get { return this._Width; } 
		
		set { this._Width = value; }
	}
	
	public int height
	{ 
		get	{ return this._Height; } 
		
		set { this._Height = value; }
	}
	
	public Dungeon(int width, int height)
	{
		this._Width = width;
		this._Height = height;
		this._DungeonRooms = new List<ConcreteRoom>();
		this._nRooms = Random.Range((int) (0.25f * _Width * _Height), (int) (0.5f * _Width * _Height)); //is this a suitable interval??
	}
	
	public int getNRooms()
	{
		return this._nRooms;
	}
	
	public void setDungeonRooms(List<ConcreteRoom> rooms)
	{
		this._DungeonRooms = rooms;
	}
	
	public List<ConcreteRoom> getDungeonRooms()
	{
		return this._DungeonRooms;
	}
	
	public InitialRoom getInitialRoom()
	{
		return (InitialRoom) this._DungeonRooms[_InitialRoom];
	}
	
	public FinalRoom getFinalRoom()
	{
		return (FinalRoom) this._DungeonRooms[_FinalRoom];
	}
	
	public void setInitialRoom(int initialRoomIndex)
	{
		this._InitialRoom = initialRoomIndex;	
	}
	
	public void setFinalRoom(int finalRoomIndex)
	{
		this._FinalRoom = finalRoomIndex;
	}
	
	public override string ToString ()
	{
		 return "N Rooms: " + this._DungeonRooms.Count;
	}
	
	
}

