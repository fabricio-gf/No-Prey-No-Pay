using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReferee : MonoBehaviour {

	public static RoundReferee instance;

	public float StockLimit;

	[SerializeField] private ScoreDisplay Display;
	[SerializeField] private GameObject VictoryWindow;

	[SerializeField] private MatchReferee MRef;

	public int NumOfPlayers;
	private GameObject[] Players;

	public void Start(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	public void StartRound(){
		/*
		for(int i = 0; i < 4; i++){

		}
		 */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
