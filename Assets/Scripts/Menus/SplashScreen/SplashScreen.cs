using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the splash screen behaviour
/// </summary>
public class SplashScreen : MonoBehaviour {

    [SerializeField] private MenuInputController m_input;
	[SerializeField] private MenuCity MenuAnimator;


	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	void Awake(){
        if (m_input == null)
		    m_input = transform.parent.GetComponent<MenuInputController>();

        Debug.Assert(m_input != null, this.gameObject.name + " - SplashScreen : Missing MenuInputController");
    }

	void Update () {
		if(m_input.GetPause()){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
