using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	[SerializeField] private GameObject MainMenuScreen;
	
	// Update is called once per frame
	void Update () {
		if(InputManager.GetButton(1, InputManager.Buttons.START)){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
