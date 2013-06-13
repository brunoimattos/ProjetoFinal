using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitObjectMovementBehaviour : MonoBehaviour {
	
	public Transform circuit_manager;
	public Transform start_node;
	public float movement_speed;
	
	private Vector3 movement_direction, next_stop_position;
	private int current_idx = 0;
	private CircuitObjectMovementManager circuit_manager_api;
	void Awake()
	{
		circuit_manager_api = circuit_manager.GetComponent<CircuitObjectMovementManager>();
		
		if(circuit_manager_api == null)
		{
			Debug.LogError("Circuit manager has no script attached!");
		}
	}
	
	void Start()
	{
		
		this.transform.position = start_node.position;
		current_idx = circuit_manager_api.get_starting_index(start_node);
		movement_direction = circuit_manager_api.get_movement_direction(current_idx);
	}
	
	
	void LateUpdate()
	{
		this.transform.position += movement_speed * movement_direction * Time.deltaTime;
		//this.transform.Translate(movement_speed * movement_direction * Time.deltaTime);
		next_stop_position = circuit_manager_api.get_next_stop_position(current_idx);
		
		if(Vector3.Distance(next_stop_position, this.transform.position) <= 0.35f)
		{
			this.transform.position = next_stop_position;
			//current_idx++;
			movement_direction = circuit_manager_api.get_movement_direction(++current_idx);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		/*if(col.CompareTag("CircuitPart"))
		{
			Debug.Log("Changing direction!");
			current_vertex = circuit[current_idx % circuit.Count];
			current_idx++;
			next_vertex = circuit[current_idx % circuit.Count];
			
			movement_direction = (next_vertex.position - current_vertex.position).normalized;
			Debug.Log("Going from: " + current_vertex.position + " to: " + next_vertex.position);
		}*/
	}
}
