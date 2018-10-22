using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the visual elements present in each character portrait in the selection screen
/// </summary>
public class CharacterPortrait : MonoBehaviour {

	// PUBLIC ATTRIBUTES
	public GameObject[] Characters;
	public int charIndex = 0;
	public int colorIndex = 1;

	// SERIALIZED ATTRIBUTES
	[Header("Portrait images references")]
	[SerializeField] private GameObject PressStart;
	[SerializeField] private GameObject Selected;

	/// <summary>
	/// Changes the color of the selected character
	/// </summary>
	public void ChangeColor(){
		colorIndex++;
		if(colorIndex > 4) colorIndex = 1;

		Characters[charIndex].GetComponent<ChangeColor>().color = colorIndex;
		Characters[charIndex].GetComponent<ChangeColor>().ManualValidate();
	}

	/// <summary>
	/// Changes the selected character
	/// </summary>
	/// <param name="direction">1 for right, -1 for left</param>
	public void ChangeCharacter(int direction){
		Characters[charIndex].SetActive(false);
		charIndex += direction;
		if(charIndex >= Characters.Length){
			charIndex = 0;
		}
		else if(charIndex < 0){
			charIndex = Characters.Length-1;
		}
		Characters[charIndex].SetActive(true);
	}

	/// <summary>
	/// Hides the "press Start to join" image and shows the characters
	/// </summary>
	public void ShowCharacter(){
		Characters[charIndex].SetActive(true);		
		PressStart.SetActive(false);
	}

	/// <summary>
	/// Hides the characters and shows the "Press start to join" image
	/// </summary>
	public void HideCharacter(){
		PressStart.SetActive(true);
		Characters[charIndex].SetActive(false);				
	}

	/// <summary>
	/// Hides the characters and shows the "Selected" image
	/// </summary>
	public void ConfirmCharacter(){
		Selected.SetActive(true);
		Characters[charIndex].SetActive(false);	
	}

	/// <summary>
	/// Hides the "selected" image and shows the characters
	/// </summary>
	public void UnconfirmCharacter(){
		Characters[charIndex].SetActive(true);		
		Selected.SetActive(false);
	}
}
