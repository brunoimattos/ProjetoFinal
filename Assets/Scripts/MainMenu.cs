using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject mainPanelObject;
    public GameObject aboutPanelObject;
	public GameObject optionsPanelObject;
	
	public void Awake(){
		NGUITools.SetActive(aboutPanelObject, false);
		NGUITools.SetActive(optionsPanelObject, false);
		NGUITools.SetActive(mainPanelObject, true);
	}
	
	public void OnPlayButtonClick () {
		Application.LoadLevel("LevelGeneration");
	}
	
	public void OnAboutButtonClick () {
		Debug.Log("About Click.");
		NGUITools.SetActive(mainPanelObject, false);
		NGUITools.SetActive(aboutPanelObject, true);
	}

	public void OnOptionsButtonClick () {
		Debug.Log("Options Click.");
		NGUITools.SetActive(mainPanelObject, false);
		NGUITools.SetActive(optionsPanelObject, true);
	}
	
	public void OnBackButtonClick () {
		Debug.Log("Back Click.");
		NGUITools.SetActive(aboutPanelObject, false);
		NGUITools.SetActive(optionsPanelObject, false);
		NGUITools.SetActive(mainPanelObject, true);	
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
