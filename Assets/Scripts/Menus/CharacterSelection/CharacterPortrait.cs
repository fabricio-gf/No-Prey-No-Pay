using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the visual elements present in each character portrait in the selection screen
/// </summary>
public class CharacterPortrait : MonoBehaviour {

	// PUBLIC ATTRIBUTES
	[SerializeField] private Sprite[] Characters;
	[SerializeField] private GameObject Poster;
	[SerializeField] private Image Char;
	
	[HideInInspector] public int charIndex = 0;
	[HideInInspector] public int colorIndex = 1;

	[HideInInspector] public bool Phase1 = true;
	[HideInInspector] public bool Phase2 = false;
	[HideInInspector] public bool Phase3 = false;

	// SERIALIZED ATTRIBUTES
	[Header("Portrait images references")]
	[SerializeField] private GameObject Selected;

	/// <summary>
	/// Changes the color of the selected character
	/// </summary>
	public void ChangeColor(){
		colorIndex++;
		if(colorIndex > 4) colorIndex = 1;

		Char.GetComponent<ChangeColor>().color = colorIndex;
		Char.GetComponent<ChangeColor>().ManualValidate();
	}

	/// <summary>
	/// Changes the selected character
	/// </summary>
	/// <param name="direction">1 for right, -1 for left</param>
	public void ChangeCharacter(int direction){

		charIndex += direction;
		if(charIndex >= Characters.Length){
			charIndex = 0;
		}
		else if(charIndex < 0){
			charIndex = Characters.Length-1;
		}
		Char.sprite = Characters[charIndex];
	}

	/// <summary>
	/// Hides the "press Start to join" image and shows the characters
	/// </summary>
	public void ShowCharacter(){
		Poster.SetActive(true);		
		Phase1 = Phase3 = false;
		Phase2 = true;
	}

	/// <summary>
	/// Hides the characters and shows the "Press start to join" image
	/// </summary>
	public void HideCharacter(){
		Poster.SetActive(false);	
		Phase2 = Phase3 = false;
		Phase1 = true;			
	}

	/// <summary>
	/// Hides the characters and shows the "Selected" image
	/// </summary>
	public void ConfirmCharacter(){
		Selected.SetActive(true);
		Phase1 = Phase2 = false;
		Phase3 = true;
	}

	/// <summary>
	/// Hides the "selected" image and shows the characters
	/// </summary>
	public void UnconfirmCharacter(){		
		Selected.SetActive(false);
		Phase1 = Phase3 = false;
		Phase2 = true;
	}

	public void ResetPortrait(){
		charIndex = 0;
		colorIndex = 1;
		HideCharacter();
	}
}
