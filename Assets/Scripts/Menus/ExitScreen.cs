using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScreen : MonoBehaviour {

	[SerializeField] private MenuInputController m_input;
	[SerializeField] private MenuCity MenuAnimator;



	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;
	
	void Awake(){
		m_input = transform.parent.GetComponent<MenuInputController>();
	}

	void Update () {
		if(m_input.GetSubmit()){
			MenuActions.instance.ExitGame();
		}
		if(m_input.GetPrevious()){
			MenuAnimator.goBack();
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}
}
