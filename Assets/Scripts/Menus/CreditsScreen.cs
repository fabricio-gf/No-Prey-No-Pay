using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the settings screen behaviour
/// </summary>
public class CreditsScreen : MonoBehaviour {

	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	// Update is called once per frame
	void Update () {
		if(InputManager.GetButton(InputManager.Buttons.BACK)){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
