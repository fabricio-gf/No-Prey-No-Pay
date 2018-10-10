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
	private bool AnalogPushed = false;
	private EventSystem eventSystem;

	void Start(){
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	// Update is called once per frame
	void Update () {
		float vertical = InputManager.GetAxis(1, InputManager.Axis.VERTICAL);
		
		if(vertical == 0){
			AnalogPushed = false;
		}
		else if(vertical < 0.3f && AnalogPushed == false){
			AnalogPushed = true;
			MoveSelection(1);
		}
		else if(vertical > 0.3f && AnalogPushed == false){
			AnalogPushed = true;
			MoveSelection(-1);
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

		if(InputManager.GetButton(1, InputManager.Buttons.SELECT)){
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
