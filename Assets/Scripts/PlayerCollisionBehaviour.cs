using UnityEngine;
using System.Collections;

public class PlayerCollisionBehaviour : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{	
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
	}
}

