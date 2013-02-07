using System;
using UnityEngine;
using System.Collections.Generic;

public class IntPoint : IComparable
{
	private int _X;
	private int _Y;
	
	public int x
	{
		get { return this._X; }
		set { this._X = value; }
	}
	
	public int y
	{
		get { return this._Y; }
		set { this._Y = value; }
	}
	
	public IntPoint(int x, int y)
	{
		this._X = x;
		this._Y = y;
	}
	
	public int distance( IntPoint destiny )
	{
		return (int) Mathf.Sqrt( ( (destiny.x - this.x)*(destiny.x - this.x) ) + ( (destiny.y - this.y)*(destiny.y - this.y) ) );
	}
	
	public IntPoint getRandomNSEWPoint()
	{
		List<IntPoint> possiblePoints = new List<IntPoint>();
		initializeList(ref possiblePoints);
		
		int randomPivot = UnityEngine.Random.Range(0, possiblePoints.Count);
		
		return possiblePoints[randomPivot];
						
	}
	
	public void negatePoint()
	{
		this._X *= -1;
		this._Y *= -1;
	}
	
	private void initializeList( ref List<IntPoint> list)
	{
		list.Add( new IntPoint(0,-1) );
		list.Add( new IntPoint(0,1) );
		list.Add( new IntPoint(-1,0) );
		list.Add( new IntPoint(1,0) );
		
		//shuffling the list
		
		int current = list.Count;
		int pivot = 0;
		IntPoint aux;
		
		while(current > 1)
		{
			current--;
			pivot = UnityEngine.Random.Range(0, current);
			aux = list[pivot];
			list.Insert (pivot, list[current]);
			list.Insert (current, aux);
		}
	}
	
	public override string ToString()
	{
		return "(" + this.x.ToString() + ", " + this.y.ToString() + ")";
	}
	
	public int CompareTo(object obj)
	{
		if(obj == null) return 1;
		
		IntPoint point = obj as IntPoint;
		
		if (this.x == point.x)
    	{
        	return this.y - point.y;
    	}
    	else
    	{
        	return this.x - point.x;
    	}
	}
	
	public static IntPoint operator +(IntPoint point1, IntPoint point2)
	{
		return new IntPoint(point1.x + point2.x, point1.y + point2.y);
	}
}
