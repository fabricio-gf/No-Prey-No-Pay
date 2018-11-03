using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundVictory : MonoBehaviour {

	[SerializeField] private UnityEngine.UI.Text VictoryText;

	public void UpdateVictoryText(int player){
		VictoryText.text = "Player " + player + " wins the round!";
	}

	public void DeactivateVictoryWindow(){
		VictoryText.text = "";
		gameObject.SetActive(false);
	}
	
}
