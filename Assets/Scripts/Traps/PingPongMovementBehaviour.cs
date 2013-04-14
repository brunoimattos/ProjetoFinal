using UnityEngine;
using System.Collections;

public class PingPongMovementBehaviour : MonoBehaviour
{

	
	public Transform pointA;
	public Transform pointB;	
	public float speed;
	public float movementTime;
	
	private Hashtable itweenParams;
		
	void Start()
	{
		if(pointA == null || pointB == null)
			Debug.LogError("Please assign points for PingPong movement!");	
		
		this.transform.position = pointA.position;
		
		itweenParams = new Hashtable();
		
		itweenParams.Add("speed", speed);
		itweenParams.Add("x", pointB.transform.position.x);
		itweenParams.Add("y", pointB.transform.position.y);
		itweenParams.Add("z", pointB.transform.position.z);
		itweenParams.Add("easetype", iTween.EaseType.linear);
		itweenParams.Add("looptype", iTween.LoopType.pingPong);
		
		iTween.MoveTo(this.transform.gameObject, itweenParams);
		
		//StartCoroutine(moveToB(speed, movementTime));
	}
	
	IEnumerator moveToB(float speed, float movementTime)
	{
		yield return new WaitForSeconds(movementTime);
		
		//StartCoroutine(moveToA(speed, movementTime));
	}
	
	IEnumerator moveToA(float speed, float movementTime)
	{
		
		itweenParams["x"] = pointA.transform.position.x;
		itweenParams["y"] = pointA.transform.position.y;
		itweenParams["z"] = pointA.transform.position.z;
			
		
		iTween.MoveTo(this.transform.gameObject, itweenParams);
		yield return new WaitForSeconds(movementTime);
		
		StartCoroutine(moveToB(speed, movementTime));
	}
	
	
	
}
