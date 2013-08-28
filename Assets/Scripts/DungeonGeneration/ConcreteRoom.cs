using System;
using UnityEngine;
using System.Collections.Generic;

public class ConcreteRoom : IEquatable<ConcreteRoom>

{
	protected Vector2 _Position;
	
	protected List<ConcreteRoom> _Neighbors;
	
	protected Transform roomPrefab;
	
	protected Transform wallPrefab;
		
	protected RoomWall leftWall = new RoomWall();
	protected RoomWall rightWall = new RoomWall();
	protected RoomWall topWall = new RoomWall();
	protected RoomWall bottomWall = new RoomWall();
	
	protected void setRoomWalls(int RoomX, int RoomY, int RoomWidth, int RoomHeight)
	{
		this.leftWall.Width = 1.0f;
		this.leftWall.Height = 1.0f;
		this.leftWall.Depth = RoomHeight;
		this.leftWall.x = RoomX - (RoomWidth / 2) - (this.leftWall.Width / 2);
		this.leftWall.y = 0.1f;
		this.leftWall.z = RoomY;
		
		this.rightWall.Width = 1.0f;
		this.rightWall.Height = 1.0f;
		this.rightWall.Depth = RoomHeight;
		this.rightWall.x = RoomX + (RoomWidth / 2) + (this.leftWall.Width / 2);
		this.rightWall.y = 0.1f;
		this.rightWall.z = RoomY;
		
		this.topWall.Width = RoomWidth;
		this.topWall.Height = 1.0f;
		this.topWall.Depth = 1.0f;
		this.topWall.x = RoomX;
		this.topWall.y = 0.1f;
		this.topWall.z = RoomY + (RoomHeight / 2) + (this.leftWall.Height / 2);
		
		this.bottomWall.Width = RoomWidth;
		this.bottomWall.Height = 1.0f;
		this.bottomWall.Depth = 1.0f;
		this.bottomWall.x = RoomX;
		this.bottomWall.y = 0.1f;
		this.bottomWall.z = RoomY - (RoomHeight / 2) - (this.leftWall.Height / 2);
	}
	
	public int x
	{
		get { return (int) this._Position.x; }
		set { this._Position.x = value; }
	}
	
	public int y
	{
		get { return (int) this._Position.y; }
		set { this._Position.y = value; }
	}
	
	public List<ConcreteRoom> neighbors
	{
		get{ return _Neighbors;}
	}
	
	public ConcreteRoom(int x, int y, int width, int height)
	{
		this._Position = new Vector2(x, y);
		setRoomWalls(x, y, 10,10);
		//initializeNeighbors(width, height);
	}
	
	public virtual void setRoomPrefab(RoomManager roomManagerScript)
	{
		this.roomPrefab = roomManagerScript.getRandomRegularRoom();
	}
	
	public Transform getRoomPrefab()
	{
		return this.roomPrefab;
	}
	
	public virtual void setWallPrefab(RoomManager roomManagerScript)
	{
		this.wallPrefab = roomManagerScript.getRoomWall();
	}
	
	public Transform getWallPrefab()
	{
		this.wallPrefab.localScale = this.leftWall.Measures;
		this.wallPrefab.localPosition = this.leftWall.Position;
		
		return this.wallPrefab;
	}
	
	public bool Equals(ConcreteRoom other)
	{
		if(this._Position.x == other.x && this._Position.y == other.y)
		{
			return true;
		}
		
		return false;
	}
	
	public override string ToString ()
	{
		 return "X: " + this.x + " Y: " + this.y;
	}

}
