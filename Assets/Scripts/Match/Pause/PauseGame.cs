using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	[SerializeField] private GameMgr GameManager;
	[SerializeField] private GameObject PauseWindow;
	private PauseInputController m_input;

	void Start(){
		m_input = GetComponent<PauseInputController>();
	}

	// Update is called once per frame
	void Update () {
		GetPauseInput();
	}

	void GetPauseInput(){
		for(int i = 0; i < 4; i++){
			if(m_input.GetPause(i)){
				GameManager.TogglePause();
			}
		}
	}

	void TogglePauseWindow(){
		PauseWindow.SetActive(!PauseWindow.activeSelf);
	}
}
