using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the main menu screen behaviour
/// </summary>
public class MainMenuScreen : MonoBehaviour {

	// PRIVATE ATTRIBUTES
	private int index = 0;
	private bool DirectionPressed = false;
	private EventSystem eventSystem;

	// SERIALIZED ATTRIBUTES
	[Header("Screen references")]
	[SerializeField] private GameObject CharacterSelectScreen;
	[SerializeField] private GameObject SettingsScreen;
	[SerializeField] private GameObject CreditsScreen;

	[SerializeField] private GameObject SplashScreen;

	[Header("Button references")]
	[SerializeField] private GameObject[] Buttons;

	void Start(){
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	// It is necessary to make the event system select a button everytime this panel is activated
	void OnEnable(){
		StartCoroutine(WaitOneFrame());
	}

	IEnumerator WaitOneFrame(){
		yield return null;
		eventSystem = EventSystem.current;
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	// Detects all the inputs that can be made in this screen
	void Update () {

		// Directional inputs
		float vertical = InputManager.GetAxis(InputManager.Axis.VERTICAL);
		bool up = InputManager.GetButton(InputManager.Buttons.UP);
		bool down = InputManager.GetButton(InputManager.Buttons.DOWN);
		
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

		// Button inputs
		if(InputManager.GetButtonDown(InputManager.Buttons.SELECT)){
			SelectButton();
		}

		if(InputManager.GetButtonDown(InputManager.Buttons.BACK)){
			MenuActions.instance.ChangePanel(SplashScreen);
		}
	}

	/// <summary>
	/// Changes the selected button on the screen
	/// </summary>
	/// <param name="direction">1 for down, -1 for up</param>
	private void MoveSelection(int direction){
		index += direction;
		if(index >= Buttons.Length){
			index = 0;
		}
		else if(index < 0){
			index = Buttons.Length-1;
		}
		
		eventSystem.SetSelectedGameObject(Buttons[index]);
	}

	/// <summary>
	/// Calls the appropriate function for the selected button
	/// </summary>
	private void SelectButton(){
		switch (index)
		{
			case 0:
				MenuActions.instance.ChangePanel(CharacterSelectScreen);
				break;
			/* case 1:
				MenuActions.instance.ChangePanel(SettingsScreen);
				break; */
			case 1:
				MenuActions.instance.ChangePanel(CreditsScreen);
				break;
			case 2:
				MenuActions.instance.ExitGame();
				break;
			default:
				break;
		}
	}
}
