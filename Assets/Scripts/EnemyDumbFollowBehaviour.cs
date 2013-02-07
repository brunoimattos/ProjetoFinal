using UnityEngine;
using System.Collections;

public class EnemyDumbFollowBehaviour : MonoBehaviour {

	
	public Transform target;
	public float movingSpeed;
	
	private CharacterController enemyController;
		
	void Start () {
		enemyController = GetComponent<CharacterController>();
		
		if (enemyController == null){
			Debug.Log("Character controller not found in " + this.GetType());
		}
	}
	
	void Update () {
		doMovement();
	}
	
	void doMovement(){
		
		this.transform.LookAt(target);		
		this.enemyController.SimpleMove(this.transform.forward * movingSpeed);
		
	}
}
