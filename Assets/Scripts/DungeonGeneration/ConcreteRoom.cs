using System;
using UnityEngine;
using System.Collections.Generic;

public class ConcreteRoom : IEquatable<ConcreteRoom>

{
	protected Vector2 _Position;
	
	protected List<ConcreteRoom> _Neighbors;
	
	protected Transform roomPrefab;
	
	protected Transform wallPrefab;
	
	private RoomWall leftWall, rightWall, topWall, bottomWall;
	
	private void setRoomWalls(int RoomX, int RoomY, int RoomWidth, int RoomHeight)
	{
		this.leftWall.Width = 3;
		this.leftWall.Height = RoomHeight;
		this.leftWall.Depth = 3;
		this.leftWall.x = RoomX - (RoomWidth / 2) - (this.leftWall.Width / 2);
		this.leftWall.y = RoomY;
		this.leftWall.z = 0;
		
		this.rightWall.Width = 3;
		this.rightWall.Height = RoomHeight;
		this.rightWall.Depth = 3;
		this.rightWall.x = RoomX + (RoomWidth / 2) + (this.leftWall.Width / 2);
		this.rightWall.y = RoomY;
		this.rightWall.z = 0;
		
		this.topWall.Width = RoomWidth;
		this.topWall.Height = 3;
		this.topWall.Depth = 3;
		this.topWall.x = RoomX;
		this.topWall.y = RoomY + (RoomHeight / 2) + (this.leftWall.Height / 2);
		this.topWall.z = 0;
		
		this.bottomWall.Width = RoomWidth;
		this.bottomWall.Height = 3;
		this.bottomWall.Depth = 3;
		this.bottomWall.x = RoomX;
		this.bottomWall.y = RoomY - (RoomHeight / 2) - (this.leftWall.Height / 2);
		this.bottomWall.z = 0;
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
		leftWall = new RoomWall();
		rightWall = new RoomWall();
		topWall = new RoomWall();
		bottomWall = new RoomWall();
		setRoomWalls(x,y,width,height);
			
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
