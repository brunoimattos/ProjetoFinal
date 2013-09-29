using UnityEngine;
using System.Collections;

public class TurretBehaviour : MonoBehaviour
{
	
	public Transform turret_muzzle;
	public Transform laser_blast;
	public float reload_cooldown = -1.0f;
	public float sight_radius = -1.0f;
	public float shot_speed = -1.0f;
	public bool is_static; // If the turret is static it fires away without locking on the target.
	
	private Transform marty;
	private bool can_shoot;
	private float shot_time;
	private Transform blast;
	
	private bool can_move = true;
	
	
	void Start()
	{
		if(reload_cooldown < 0.0f)
		{
			Debug.LogError("Please set the cooldown time between shots.");
		}
		
		if(sight_radius < 0.0f)
		{
			Debug.LogError("Please set the sight radius of the turret.");
		}
		
		marty = GameObject.FindGameObjectWithTag("Player").transform;
		
		if(marty == null)
		{
			Debug.LogError("Could not find Marty :(");
		}
		
		if(turret_muzzle == null)
		{
			Debug.LogError("Could not find turret_muzzle. How am I supposed to aim without it?!");		
		}
		
		if(laser_blast == null)
		{
			Debug.LogError("No laser blast. What am I supposed to fire?!");
		}
		
		if(shot_speed < 0.0f)
		{
			Debug.LogError("Please set a shot speed!");
		}
		
		StartCoroutine(doCooldown(reload_cooldown));
	}
	

	void Update()
	{	
		
		if(marty != null)
		{
			if(is_static)
			{
				doShoot();	
			}
			else
			{
				doLockOn();
			}
		}
		
		
	}
	
	IEnumerator doCooldown(float cooldownTime)
	{
		Debug.Log("Waiting..." + cooldownTime);
		yield return new WaitForSeconds(cooldownTime);
		Debug.Log("Done Waiting...");
		can_shoot = true;
	}

	void doLockOn()
	{
		if(Vector3.Distance(marty.transform.position, turret_muzzle.transform.position) <= sight_radius)
		{
			RaycastHit hit;
			
			//Debug.DrawRay(turret_muzzle.transform.position, transform.forward * sight_radius, Color.yellow, 4);
			if(Physics.Raycast(turret_muzzle.transform.position, transform.forward, out hit, sight_radius))
			{
				doShoot();
			}
			
			this.transform.LookAt(marty.transform.position);	
			
		}
	}
	
	void doShoot()
	{
		if(can_shoot)
		{
			can_move = false;
			Debug.Log("Pew!");
			blast = GameObject.Instantiate(laser_blast, turret_muzzle.position, Quaternion.identity) as Transform;
			
			foreach(Transform child in transform)
			{
				Debug.Log("CHild name: " + child.name);
				Physics.IgnoreCollision(child.collider, blast.collider);
			}

			blast.gameObject.rigidbody.velocity = turret_muzzle.transform.forward * shot_speed;
			blast.GetComponent<TurretLaserBehaviour>().setOrigin(turret_muzzle.transform.position);
			can_shoot = false;
			
			StartCoroutine(doCooldown(reload_cooldown));		
		}
	}
}

