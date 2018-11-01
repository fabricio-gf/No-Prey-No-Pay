using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the splash screen behaviour
/// </summary>
public class SplashScreen : MonoBehaviour {

    [SerializeField] private MenuInputController m_input;

	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	void Awake(){
	}

	void Update () {
		if(m_input.GetPause()){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
