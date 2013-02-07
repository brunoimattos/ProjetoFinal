using UnityEngine;
using System.Collections;

public class CreateParticleEmitter : MonoBehaviour {

	public GameObject rockEmitter;
	
	
	void Start () {
	
	}
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			Instantiate(rockEmitter, (Camera.mainCamera.transform.position + Camera.main.transform.forward * 4), Quaternion.identity);

		}
	}
}
