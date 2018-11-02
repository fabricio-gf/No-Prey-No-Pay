using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

	private int CountdownTime = 3;
	private int CurrentTime;

	[SerializeField] private UnityEngine.UI.Text CountdownText;

	public void StartCountdown(){
		CurrentTime = 3;
		CountdownText.gameObject.SetActive(true);
		InvokeRepeating("CountdownOneSecond", 1f, 1f);
	}

	private void CountdownOneSecond(){
		if(CurrentTime > 0){
			CountdownText.text = CurrentTime.ToString();
			CurrentTime--;
		}
		else{
			CountdownText.text = "Brawl!";
			RoundReferee.instance.StartRound();
		}
	}

	IEnumerator HideCountdownText(){
		yield return new WaitForSeconds(1f);
		CountdownText.text = "";
		CountdownText.gameObject.SetActive(false);
	}
}
