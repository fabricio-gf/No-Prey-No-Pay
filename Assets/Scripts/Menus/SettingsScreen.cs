using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the settings screen behaviour
/// </summary>
public class SettingsScreen : MonoBehaviour {

    [SerializeField] private MenuInputController m_input;


	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	void Awake(){
	}

	void Update () {
		if(m_input.GetPrevious()){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
