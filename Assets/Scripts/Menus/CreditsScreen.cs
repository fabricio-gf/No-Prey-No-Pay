using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour {

	[SerializeField] private GameObject MainMenuScreen;
	
	// Update is called once per frame
	void Update () {
		if(InputManager.GetButton(InputManager.Buttons.BACK)){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
