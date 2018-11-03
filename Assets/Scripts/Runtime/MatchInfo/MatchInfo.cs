using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MatchInfo")]
public class MatchInfo : ScriptableObject {

	[Range(0,3)] public int StockLimit;
	[Range(0, 120)] public float TimeLimit;
	[Range(0, 5)] public int NumberOfWinsToEnd;
}
