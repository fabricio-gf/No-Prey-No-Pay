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
	public int StockLimit;
	public float TimeLimit;
	public int NumberOfWinsToEnd;

	// PRIVATE ATTRIBUTES
	private int NumActivePlayers;
	private int NumReadyPlayers;
	[HideInInspector] public bool[] ConnectedPlayers = new bool[4];
	private bool CanStart;

	// SERIALIZED ATTRIBUTES
	[SerializeField] private PlayerInfo[] PlayerList = new PlayerInfo[4];	
	[SerializeField] private UnityEngine.UI.Text StartText;
	[SerializeField] private Color StartTextActiveColor;
	[SerializeField] private Color StartTextNonActiveColor;


	public void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this.gameObject);
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
		PlayerList[playerNumber].isSelected = true;
		PlayerList[playerNumber].SelectedCharacter = (PlayerInfo.Character)character;
		PlayerList[playerNumber].SelectedColor = color;
		ConnectedPlayers[playerNumber] = true;

		NumReadyPlayers ++;
		CheckStartButton();

	}

	public void RemovePlayerReady(int playerNumber){
		PlayerList[playerNumber].isSelected = false;
		ConnectedPlayers[playerNumber] = false;

		NumReadyPlayers --;
		CheckStartButton();

	}
	
}
