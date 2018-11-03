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
	private EventSystem eventSystem;
    [SerializeField] private MenuInputController m_input;


	// SERIALIZED ATTRIBUTES
	[Header("Screen references")]
	[SerializeField] private GameObject CharacterSelectScreen;
	[SerializeField] private GameObject SettingsScreen;
	[SerializeField] private GameObject CreditsScreen;

	[SerializeField] private GameObject SplashScreen;

	[Header("Button references")]
	[SerializeField] private GameObject[] Buttons;

	void Awake(){
	}

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
		bool up = m_input.GetUp();
		bool down = m_input.GetDown();
		
		if(down){
			MoveSelection(-1);
		}
		if(up){
			MoveSelection(1);
		}

		// Button inputs
		if(m_input.GetSubmit()){
			SelectButton();
		}

		if(m_input.GetPrevious()){
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
