using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitObjectMovementManager : MonoBehaviour {

	public List<Transform> circuit_nodes;
	
	void Start () {
		if(circuit_nodes.Count == 0)
		{
			Debug.LogError("Please add some nodes to the circuit!");
		}
		
		foreach(Transform t in circuit_nodes)
		{
			if(t == null)
			{
				Debug.LogError("Please add some non-empty nodes to the circuit!");	
			}	
		}
	}
	
	public int get_starting_index(Transform starting_node)
	{
		return circuit_nodes.IndexOf(starting_node);
	}
	
	
	public Vector3 get_movement_direction(int current_idx)
	{
		Transform current_node, next_node;
		
		current_node = circuit_nodes[current_idx % circuit_nodes.Count];
		current_idx++;
		next_node = circuit_nodes[current_idx % circuit_nodes.Count];
		
		return (next_node.position - current_node.position).normalized;
	}
	
	public Vector3 get_next_stop_position(int current_idx)
	{
		return circuit_nodes[(++current_idx) % circuit_nodes.Count].position;
	}
}
