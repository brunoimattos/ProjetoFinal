using UnityEngine;
using System.Collections;

public class CameraFollowBehaviour : MonoBehaviour {

	public float cameraHeight = 1.0f;
	
	public Transform cameraFocus = null;
		
	void Start()
	{
		if(cameraHeight < 0.0f)
			Debug.LogError("Please set the camera height!");
		
		if(cameraFocus == null)
			Debug.LogError("Please set the camera focus!");
		
	}
	
	void LateUpdate()
	{
		doFollow();
	}
	
	void doFollow()
	{
		this.transform.position = new Vector3(this.cameraFocus.position.x, this.cameraFocus.position.y + this.cameraHeight, this.cameraFocus.position.z);
	}
}
