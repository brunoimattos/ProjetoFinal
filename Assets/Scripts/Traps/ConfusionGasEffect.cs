using UnityEngine;
using System.Collections;

public class ConfusionGasEffect : MonoBehaviour 
{
	public float confuseTime = 3.0f;
	private PlayerMovement playerApi;
	
	void Start()
	{
		if(confuseTime < 0.0f)
		{
			Debug.LogError("Please set confuseTime");
		}
		
		playerApi = GetComponent<PlayerMovement>();
		
		if(playerApi == null)
		{
			Debug.LogError("Eitcha lele!");
		}
		
		StartCoroutine(doConfusion(confuseTime, playerApi));
	}
	
	IEnumerator doConfusion(float time, PlayerMovement playerApi)
	{
		playerApi.setConfusion(true);
		
		yield return new WaitForSeconds(time);
		
		playerApi.setConfusion(false);
		Destroy(this);
		
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(500, 10, 100, 25), "To confuso!");
	}
}
