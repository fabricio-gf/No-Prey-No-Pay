using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuScreen : MonoBehaviour {

	[SerializeField] private GameObject CharacterSelectScreen;
	[SerializeField] private GameObject SettingsScreen;
	[SerializeField] private GameObject CreditsScreen;

	[SerializeField] private GameObject[] Buttons;

	private int index = 0;
	private bool DirectionPressed = false;
	private EventSystem eventSystem;

	void Start(){
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	void OnEnable(){
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	// Update is called once per frame
	void Update () {
		float vertical = InputManager.GetAxis(InputManager.Axis.VERTICAL);
		bool up = InputManager.GetButton(InputManager.Buttons.UP);
		bool down = InputManager.GetButton(InputManager.Buttons.DOWN);
		
		print(down + " " + up);
		if(!down && !up){
			DirectionPressed = false;
		}
		/*
		if(vertical == 0){
			DirectionPressed = false;
		}
		else if(vertical < 0.3f && DirectionPressed == false){
			DirectionPressed = true;
			MoveSelection(1);
		}
		else if(vertical > 0.3f && DirectionPressed == false){
			DirectionPressed = true;
			MoveSelection(-1);
		}
		*/
		if(down && !DirectionPressed){
			MoveSelection(1);
			DirectionPressed = true;
		}
		if(up && !DirectionPressed){
			MoveSelection(-1);
			DirectionPressed = true;

		}
		
		
		/*switch (vertical)
		{
			case 0:
				AnalogPushed = false;
				break;
			default:
				if(AnalogPushed = true)
					break;
				AnalogPushed = true;
				MoveSelection(vertical);
				break;
		}*/

		if(InputManager.GetButton(InputManager.Buttons.SELECT)){
			SelectButton();
		}
	}

	private void MoveSelection(int direction){
		index += direction;
		if(index >= Buttons.Length){
			index = 0;
		}
		else if(index < 0){
			index = Buttons.Length-1;
		}
		print(index);
		
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	private void SelectButton(){
		switch (index)
		{
			case 0:
				MenuActions.instance.ChangePanel(CharacterSelectScreen);
				break;
			case 1:
				MenuActions.instance.ChangePanel(SettingsScreen);
				break;
			case 2:
				MenuActions.instance.ChangePanel(CreditsScreen);
				break;
			case 3:
				MenuActions.instance.ExitGame();
				break;
			default:
				break;
		}
	}
}
