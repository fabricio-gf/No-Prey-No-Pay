using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the settings screen behaviour
/// </summary>
public class CreditsScreen : MonoBehaviour {

    private MenuInputController m_input;

	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	void Awake(){
		m_input = this.gameObject.GetComponent<MenuInputController>();
	}

	void Update () {
		if(m_input.GetPrevious()){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
