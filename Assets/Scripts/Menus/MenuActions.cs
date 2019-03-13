using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager used for general actions inside the menu scene, like opening/closing windows and panels, and changing volume.
/// Is a singleton
/// </summary>
public class MenuActions : MonoBehaviour {

	public static MenuActions instance;
	[SerializeField] public static MenuCity MenuAnimator;

	[SerializeField] private GameObject ActivePanel;
	private GameObject ActiveWindow;

	public void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	public void ChangePanel(GameObject panel){
		ActivePanel?.SetActive(false);
		panel.SetActive(true);
		ActivePanel = panel;
	}

	public void TriggerWindow(GameObject window){
		ActiveWindow?.SetActive(false);
		window.SetActive(true);
		ActiveWindow = window;
	}

	/* public void MuteAll(bool mute){
		
	}

	public void ChangeMusicVolume(float volume){

	}

	public void ChangeSFXVolume(float volume){
		
	}

	public void ChangeVoiceVolume(float volume){
		
	}

	public void ChangeNarratorVolume(float volume){
		
	} */

	public void ExitGame(){
		Application.Quit();
	}
}
