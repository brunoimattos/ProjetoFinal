using System;
using UnityEngine;
using System.Collections.Generic;

public class ConcreteRoom : IEquatable<ConcreteRoom>

{
	protected Vector2 _Position;
	
	protected List<ConcreteRoom> _Neighbors;
	
	protected Transform roomPrefab;
	
	protected Transform leftWallPrefab;
	protected Transform rightWallPrefab;
	protected Transform topWallPrefab;
	protected Transform bottomWallPrefab;
		
	protected RoomWall leftWall = new RoomWall();
	protected RoomWall rightWall = new RoomWall();
	protected RoomWall topWall = new RoomWall();
	protected RoomWall bottomWall = new RoomWall();
	
	protected void setRoomWalls(int gridX, int gridY, int RoomWidth, int RoomHeight)
	{
		this.leftWall.Width = 1.0f;
		this.leftWall.Height = 1.0f;
		this.leftWall.Depth = RoomHeight;
		this.leftWall.x = gridX * RoomWidth - (RoomWidth/2);
		this.leftWall.y = 1.0f;
		this.leftWall.z = gridY * RoomHeight;
		
		this.rightWall.Width = 1.0f;
		this.rightWall.Height = 1.0f;
		this.rightWall.Depth = RoomHeight;
		this.rightWall.x = (gridX * RoomWidth) + (RoomWidth / 2);
		this.rightWall.y = 1.0f;
		this.rightWall.z = gridY * RoomHeight;
		
		this.topWall.Width = RoomWidth;
		this.topWall.Height = 1.0f;
		this.topWall.Depth = 1.0f;
		this.topWall.x = gridX * RoomWidth;
		this.topWall.y = 1.0f;
		this.topWall.z = (gridY * RoomHeight) + (RoomHeight / 2);
		
		this.bottomWall.Width = RoomWidth;
		this.bottomWall.Height = 1.0f;
		this.bottomWall.Depth = 1.0f;
		this.bottomWall.x = gridX * RoomWidth;
		this.bottomWall.y = 1.0f;
		this.bottomWall.z = (gridY * RoomHeight) - (RoomHeight / 2);
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
		setRoomWalls(x, y, 20, 10);
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
		this.leftWallPrefab = roomManagerScript.getRoomWall();
		this.rightWallPrefab = roomManagerScript.getRoomWall();
		this.topWallPrefab = roomManagerScript.getRoomWall();
		this.bottomWallPrefab = roomManagerScript.getRoomWall();
	}
	
	public Transform getLeftWallPrefab()
	{
		this.leftWallPrefab.localScale = this.leftWall.Measures;
		this.leftWallPrefab.position = this.leftWall.Position;
		
		return this.leftWallPrefab;
	}
	
	public Transform getRightWallPrefab()
	{
		this.rightWallPrefab.localScale = this.rightWall.Measures;
		this.rightWallPrefab.position = this.rightWall.Position;
		
		return this.rightWallPrefab;
	}
	
	public Transform getTopWallPrefab()
	{
		this.topWallPrefab.localScale = this.topWall.Measures;
		this.topWallPrefab.position = this.topWall.Position;
		
		return this.topWallPrefab;
	}
	
	public Transform getBottomWallPrefab()
	{
		this.bottomWallPrefab.localScale = this.bottomWall.Measures;
		this.bottomWallPrefab.position = this.bottomWall.Position;
		
		return this.bottomWallPrefab;
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
