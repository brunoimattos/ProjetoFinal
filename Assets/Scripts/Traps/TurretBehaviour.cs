using UnityEngine;
using System.Collections;

public class TurretBehaviour : MonoBehaviour
{
	
	public Transform turret_muzzle;
	public Transform laser_blast;
	public float reload_cooldown = -1.0f;
	public float sight_radius = -1.0f;
	public bool is_static; // If the turret is static it fires away without locking on the target.
	
	private Transform marty;
	private bool can_shoot;
	private float shot_time;
	private GameObject blast;
	
	
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
	}
	

	void Update()
	{	
		if(marty != null)
		{
			/*
			Vector3 lookPosition = marty.position - transform.position;
						
			Quaternion rotation = Quaternion.LookRotation(lookPosition);
			//rotation.x = 0;
			//rotation.y = 0;
			//rotation.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5.0f);
			*/
			
			if (!is_static)
			{
				doLockOn();
			}
			else
			{
				doCooldownTimer();
			}
		}
		
	}
	
	void doLockOn()
	{
		if( marty != null && Vector3.Distance(marty.transform.position, this.transform.position) <= sight_radius)
		{
			RaycastHit hit;
			
			if(Physics.Raycast(transform.position, this.transform.forward, out hit, sight_radius))
			{
				if(hit.collider.gameObject.CompareTag("Player"))
				{
					doCooldownTimer();
				}
			}
			
			this.transform.LookAt(marty.transform.position);
		}
	}
	
	void doCooldownTimer()
	{
		if(Time.time > shot_time)
		{
			can_shoot = true;
			doShoot();
			shot_time = Time.time + reload_cooldown;
		}
		else
		{
			can_shoot = false;
		}
	}
	
	void doShoot()
	{
		if(can_shoot)
		{
			blast = GameObject.Instantiate(laser_blast, this.turret_muzzle.transform.position, this.transform.rotation) as GameObject;
			
			// FIXME: Velocidade do tiro hard coded?!
			blast.gameObject.rigidbody.velocity = this.transform.forward * 50;
			//FIXME: Criar o script LaserBehaviour
			//blast.GetComponent<LaserBehaviour>().setOrigin(turet_muzzle.transform.position);
		}
	}
}

