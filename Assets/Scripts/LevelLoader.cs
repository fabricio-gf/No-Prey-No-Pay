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
	[SerializeField] private MatchInfo matchInfo;

	// PRIVATE ATTRIBUTES
	private int NumActivePlayers;
	private int NumReadyPlayers;
	private bool CanStart;

	// SERIALIZED ATTRIBUTES
	[SerializeField] private PlayerInfo[] PlayerList = new PlayerInfo[4];	
	[SerializeField] private UnityEngine.UI.Image StartText;
	[SerializeField] private Color StartTextActiveColor;
	[SerializeField] private Color StartTextNonActiveColor;


	public void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

#if UNITY_EDITOR
	// CHEAT CODES
	public void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			matchInfo.StockLimit = 3;
			matchInfo.TimeLimit = 0;
			matchInfo.NumberOfWinsToEnd = 3;
			PlayerList[0].isSelected = true;
			PlayerList[0].SelectedColor = 2;
			PlayerList[1].isSelected = true;
			PlayerList[1].SelectedColor = 4;
			SceneManager.LoadScene("TavernGold");
		}
	}
#endif

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
			print(scene);
			SceneManager.LoadScene(scene);
		}
	}

	public void GoToMenu(){
		ResetReadyPlayers();
		SceneManager.LoadScene("MenuGold");
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

		NumReadyPlayers ++;
		CheckStartButton();

	}

	public void RemovePlayerReady(int playerNumber){
		PlayerList[playerNumber].isSelected = false;

		NumReadyPlayers --;
		CheckStartButton();
	}

	private void ResetReadyPlayers(){
		for(int i = 0; i < 4; i++){
			PlayerList[i].isSelected = false;
			//PlayerList[i].SelectedCharacter = (PlayerInfo.Character)i;
			PlayerList[i].SelectedColor = i+1;
		}
	}
	
}
