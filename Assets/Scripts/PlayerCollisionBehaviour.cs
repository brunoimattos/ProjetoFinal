using UnityEngine;
using System.Collections;

public class PlayerCollisionBehaviour : MonoBehaviour
{
	Inventory inventarioApi;
	
	void Start()
	{
		inventarioApi = transform.gameObject.GetComponent<Inventory>();
		
		if(inventarioApi == null)
			Debug.Log("");
	}
	
	
	void OnCollisionEnter(Collision col)
	{	
		Debug.Log("Col tag: " + col.gameObject.tag);
		
		/* The logic behind the player's collision. */
		if(col.gameObject.CompareTag("Trap"))
		{
			if(Application.loadedLevelName == "TrapsWorkshop"){
				Application.LoadLevel("TrapsWorkshop");	
			}
			else{
				Application.LoadLevel("LevelGeneration");	
			}
			
			Debug.Log("LOL, I'm dead!");	
		}
		
		if(col.gameObject.CompareTag("ConfuseGas"))
		{
			Destroy(col.gameObject);

			if (GetComponent<ConfusionGasEffect>() == null)
			{
				gameObject.AddComponent("ConfusionGasEffect");
			}
		}
		
		if(col.gameObject.CompareTag("Key"))
		{
			inventarioApi.AddItem(col.gameObject.name);
			Destroy(col.gameObject);
		}
		
		if(col.gameObject.CompareTag("FinalDoor"))
		{
			if(inventarioApi.HasItem("FinalRoomKey")){
				inventarioApi.UseItem("FinalRoomKey");
				Destroy(col.gameObject);
			}
		}
	}
}

