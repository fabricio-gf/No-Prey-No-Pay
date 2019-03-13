using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject {

	public enum Character{
		ACE,
		QUEEN,
		JACK,
		KING
	}

	public bool isSelected;
	[Range(0,3)] public int ControllerNumber;
	public Character SelectedCharacter;
	[Range(1,4)] public int SelectedColor;
}
