using UnityEngine;
using System.Collections;

public class TurretBehaviour : MonoBehaviour
{
	
	public float shot_cooldown = -1.0f;
	private Transform marty;
	
	void Start()
	{
		if(shot_cooldown < 0.0f)
		{
			Debug.LogError("Please set the cooldown time between shots.");
		}
		
		marty = GameObject.FindGameObjectWithTag("Player").transform;
		
		if(marty == null)
		{
			Debug.LogError("Could not find Marty :(");
		}
	}
	

	void Update()
	{	
		if(marty != null)
		{
			/*
			Vector3 lookPosition = marty.position - transform.position;
						
			Quaternion rotation = Quaternion.LookRotation(lookPosition);
			//rotation.x = 0;
			//rotation.y = 0;
			//rotation.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5.0f);
			*/
			
			transform.LookAt(marty.position);
		}
		
	}
}
