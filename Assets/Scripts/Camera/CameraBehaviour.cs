using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public float cameraHeight = 9.57f;
		
	void Update()
	{
		//this.transform.position = new Vector3(0, cameraHeight, 0 );
	}
	
	public void setInitialPosition(Vector3 toPosition)
	{
		this.transform.position = new Vector3(toPosition.x, cameraHeight, toPosition.z );
	}
	
	public void snapToPosition(Vector3 toPosition)
	{
		//this.transform.position = new Vector3(cameraHeight);
	}
}
