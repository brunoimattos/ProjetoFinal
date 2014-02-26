using UnityEngine;
using System.Collections;

public class StairBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Player")){
			TreeDungeon tDungeon = TreeDungeon.instance;
			tDungeon.RegenerateDungeon();
		}
	}
}
