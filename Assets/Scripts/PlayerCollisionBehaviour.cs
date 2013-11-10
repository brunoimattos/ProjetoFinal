using UnityEngine;
using System.Collections;

public class PlayerCollisionBehaviour : MonoBehaviour
{
	Inventory inventarioApi;
	private PlayerMovement playerMovement;
	
	void Start()
	{
		inventarioApi = transform.gameObject.GetComponent<Inventory>();
		
		playerMovement = transform.gameObject.GetComponent<PlayerMovement>();
		
		if(inventarioApi == null)
			Debug.Log("");
	}
	
	
	void OnCollisionEnter(Collision col)
	{	
		switch (col.gameObject.tag) {
		case "Trap":
			handleTrapCollision();
			break;
		case "ConfuseGas":
			handleConfuseGasCollision(col);
			break;
		case "Key":
			handleKeyCollision(col);
			break;
		case "FinalDoor":
			handleFinalDoorCollision(col);
			break;
		default:
			playerMovement.setLerping(false);
			break;
		}
	}
	
	private void handleTrapCollision(){
		playerMovement.setLerping(false);
		if(Application.loadedLevelName == "TrapsWorkshop"){
			Application.LoadLevel("TrapsWorkshop");	
		}
		else{
			Application.LoadLevel("LevelGeneration");	
		}
	}
	
	private void handleConfuseGasCollision(Collision col){
		Destroy(col.gameObject);

		if (GetComponent<ConfusionGasEffect>() == null)
		{
			gameObject.AddComponent("ConfusionGasEffect");
		}
	}
	
	private void handleKeyCollision(Collision col){

		playerMovement.setLerping(false);
		inventarioApi.AddItem(col.gameObject.name);
		Destroy(col.gameObject);
		
	}
	
	private void handleFinalDoorCollision(Collision col){
		playerMovement.setLerping(false);
		if(inventarioApi.HasItem("FinalRoomKey")){
			inventarioApi.UseItem("FinalRoomKey");
			Destroy(col.gameObject);
		}
	}
}

