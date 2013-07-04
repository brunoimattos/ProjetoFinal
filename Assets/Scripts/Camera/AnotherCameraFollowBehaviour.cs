using UnityEngine;
using System.Collections;

public class AnotherCameraFollowBehaviour : MonoBehaviour
{

	public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
 	public float camera_height = -1.0f;
	
	private Vector3 player_start_pos;
	
	void Start()
	{
		if(camera_height < 0.0f)
		{
			Debug.LogError("Please set camera_height!");
		}
		
		//player_start_pos = camera.WorldToViewportPoint(target.position);
		//transform.position -= new Vector3(0, 0, camera_height);
	}
	
	
    void Update() 
    {
       if (target)
       {
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position + new Vector3(0, camera_height, 0) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0.5f)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
		
		// Mudar para Pinch & Zoom :)
		if(Input.GetKeyDown(KeyCode.PageDown))
		{
			camera.transform.position -= new Vector3(0, 0, 5);
		}
		
		if(Input.GetKeyDown(KeyCode.PageUp))
		{
			camera.transform.position += new Vector3(0, 0, 5);
		}
 
    }
}
