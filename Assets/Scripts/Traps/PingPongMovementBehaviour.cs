using UnityEngine;
using System.Collections;

public class PingPongMovementBehaviour : MonoBehaviour{

	
	public Transform pointA;
	public Transform pointB;	
	public float speed;
	public float movementTime;
	
	
	IEnumerator Start(){	
		while (true) {
		    yield return StartCoroutine(MoveObject(transform, pointA.position, pointB.position, movementTime));
			
		    yield return StartCoroutine(MoveObject(transform, pointB.position, pointA.position, movementTime));
		}
    }
    
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time){
		var i= 0.0f;
		var rate= 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
    }
}