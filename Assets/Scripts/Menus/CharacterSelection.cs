using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour {

	public int PlayerCount;
	private List<Character> SelectedCharacters;

	private bool DirectionPressed1 = false;
	private bool DirectionPressed2 = false;
	private bool DirectionPressed3 = false;
	private bool DirectionPressed4 = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetNewPlayers();

		GetExitPlayers();

		GetCharacterSwap();

		GetColorSwap();
	}

	private void GetNewPlayers(){
		if(InputManager.GetButton(1, InputManager.Buttons.START)){
			AddPlayer(1);
		}
		if(InputManager.GetButton(2, InputManager.Buttons.START)){
			AddPlayer(2);
		}
		if(InputManager.GetButton(3, InputManager.Buttons.START)){
			AddPlayer(3);			
		}
		if(InputManager.GetButton(4, InputManager.Buttons.START)){
			AddPlayer(4);			
		}
	}

	private void GetExitPlayers(){
		if(InputManager.GetButton(1, InputManager.Buttons.BACK)){
			RemovePlayer(1);
		}
		if(InputManager.GetButton(2, InputManager.Buttons.BACK)){
			RemovePlayer(2);
		}
		if(InputManager.GetButton(3, InputManager.Buttons.BACK)){
			RemovePlayer(3);			
		}
		if(InputManager.GetButton(4, InputManager.Buttons.BACK)){
			RemovePlayer(4);			
		}
	}

	private void GetCharacterSwap(){
		
		bool right1 = InputManager.GetButton(1, InputManager.Buttons.RIGHT);
		bool left1 = InputManager.GetButton(1, InputManager.Buttons.LEFT);
		
		if(right1 && !DirectionPressed1){
			ChangeCharacter(1);
			DirectionPressed1 = true;
		}
		if(left1 && !DirectionPressed1){
			ChangeCharacter(1);
			DirectionPressed1 = true;
		}

		bool right2 = InputManager.GetButton(2, InputManager.Buttons.RIGHT);
		bool left2 = InputManager.GetButton(2, InputManager.Buttons.LEFT);

		if(right2 && !DirectionPressed2){
			ChangeCharacter(2);
			DirectionPressed1 = true;
		}
		if(left2 && !DirectionPressed2){
			ChangeCharacter(2);
			DirectionPressed1 = true;
		}

		bool right3 = InputManager.GetButton(3, InputManager.Buttons.RIGHT);
		bool left3 = InputManager.GetButton(3, InputManager.Buttons.LEFT);
		
		if(right3 && !DirectionPressed3){
			ChangeCharacter(3);
			DirectionPressed1 = true;
		}
		if(left3 && !DirectionPressed3){
			ChangeCharacter(3);
			DirectionPressed1 = true;
		}
		
		bool right4 = InputManager.GetButton(4, InputManager.Buttons.RIGHT);
		bool left4 = InputManager.GetButton(4, InputManager.Buttons.LEFT);

		if(right4 && !DirectionPressed4){
			ChangeCharacter(4);
			DirectionPressed1 = true;
		}
		if(left4 && !DirectionPressed4){
			ChangeCharacter(4);
			DirectionPressed1 = true;
		}
	}

	private void GetColorSwap(){
		if(InputManager.GetButton(1, InputManager.Buttons.ATTACK)){
			ChangeColor(1);
		}
		if(InputManager.GetButton(2, InputManager.Buttons.ATTACK)){
			ChangeColor(2);
		}
		if(InputManager.GetButton(3, InputManager.Buttons.ATTACK)){
			ChangeColor(3);			
		}
		if(InputManager.GetButton(4, InputManager.Buttons.ATTACK)){
			ChangeColor(4);
		}
	}

	public void AddPlayer(int controller){

	}

	public void RemovePlayer(int controller){

	}

	public void ChangeCharacter(int player){
		
	}

	public void ChangeColor(int player){

	}

	public void ChangeColor(int index, int variant){
		
	}
}
