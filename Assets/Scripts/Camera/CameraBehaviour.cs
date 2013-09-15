using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public float cameraHeight = 9.57f;
		
	public void setInitialPosition(Vector3 toPosition)
	{
		this.transform.position = new Vector3(toPosition.x, cameraHeight, toPosition.z );
	}
	
	public void snapToPosition(Vector3 toPosition)
	{
		this.transform.position = toPosition;
	}
}
