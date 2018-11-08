using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchVictory : MonoBehaviour {

	[SerializeField] private UnityEngine.UI.Text VictoryText;

	[SerializeField] private MenuInputController m_input;

	void Update(){
		if(m_input.GetSubmit()){
			Statistics.instance.DisplayStats();
			DeactivateVictoryWindow();
		}
	}

	public void UpdateVictoryText(int player){
		VictoryText.text = "Player " + player + " wins the match!\nPress A to continue";
	}

	public void DeactivateVictoryWindow(){
		VictoryText.text = "";
		gameObject.SetActive(false);
	}
}
