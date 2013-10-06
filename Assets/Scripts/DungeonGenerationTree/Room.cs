using UnityEngine;
using System.Collections;

public class Room
{
	public int x, y;
	public Room parent;
	public Room child1, child2;
	private Transform roomTransform;
	private float width, height;
	private TreeDungeon dungeon;
	
	private static int MAX_TRIES = 2;
	
	public Room(Room _parent, int _x, int _y, float _width, float _height)
	{
		parent = _parent;
		x = _x;
		y = _y;
		width = _width;
		height = _height;
		dungeon = TreeDungeon.instance;
	}
	
	public void setRoomTransform(Transform transform)
	{
		roomTransform = transform;
	}
	
	public void setActiveTrap(bool active)
	{
		Transform traps = roomTransform.Find("Traps");
		if (traps != null)
		{
			foreach(Transform trap in traps)
			{
				trap.gameObject.SetActive(active);
			}
		}
	}
	
	public float worldX
	{
		get
		{
			return x * width;	
		}
	}
	
	public float worldZ
	{
		get
		{
			return y * height;	
		}
	}
	
	public bool IsFirstNode()
	{
		if (parent == null) return true;
		return false;
	}
	
	public bool HasChildren()
	{
		return NumChildren() > 0;
	}
	
	public int NumChildren()
	{
		int n = 0;
		if (child1 != null) n++;
		if (child2 != null) n++;
		return n;
	}
	
	public void GenerateChildren()
	{
		if (NumChildren() == 2) return;
		if (NumEmptyNeighbours() == 0) return;
		
		int dir_child_1 = GetValidDirection(1);
		if (dir_child_1 >= 0) child1 = AddChild(dir_child_1);

		int dir_child_2 = GetValidDirection(1);
		if (dir_child_1 >= 0) child2 = AddChild(dir_child_2);
		
		if (child1 != null) child1.GenerateChildren();
		if (child2 != null) child2.GenerateChildren();
	}
	
	public Room AddChild(int direction)
	{
		if (direction == 0) return dungeon.AddRoom(this,x,y+1, width, height); // Top
		if (direction == 1) return dungeon.AddRoom(this,x+1,y, width, height); // Right
		if (direction == 2) return dungeon.AddRoom(this,x,y-1, width, height); // Bottom
		if (direction == 3) return dungeon.AddRoom(this,x-1,y, width, height); // Left
		
		return null;
	}
	
	public int GetValidDirection(int num_tries)
	{
		if (num_tries > MAX_TRIES) return -1;
		
		int direction = Random.Range(0,4);
		
		switch (direction)
		{
			case 0: // Top
				if (y >= dungeon.DUNGEON_SIZE_Y - 1) return GetValidDirection(num_tries+1);
				if (GetTop() != null) return GetValidDirection(num_tries+1);	
				break;	
			case 1: // Right
				if (x >= dungeon.DUNGEON_SIZE_X - 1) return GetValidDirection(num_tries+1);
				if (GetRight() != null) return GetValidDirection(num_tries+1);
				break;
			case 2: // Bottom
				if (y == 0) return GetValidDirection(num_tries++);
				if (GetBottom() != null) return GetValidDirection(num_tries+1);
				break;
			case 3: // Left
				if (x == 0) return GetValidDirection(num_tries+1);
				if (GetLeft() != null) return GetValidDirection(num_tries+1);
				break;
		}
		
		return direction;
	}
	
	public bool IsConnectedTo(Room room)
	{
		if (room == null) return false;
		if (room.parent == this) return true;
		if (room == this.parent) return true;
		return false;
	}
	
	public Room GetRight()
	{
		int tileX = x + 1;
		if (tileX >= dungeon.DUNGEON_SIZE_X) return null;
		
		int tileY = y;
		
		return dungeon.rooms[tileX, tileY];
	}
	
	public Room GetLeft()
	{
		int tileX = x - 1;
		if (tileX < 0) return null;
		
		int tileY = y;
		return dungeon.rooms[tileX, tileY];
	}
	
	public Room GetTop()
	{
		int tileY = y + 1;
		if (tileY >= dungeon.DUNGEON_SIZE_Y) return null;
		
		int tileX = x;		
		
		return dungeon.rooms[tileX, tileY];
	}
	
	public Room GetBottom()
	{
		int tileY = y - 1;
		if (tileY < 0) return null;
		
		int tileX = x;		
		return dungeon.rooms[tileX, tileY];
	}
	
	public bool IsThereRoomsAround()
	{
		if (GetTop() != null) return true;
		if (GetBottom() != null) return true;
		if (GetLeft() != null) return true;
		if (GetRight() != null) return true;
		return false;
	}
	
	public int NumNeighbours()
	{
		int n = 0;
		if (GetTop() != null) n++;
		if (GetBottom() != null) n++;
		if (GetLeft() != null) n++;
		if (GetRight() != null) n++;
		return n;
	}
			
	public int NumEmptyNeighbours()
	{
		int n = 0;
		if (GetTop() == null && y < dungeon.DUNGEON_SIZE_Y - 1) n++;
		if (GetBottom() == null && y > 0) n++;
		if (GetLeft() == null && x > 0) n++;
		if (GetRight() == null && x < dungeon.DUNGEON_SIZE_X - 1) n++;
		return n;
	}
	
	public string ToString()
	{
		return "X: " + x + ", " + "Y:" + y;
	}
}