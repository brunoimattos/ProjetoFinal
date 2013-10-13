using UnityEngine;
using System.Collections;

public class GameRoom : MonoBehaviour {
	
	public GameObject doorWest, doorEast, doorNorth, doorSouth;
	public Room room;
	
	/* A configuraçao das portas eh no padrao NLSO */
	private string doorConfig = "NLSO";
	
	void Start() 
	{
		// Remove walls if connected
		if (room.IsConnectedTo(room.GetLeft()))
		{
			//doorWest.SetActive(false);
			doorWest.collider.isTrigger = true;
			doorWest.renderer.enabled = false;
			
		}
		else
		{
			doorWest.SetActive(true);	
		}
		
		if (room.IsConnectedTo(room.GetRight()))
		{
			//doorEast.SetActive(false);
			doorEast.collider.isTrigger = true;
			doorEast.renderer.enabled = false;
		}
		else
		{
			doorEast.SetActive(true);
		}
		
		if (room.IsConnectedTo(room.GetTop()))
		{
			//doorNorth.SetActive(false);
			doorNorth.collider.isTrigger = true;
			doorNorth.renderer.enabled = false;
		}
		else
		{
			doorNorth.SetActive(true);
		}
		
		if (room.IsConnectedTo(room.GetBottom()))
		{
			//doorSouth.SetActive(false);
			doorSouth.collider.isTrigger = true;
			doorSouth.renderer.enabled = false;
		}
		else
		{
			doorSouth.SetActive(true);
		}
	}
	
	void Update () 
	{
		
	}
	
	public void createDoorConfig()
	{
		if (room.IsConnectedTo(room.GetLeft()))
		{
			doorConfig = doorConfig.Replace("O", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("O", "0");
		}
		
		if (room.IsConnectedTo(room.GetRight()))
		{
			doorConfig = doorConfig.Replace("L", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("L", "0");
		}
		
		if (room.IsConnectedTo(room.GetTop()))
		{
			doorConfig = doorConfig.Replace("N", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("N", "0");
		}
		
		if (room.IsConnectedTo(room.GetBottom()))
		{
			doorConfig = doorConfig.Replace("S", "1");
		}
		else
		{
			doorConfig = doorConfig.Replace("S", "0");
		}
	}
	
	public string getDoorConfig()
	{
		return this.doorConfig;
	}
}
