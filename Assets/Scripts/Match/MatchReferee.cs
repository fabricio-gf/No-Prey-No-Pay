using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchReferee : MonoBehaviour {

	public int NumberOfWinsToEnd;
	private int[] Wins;
	private int NumOfPlayers = 0;
	public bool[] ConnectedPlayers = new bool[4];

	[SerializeField] private PlayerSpawner PSpawner;
	[SerializeField] private RoundStarter RStarter;
	
	[SerializeField] private PlayerInfo[] PlayerInfos = new PlayerInfo[4];

	[SerializeField] private GameObject VictoryWindow;

	private bool MatchEnded = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		InitializeScene();
	}

	void InitializeScene(){
		LevelLoader loader = GameObject.FindWithTag("LevelLoader").GetComponent<LevelLoader>();


		// Set player infos
		ConnectedPlayers = loader.ConnectedPlayers;

		List<PlayerInfo> infos = new List<PlayerInfo>();

		for(int i = 0; i < ConnectedPlayers.Length; i++){
			if(ConnectedPlayers[i]){
				infos.Add(PlayerInfos[i]);
				NumOfPlayers++;
			}
		}

		// RStarter.PlayersToSpawn = infos;

		// Set winners info
		Wins = new int[NumOfPlayers];
		for(int i = 0; i < Wins.Length; i++){
			Wins[i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(MatchEnded == true){
			// get inputs here
		}
	}

	public void EndRound(int PlayerNumber){
		Wins[PlayerNumber]++;
		if(Wins[PlayerNumber] >= NumberOfWinsToEnd){
			EndMatch();
		}
	}

	public void EndMatch(){
		ToggleVictoryWindow();
		//show stats
		MatchEnded = true;
	}

	private void ToggleVictoryWindow(){
		VictoryWindow.SetActive(!VictoryWindow.activeSelf);
	}
}
