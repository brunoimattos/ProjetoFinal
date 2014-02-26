using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public GameObject mainPanelObject;
    public GameObject aboutPanelObject;
	
	public void Awake(){
		//NGUITools.SetActive(aboutPanelObject, false);
		//NGUITools.SetActive(mainPanelObject, true);
	}
	
	public void OnPlayButtonClick () {
		Application.LoadLevel("LevelGeneration");
	}
	
	public void OnAboutButtonClick () {
		NGUITools.SetActive(mainPanelObject, false);
		NGUITools.SetActive(aboutPanelObject, true);
	}
	
	public void OnBackButtonClick () {
		NGUITools.SetActive(aboutPanelObject, false);
		NGUITools.SetActive(mainPanelObject, true);	
	}
}
