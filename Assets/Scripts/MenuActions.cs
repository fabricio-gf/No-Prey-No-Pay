using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour {

	[Header("References")]
	[SerializeField] private GameObject MusicManager;
	[SerializeField] private GameObject SFXManager;
	[SerializeField] private GameObject VoiceManager;
	[SerializeField] private GameObject NarratorManager;

	private GameObject ActivePanel;
	private GameObject ActiveWindow;


	public void ChangePanel(GameObject panel){
		//deactivate current panel
		panel.SetActive(true);
	}

	public void TriggerWindow(GameObject window){
		//deactivate current window
		window.SetActive(true);
	}

	public void MuteAll(bool mute){
		
	}

	public void ChangeMusicVolume(float volume){

	}

	public void ChangeSFXVolume(float volume){
		
	}

	public void ChangeVoiceVolume(float volume){
		
	}

	public void ChangeNarratorVolume(float volume){
		
	}
}
