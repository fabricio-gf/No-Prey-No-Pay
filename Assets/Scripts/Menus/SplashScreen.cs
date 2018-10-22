using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the splash screen behaviour
/// </summary>
public class SplashScreen : MonoBehaviour {

	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	// Update is called once per frame
	void Update () {
		if(InputManager.GetButton(InputManager.Buttons.START)){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
