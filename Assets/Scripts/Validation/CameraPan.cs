using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {
	Vector3 movement;
	public float speed = 1.5f;
	
	// Use this for initialization
	void Start () {
		Screen.SetResolution(1280, 800, true);
	}
	
	// Update is called once per frame
	void Update () {
		movement = Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical");
		this.transform.Translate(movement * Time.deltaTime * speed);
	}
	
	public void setInitialPosition(Vector3 toPosition)
	{
		this.transform.position = new Vector3(toPosition.x, this.transform.position.y, toPosition.z );
	}
}

