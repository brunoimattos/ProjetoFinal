using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class SimulationInputs : MonoBehaviour {
	
	int input_x, input_y, max_tries;
	int mouseScroll;
	
	public int ortographicScrollAmount = 5;
	
	TreeDungeon treeDungeonApi;
	
	// Use this for initialization
	void Start () {
		
		
		treeDungeonApi = (TreeDungeon) Camera.main.GetComponent<TreeDungeon>();
		
		if(treeDungeonApi == null){
			Debug.LogError("O componente <TreeDungeon> nao foi achado na camera.");
		}
		input_x = treeDungeonApi.DUNGEON_SIZE_X;
		input_y = treeDungeonApi.DUNGEON_SIZE_Y;
		// Mudar!
		max_tries = 2;
		
	}
	
	void Update(){
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + ortographicScrollAmount, 80);
 
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - ortographicScrollAmount, 10);
        }
	}
	
	void OnGUI(){
		int.TryParse(GUI.TextField(new Rect(60, 10, 50, 20), input_x.ToString(), 2), out input_x);
		int.TryParse(GUI.TextField(new Rect(60, 30, 50, 20), input_y.ToString(), 2), out input_y);
		int.TryParse(GUI.TextField(new Rect(60, 50, 50, 20), max_tries.ToString(), 2), out max_tries);
		
		GUI.Label(new Rect(10, 10, 10, 20), "X: ");
		GUI.Label(new Rect(10, 30, 10, 20), "Y: ");
		GUI.Label(new Rect(10, 50, 50, 20), "MAX_TRIES: ");
		
		int.TryParse(Regex.Replace(input_x.ToString(), @"[^0-9]", ""), out input_x);
		int.TryParse(Regex.Replace(input_y.ToString(), @"[^0-9]", ""), out input_y);
		
		if (GUI.Button(new Rect(10, 90, 100, 30), "Gerar Fase")){
			treeDungeonApi.OnGenerateClick(input_x, input_y, max_tries);
		}
            
		
	}
}
