using UnityEngine;
using System.Collections;

public class TowerCollisionBehav : MonoBehaviour {

	
	public GameObject rockEmitter;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision col){
		//Debug.Log(col.gameObject.tag + " tocou na torre!");
		
		if(col.gameObject.CompareTag("Enemy")){
			Debug.Log("Inimigo tocou na torre!");
			
			Instantiate(rockEmitter, col.contacts[0].point, Quaternion.identity);
		}
	}
}
