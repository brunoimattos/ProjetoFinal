using UnityEngine;
using System.Collections;

public class TurretLaserBehaviour : MonoBehaviour {

	private bool startCounting;
	private Vector3 origin = Vector3.zero;
	
	public float bulletRange = -1.0f;
	
	void Awake() 
	{
		if(bulletRange < 0.0f)
		{
			Debug.LogError("Please set bulletRange.");
		}
	}
	
	
	void Update()
	{	
		if(this.origin != Vector3.zero)
		{
			if(Vector3.Distance(origin, transform.position) >= bulletRange)
			{
				Destroy(this.gameObject);
			}	
		}
		
	}
	
	public void setOrigin(Vector3 origin)
	{
		this.origin = origin;
	}
}
