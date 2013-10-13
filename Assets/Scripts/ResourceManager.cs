using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {
	
	public List<Texture2D> Textures;
	public List<AudioClip> Sounds;

	
	public List<Transform> InitialRooms;
	public List<Transform> TrapRooms;
	public List<Transform> FinalRooms;
	
	public List<Transform> traps_0100;
	public List<Transform> traps_1000;
	public List<Transform> traps_1100;
	public List<Transform> traps_1010;
	public List<Transform> traps_0101;
	public List<Transform> traps_1011;
	public List<Transform> traps_0111;

	private Dictionary<string, List<Transform>> TrapSet;
	private Dictionary<string, string> FlipMapping;
		
	void Awake()
	{
		if(TrapRooms == null) Debug.LogError("There are no TrapRoom Prefabs assigned to the Room Manager!");
		if(InitialRooms == null) Debug.LogError("There are no InitialRoom Prefabs assigned to the Room Manager!");
		if(FinalRooms == null) Debug.LogError("There are no FinalRoom Prefabs assigned to the Room Manager!");

		/* Dicionario que relaciona uma configuraçao de porta com as traps criadas para aquela configuraçao. */
		TrapSet = new Dictionary<string, List<Transform>>();
		
		/* A configuraçao das portas eh no padrao NLSO */
		TrapSet.Add("0100", traps_0100);
		TrapSet.Add("1000", traps_1000);
		TrapSet.Add("1100", traps_1100);
		TrapSet.Add("1010", traps_1010);
		TrapSet.Add("0101", traps_0101);
		TrapSet.Add("1011", traps_1011);
		TrapSet.Add("0111", traps_0111);
		
		/* Dicionario que relaciona os flips das traps. */
		FlipMapping = new Dictionary<string, string>();
		
		FlipMapping.Add("0001", "0100.01");
		FlipMapping.Add("0010", "1000.10");
		FlipMapping.Add("0110", "1100.10");
		FlipMapping.Add("0011", "1100.11");
		FlipMapping.Add("1001", "1100.01");
		FlipMapping.Add("1110", "1011.01");
		FlipMapping.Add("1101", "0111.10");
		
	}
	
	public Transform getRandomInitialRoom()
	{
		int i = UnityEngine.Random.Range(0, InitialRooms.Count);
		return InitialRooms[i];
	}
	
	public Transform getRandomFinalRoom()
	{
		int i = UnityEngine.Random.Range(0, FinalRooms.Count);
		return FinalRooms[i];
	}
				
	public Transform getRandomTrapRoom()
	{
		int i = UnityEngine.Random.Range(0, TrapRooms.Count);
		return TrapRooms[i];
	}
	
	public Transform getRandomTrap(string doorConfig, ref Vector3 flipping)
	{

		if(FlipMapping.ContainsKey(doorConfig))
		{			
			string instructions = FlipMapping[doorConfig];
			
			string[] parts = instructions.Split('.');
			
			// parts[0] contem uma configuraçao "primordial" de portas.
			//Transform trap = getRandomTrap(parts[0]);
			Transform trap = GetRandomItemInList(TrapSet[parts[0]]);
			
			flipping = new Vector3(1, 1, 1);

			// parts[1] contem instrucoes sobre inverter os sinais das posiçoes X e Z de um transform.
			if(parts[1][0] == '1')
			{
				flipping = new Vector3(1, 1, -1);
			}
			
			if(parts[1][1] == '1')
			{
				flipping = new Vector3(-1, 1, 1);
			}
			
			return trap;
			
		}
		flipping = new Vector3(1, 1, 1);
		
		return GetRandomItemInList(TrapSet[doorConfig]);
	}
	
	private Transform GetRandomItemInList(List<Transform> list)
	{
		int i = UnityEngine.Random.Range(0, list.Count);
		return list[i];
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
