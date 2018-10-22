using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager that stores player info from character selection, and loads the game scene, calling the player spawners then.
/// Is a singleton
/// </summary>
public class LevelLoader : MonoBehaviour {

	// PUBLIC ATTRIBUTES
	public static LevelLoader instance;

	public struct PlayerInfo{
		public int playerNumber;
		public int character;
		public int color;
	};

	// PRIVATE ATTRIBUTES
	private List<PlayerInfo> PlayerList = new List<PlayerInfo>();

	private int NumActivePlayers;
	private int NumReadyPlayers;
	private bool CanStart;

	// SERIALIZED ATTRIBUTES
	[SerializeField] private UnityEngine.UI.Text StartText;
	[SerializeField] private Color StartTextActiveColor;
	[SerializeField] private Color StartTextNonActiveColor;


	public void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	/// <summary>
	/// Checks if all active players have selected a character, and changes the color of the "Press start" text to indicate the game can begin
	/// </summary>
	private void CheckStartButton(){
		if(NumReadyPlayers == NumActivePlayers && NumReadyPlayers >= 2){
			CanStart = true;
			StartText.color = StartTextActiveColor;
		}
		else{
			CanStart = false;
			StartText.color = StartTextNonActiveColor;
		}
	}

	public void StartGame(string scene){
		if(CanStart){
			SceneManager.LoadScene(scene);
		}
	}

	public void AddPlayerActive(){
		NumActivePlayers ++;
		CheckStartButton();
	}

	public void RemovePlayerActive(){
		NumActivePlayers --;
		CheckStartButton();
	}

	public void AddPlayerReady(int playerNumber, int character, int color){
		PlayerInfo p;
		p.playerNumber = playerNumber;
		p.character = character;
		p.color = color;
		PlayerList.Add(p);

		NumReadyPlayers ++;
		CheckStartButton();

	}

	public void RemovePlayerReady(int playerNumber, int character, int color){
		PlayerInfo p;
		p.playerNumber = playerNumber;
		p.character = character;
		p.color = color;
		PlayerList.Remove(p);

		NumReadyPlayers --;
		CheckStartButton();

	}
	
}
