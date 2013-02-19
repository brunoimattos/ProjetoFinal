using System;
using UnityEngine;
using System.Collections.Generic;

public class ConcreteRoom : IEquatable<ConcreteRoom>

{
	private Vector2 _Position;
	
	private List<Vector2> _Neighbors;
	
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
	
	public List<Vector2> neighbors
	{
		get{ return this._Neighbors; }
	}
	
	public ConcreteRoom(int x, int y, int width, int height)
	{
		this._Position = new Vector2(x, y);
			
		initializeNeighbors(width, height);
	}
	
	private void initializeNeighbors(int width, int height)
	{
		_Neighbors = new List<Vector2>();
		
		//West Neighbor
		if(this._Position.x > 0)
			_Neighbors.Add(this._Position + new Vector2(-1, 0));
		
		//East Neighbor
		if(this._Position.x < width - 1)
			_Neighbors.Add(this._Position + new Vector2(1, 0));
		
		//North Neighbor
		if(this._Position.y > 0)
			_Neighbors.Add(this._Position + new Vector2(0, -1));
		
		//South Neighbor
		if(this._Position.y < height - 1)
			_Neighbors.Add(this._Position +  new Vector2(0, 1));
	}
	
	
	public bool Equals(ConcreteRoom other)
	{
		if(this._Position.x == other.x && this._Position.y == other.y)
		{
			return true;
		}
		
		return false;
	}

}
