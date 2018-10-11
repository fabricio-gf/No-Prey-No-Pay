using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

	public GameObject MainMenuScreen;

	public int PlayerCount;
	private bool[] ActivePlayers;
	private Character[] SelectedCharacters;
	public GameObject[] CharacterPortraits;

	/*private bool DirectionPressed1 = false;
	private bool DirectionPressed2 = false;
	private bool DirectionPressed3 = false;
	private bool DirectionPressed4 = false;*/


	// Use this for initialization
	void Start () {
		ActivePlayers = new bool[4];
		for(int i = 0; i < 4; i++){
			ActivePlayers[i] = false;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		GetNewPlayers();

		GetExitPlayers();

		//GetCharacterSwap();

		GetColorSwap();
	}

	private void GetNewPlayers(){
		for(int i = 0; i < 4; i++){
			if(!ActivePlayers[i] && InputManager.GetButton(i, InputManager.Buttons.START)){
				AddPlayer(i);
			}
		}
	}

	private void GetExitPlayers(){
		int inactive = 0;		
		
		for(int i = 0; i < 4; i++){
			if(!ActivePlayers[i]){
				inactive++;
			}
			else if(ActivePlayers[i] && InputManager.GetButton(i, InputManager.Buttons.BACK)){
				RemovePlayer(i);
			}
		}
		
		if(inactive == 4 && InputManager.GetButton(InputManager.Buttons.BACK)){
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

	private void GetColorSwap(){
		for(int i = 0; i < 4; i++){
			if(ActivePlayers[i] && InputManager.GetButton(i, InputManager.Buttons.ATTACK)){
				CharacterPortraits[i].GetComponent<CharacterPortrait>().ChangeColor();
			}
		}
	}

	public void AddPlayer(int controller){
		ActivePlayers[controller] = true;
		PlayerCount++;
	}

	public void RemovePlayer(int controller){
		ActivePlayers[controller] = false;
		PlayerCount--;
	}

	public void ChangeCharacter(int player, int direction){
		
	}

	public void ChangeColor(int player){

	}

	public void ChangeColor(int index, int variant){
		
	}
}
