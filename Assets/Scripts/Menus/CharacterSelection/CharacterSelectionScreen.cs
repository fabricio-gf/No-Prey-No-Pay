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

	// SERIALIZED ATTRIBUTES
	[Header("Screen references")]
	[SerializeField] private GameObject MainMenuScreen;


	void Start () {
		ActivePlayers = new bool[4];
		for(int i = 0; i < 4; i++){
			ActivePlayers[i] = false;
		}	
	}
	
	// Calls the input recieving methods
	void Update () {
		GetNewPlayers();

		GetExitPlayers();

		//GetCharacterSwap();

		GetColorSwap();

		GetPlayerConfirm();
	}

	/// <summary>
	/// Detects if a new player has joined
	/// </summary>
	private void GetNewPlayers(){
		for(int i = 0; i < 4; i++){
			if(!ActivePlayers[i] && Portraits[i].Phase1 && InputManager.GetButton(i, InputManager.Buttons.START)){
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
					if(InputManager.GetButton(i, InputManager.Buttons.BACK)){
						UnconfirmPlayer(i);
					}
				}
			}
			else if(ActivePlayers[i]){
				if(InputManager.GetButton(i, InputManager.Buttons.BACK)){
					if(Portraits[i].Phase2){
						RemovePlayer(i);
					}
				}
			}
		}
		
		if(inactive == 4 && NumReadyPlayers == 0 && InputManager.GetButton(InputManager.Buttons.BACK)){
			MenuActions.instance.ChangePanel(MainMenuScreen);
		}
	}

	/*private void GetCharacterSwap(){
		
		bool right1 = InputManager.GetButton(1, InputManager.Buttons.RIGHT);
		bool left1 = InputManager.GetButton(1, InputManager.Buttons.LEFT);
		
		if(right1 && !DirectionPressed1){
			ChangeCharacter(1, 1);
			DirectionPressed1 = true;
		}
		if(left1 && !DirectionPressed1){
			ChangeCharacter(1, -1);
			DirectionPressed1 = true;
		}

		bool right2 = InputManager.GetButton(2, InputManager.Buttons.RIGHT);
		bool left2 = InputManager.GetButton(2, InputManager.Buttons.LEFT);

		if(right2 && !DirectionPressed2){
			ChangeCharacter(2, 1);
			DirectionPressed1 = true;
		}
		if(left2 && !DirectionPressed2){
			ChangeCharacter(2, -1);
			DirectionPressed1 = true;
		}

		bool right3 = InputManager.GetButton(3, InputManager.Buttons.RIGHT);
		bool left3 = InputManager.GetButton(3, InputManager.Buttons.LEFT);
		
		if(right3 && !DirectionPressed3){
			ChangeCharacter(3, 1);
			DirectionPressed1 = true;
		}
		if(left3 && !DirectionPressed3){
			ChangeCharacter(3, -1);
			DirectionPressed1 = true;
		}
		
		bool right4 = InputManager.GetButton(4, InputManager.Buttons.RIGHT);
		bool left4 = InputManager.GetButton(4, InputManager.Buttons.LEFT);

		if(right4 && !DirectionPressed4){
			ChangeCharacter(4, 1);
			DirectionPressed1 = true;
		}
		if(left4 && !DirectionPressed4){
			ChangeCharacter(4, -1);
			DirectionPressed1 = true;
		}
	}*/

	/// <summary>
	/// Detects if a player has swapped the color of it's current selected character
	/// </summary>
	private void GetColorSwap(){
		for(int i = 0; i < 4; i++){
			if(ActivePlayers[i] && InputManager.GetButton(i, InputManager.Buttons.ATTACK)){
				Portraits[i].ChangeColor();
			}
		}
	}

	/// <summary>
	/// Detects if a player is ready and has confirmed his character
	/// </summary>
	private void GetPlayerConfirm(){
		for(int i = 0; i < 4; i++){
			if(ActivePlayers[i] && InputManager.GetButton(i, InputManager.Buttons.SELECT)){
				ConfirmPlayer(i);
			}
		}
	}

	/// <summary>
	/// Adds a selecting player to the level loader
	/// </summary>
	/// <param name="controller">Controller number [0,3]</param>
	public void AddPlayer(int controller){
		ActivePlayers[controller] = true;
		Portraits[controller].ShowCharacter();
		LevelLoader.instance.AddPlayerActive();
	}

	/// <summary>
	/// Removes a selecting player from the level loader
	/// </summary>
	/// <param name="controller">Controller number [0,3]</param>
	public void RemovePlayer(int controller){
		ActivePlayers[controller] = false;
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
		NumReadyPlayers--;
		Portraits[controller].UnconfirmCharacter();
		LevelLoader.instance.RemovePlayerReady(controller, Portraits[controller].charIndex, Portraits[controller].colorIndex);
	}
}
