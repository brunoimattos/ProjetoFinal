using UnityEngine;
using System.Collections;

public class ShowHideBehaviour : MonoBehaviour
{

	
	public float showTime;
	public float hideTime;
	public Transform showHideObject;
	
	void Start()
	{
		StartCoroutine(showObject(showTime));
	}
	
	IEnumerator showObject(float showTime)
	{
		showHideObject.renderer.enabled = true;
		showHideObject.gameObject.collider.enabled = true;
		
		yield return new WaitForSeconds(showTime);
		
		StartCoroutine(hideObject(hideTime));
	}
	
	IEnumerator hideObject(float hideTime)
	{
		showHideObject.renderer.enabled = false;
		showHideObject.gameObject.collider.enabled = false;
		yield return new WaitForSeconds(hideTime);
		
		StartCoroutine(showObject(showTime));
	}
	
	void OnEnable () {
		StartCoroutine(showObject(showTime));
	}
}
