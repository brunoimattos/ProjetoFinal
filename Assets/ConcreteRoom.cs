using UnityEngine;
using System.Collections.Generic;

public class ConcreteRoom
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
		Vector2 candidate;
		_Neighbors = new List<Vector2>();
		
		//West Neighbor
		_Neighbors.Add(this._Position + new Vector2(-1, 0));
		
		//East Neighbor
		_Neighbors.Add(this._Position + new Vector2(1, 0));
		
		//North Neighbor
		_Neighbors.Add(this._Position + new Vector2(0, -1));
		
		//South Neighbor
		_Neighbors.Add(this._Position +  new Vector2(0, 1));
		
		
		for(int i = 0; i < _Neighbors.Count; i++)
		{
			/*if(pivot.x < 0 || pivot.x >= width)
			{
				_Neighbors.Remove(pivot);
			}
			else
			{
				if(pivot.y < 0 || pivot.y >= height)
				{
					_Neighbors.Remove(pivot);
				}
			}*/
			if(_Neighbors[i].x < 0 || _Neighbors[i].x >= width)
			{
				_Neighbors.Remove(_Neighbors[i]);
			}
			else
			{
				if(_Neighbors[i].y < 0 || _Neighbors[i].y >= height)
				{
					_Neighbors.Remove(_Neighbors[i]);
				}
			}
		}
		
		
		
		
	}

}
