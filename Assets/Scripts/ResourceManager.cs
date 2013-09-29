using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {
	
	public List<Texture2D> Textures;
	public List<AudioClip> Sounds;
	public List<Transform> Traps;
	
	public List<Transform> TrapRooms;
	public List<Transform> InitialRooms;
	public List<Transform> FinalRooms;

		
	void Awake()
	{
		if(TrapRooms == null) Debug.LogError("There are no TrapRoom Prefabs assigned to the Room Manager!");
		if(InitialRooms == null) Debug.LogError("There are no InitialRoom Prefabs assigned to the Room Manager!");
		if(FinalRooms == null) Debug.LogError("There are no FinalRoom Prefabs assigned to the Room Manager!");
		if(Traps == null) Debug.LogError("There are no Trap Prefabs assigned to the Room Manager!");
		
	}
	
	public Transform getRandomInitialRoom()
	{
		int i = Random.Range(0, InitialRooms.Count);
		return InitialRooms[i];
	}
	
	public Transform getRandomFinalRoom()
	{
		int i = Random.Range(0, FinalRooms.Count);
		return FinalRooms[i];
	}
				
	public Transform getRandomTrapRoom()
	{
		int i = Random.Range(0, TrapRooms.Count);
		return TrapRooms[i];
	}
	
	public Transform getRandomTrap()
	{
		int i = Random.Range(0, Traps.Count);
		return Traps[i];
	}

	public Texture2D getTextureByName(string textureName)
	{
		foreach(Texture2D texture in Textures)
		{
			if(texture.name == textureName)
				return texture;
		}

		return null;
	}

	public Texture2D getTextureByIndex(int idx)
	{
		return Textures[idx];
	}
}
