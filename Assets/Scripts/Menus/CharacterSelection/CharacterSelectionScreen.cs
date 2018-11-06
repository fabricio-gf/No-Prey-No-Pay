using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls general behaviour in character selection screen, like inputs
/// </summary>
public class CharacterSelectionScreen : MonoBehaviour {

	// PUBLIC ATTRIBUTES
	public CharacterPortrait[] Portraits;

	// PRIVATE ATTRIBUTES
	private bool[] ActivePlayers;
	private int NumReadyPlayers = 0;
    [SerializeField] private CharacterSelectInputController m_input;
	[SerializeField] private MenuCity MenuAnimator;

    private bool[] ReadyPlayers;


	// SERIALIZED ATTRIBUTES
	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;

	void Awake(){
		m_input = GetComponent<CharacterSelectInputController>();
	}
	void Start () {
		ActivePlayers = new bool[4];
        ReadyPlayers = new bool[4];
		for(int i = 0; i < 4; i++){
			ActivePlayers[i] = false;
            ReadyPlayers[i] = false;
		}	
	}
	
	// Calls the input recieving methods
	void Update () {
		GetNewPlayers();

		GetExitPlayers();

		//GetCharacterSwap();

		GetColorSwap();

		GetPlayerConfirm();

		GetGameStart();
	}

	/// <summary>
	/// Detects if a new player has joined
	/// </summary>
	private void GetNewPlayers(){
		for(int i = 0; i < 4; i++){
			if(!ActivePlayers[i] && Portraits[i].Phase1 && m_input.GetSubmit(i)){
				AddPlayer(i);
			}
		}
	}

	/// <summary>
	/// Detects if an existing player has left, and if no players are remaining, goes back to the previous screen
	/// </summary>
	private void GetExitPlayers(){
		int inactive = 0;		
		
		for(int i = 0; i < 4; i++){
			if(!ActivePlayers[i]){
				if(Portraits[i].Phase1){
					inactive++;
				}
				else if(Portraits[i].Phase3){
					if(m_input.GetPrevious(i)){
						UnconfirmPlayer(i);
					}
				}
			}
			else if(ActivePlayers[i]){
				if(m_input.GetPrevious(i)){
					if(Portraits[i].Phase2){
						RemovePlayer(i);
					}
				}
			}
		}
		if(inactive == 4 && NumReadyPlayers == 0){
			for(int i = 0; i < 4; i++){
				if(m_input.GetPrevious(i)){
					MenuActions.instance.ChangePanel(MainMenuScreen);
					MenuAnimator.goBack();
				}
			}
		}
	}

	/// <summary>
	/// Detects if a player has swapped the color of it's current selected character
	/// </summary>
	private void GetColorSwap(){
		for(int i = 0; i < 4; i++){
			if(ActivePlayers[i] && m_input.GetChangeColor(i)){
				Portraits[i].ChangeColor();
			}
		}
	}

	/// <summary>
	/// Detects if a player is ready and has confirmed his character
	/// </summary>
	private void GetPlayerConfirm(){
		for(int i = 0; i < 4; i++){
			if(ActivePlayers[i] && m_input.GetSubmit(i)){
				ConfirmPlayer(i);
			}
		}
	}

	private void GetGameStart(){
		for(int i = 0; i < 4; i++){
			if(ReadyPlayers[i] && m_input.GetPause(i)){
				LevelLoader.instance.StartGame("Tavern");  //Temp string, gonna use scriptable object later or reference in level loader
			}
		}
	}

	/// <summary>
	/// Adds a selecting player to the level loader
	/// </summary>
	/// <param name="controller">Controller number [0,3]</param>
	public void AddPlayer(int controller){
		ActivePlayers[controller] = true;
        ReadyPlayers[controller] = false;
		Portraits[controller].ShowCharacter();
		LevelLoader.instance.AddPlayerActive();
	}

	/// <summary>
	/// Removes a selecting player from the level loader
	/// </summary>
	/// <param name="controller">Controller number [0,3]</param>
	public void RemovePlayer(int controller){
		ActivePlayers[controller] = false;
        ReadyPlayers[controller] = false;
		Portraits[controller].HideCharacter();
		LevelLoader.instance.RemovePlayerActive();
	}

	/* 	public void ChangeCharacter(int player, int direction){
		
	} */

	/// <summary>
	/// Adds a ready player to the level loader
	/// </summary>
	/// <param name="controller">Controller number [0,3]</param>
	public void ConfirmPlayer(int controller){
		ActivePlayers[controller] = false;
        ReadyPlayers[controller] = true;
		NumReadyPlayers++;
		Portraits[controller].ConfirmCharacter();
		LevelLoader.instance.AddPlayerReady(controller, Portraits[controller].charIndex, Portraits[controller].colorIndex);
	}

	/// <summary>
	/// Removes a ready player from the level loader
	/// </summary>
	/// <param name="controller">Controller number [0,3]</param>
	public void UnconfirmPlayer(int controller){
		ActivePlayers[controller] = true;
        ReadyPlayers[controller] = false;
		NumReadyPlayers--;
		Portraits[controller].UnconfirmCharacter();
		LevelLoader.instance.RemovePlayerReady(controller);
	}
}
