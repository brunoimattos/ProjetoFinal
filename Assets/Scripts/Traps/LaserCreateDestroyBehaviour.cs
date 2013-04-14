using UnityEngine;
using System.Collections;
/// <summary>
/// Creates and Destroys a laser based on the emmiter's positions.
/// </summary>
public class LaserCreateDestroyBehaviour : MonoBehaviour
{
	public Transform createDestroyObject;
	public Transform emmiterA;
	public Transform emmiterB;
	
	public float laserOnTime;
	public float laserOffTime;
	
	private Transform createdLaser;
	private bool laserOn;
	
	void Awake()
	{
		if(createDestroyObject == null)
			Debug.LogError("Please assign object to be created!");
		
		if(emmiterA == null || emmiterB == null)
			Debug.LogError("Please assign the points of reference for creation!");
		
		this.laserOn = false;
	}
	
	void Start()
	{
		StartCoroutine(turnLaserOn(laserOnTime));
	}
	
	IEnumerator turnLaserOn(float laserOnTime)
	{
		LaserOn();
		
		yield return new WaitForSeconds(laserOnTime);
		
		StartCoroutine(turnLaserOff(laserOffTime));
	}
	
	private void LaserOn()
	{
		Vector3 mid_distance = Vector3.zero;
		Vector3 difference = Vector3.zero;
		float totalRotation = 90.0f;
		float angle;
		float distance;
		
		float t = 0.5f;
		
		if(emmiterA.position.x < emmiterB.position.x)
		{
			mid_distance = emmiterA.position * t + emmiterB.position * (1 - t);
			difference = (emmiterB.position - emmiterA.position);
		}
		else if(emmiterB.position.x < emmiterA.position.x)
		{
			mid_distance = emmiterA.position * (1 - t) + emmiterB.position * t;
			difference = (emmiterA.position - emmiterB.position);
		}
		
		distance = difference.magnitude;
		
		createdLaser = Instantiate(createDestroyObject, mid_distance, this.transform.rotation * Quaternion.Euler(0, 0, -90.0f)) as Transform;
		
		Vector3 scale = createdLaser.localScale;
		scale.y = distance / 2;
				
		createdLaser.gameObject.transform.localScale = scale;
		createdLaser.parent = this.gameObject.transform;
		
		this.laserOn = true;
	}
	
	IEnumerator turnLaserOff(float laserOffTime)
	{
		LaserOff();
		
		yield return new WaitForSeconds(laserOffTime);
		
		StartCoroutine(turnLaserOn(laserOnTime));
	}
	
	private void LaserOff()
	{
		Destroy(createdLaser.gameObject);
		
		this.laserOn = false;
	}
}
