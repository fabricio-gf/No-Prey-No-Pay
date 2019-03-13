using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWindow : MonoBehaviour {

	[SerializeField] private MenuInputController m_input;

	void Update(){

#if UNITY_EDITOR
		if(m_input.GetPause()){
			UnityEngine.SceneManagement.SceneManager.LoadScene("MenuGold");
		}
		else if(m_input.GetChangeColor()){
			UnityEngine.SceneManagement.SceneManager.LoadScene("TavernGold");
		}
#endif
		if(m_input.GetPause()){
			LevelLoader.instance.GoToMenu();
		}
		else if(m_input.GetChangeColor()){
			LevelLoader.instance.StartGame("TavernGold");
		}
	}
}
