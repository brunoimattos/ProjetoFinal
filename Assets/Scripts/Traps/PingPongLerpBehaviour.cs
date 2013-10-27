using UnityEngine;
using System.Collections;

public class PingPongLerpBehaviour : MonoBehaviour {
	
	public Vector3 _fromPosition;
	public Vector3 _toPosition;
 	
    IEnumerator Start()
    {

		_fromPosition = transform.position;
		_toPosition = _fromPosition + Vector3.forward * 0.5f;
		
		while (true) {
		    yield return StartCoroutine(MoveObject(transform, _fromPosition, _toPosition, 0.2f));
			yield return new WaitForSeconds(1.0f);
		    yield return StartCoroutine(MoveObject(transform, _toPosition, _fromPosition, 2.0f));
		}
    }
 
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
		var i= 0.0f;
		var rate= 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
    }
	
}

