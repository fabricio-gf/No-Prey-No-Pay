using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject {

	public enum Character{
		ACE,
		JACK,
		KING,
		QUEEN
	}

	public bool isSelected;
	[Range(0,3)] public int ControllerNumber;
	public Character SelectedCharacter;
	public int SelectedColor;
}
