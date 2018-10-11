using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPortrait : MonoBehaviour {

	public ChangeColor[] Characters;
	private int charIndex = 0;
	private int colorIndex = 1;

	public void ChangeColor(){
		colorIndex++;
		if(colorIndex > 4) colorIndex = 1;

		Characters[charIndex].color = colorIndex;
		Characters[charIndex].ManualValidate();
	}
}
