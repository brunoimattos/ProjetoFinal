using UnityEngine;
using System.Collections;

public class TowerAutoMoveBehaviour : MonoBehaviour {

	public float movingSpeed;
	public float movingTimer;
	public float haltTimer;
	
	private float timer;
	
	
	void Start () {
		timer = movingTimer;
	}
	
	
	void Update () {
		doMovement();
	}
	
	void doMovement(){
		if(timer <= 0){
			timer = movingTimer;
			
		}
		else{
			this.transform.Translate(Vector3.up * movingSpeed * Time.deltaTime);
			timer -= Time.deltaTime;
		}
	}
}
