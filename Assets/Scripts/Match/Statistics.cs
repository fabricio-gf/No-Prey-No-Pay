using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {

	public static Statistics instance;

	[SerializeField] Transform[] StatsCard = new Transform[4];
	[System.Serializable] class Stats{
		public string name;
		public string title;
		public string description;
		[HideInInspector] public int[] value;
	}

	[SerializeField] Stats[] StatsList;
 
	private Dictionary<string, Stats> statsDict = new Dictionary<string, Stats>();

	private List<string>[] HighestStats = new List<string>[4];

	void InitializeDictionary(){
		foreach(var s in StatsList){
			s.value = new int[4];
			statsDict.Add(s.name, s);
		}
	}

	public void IncrementValue(string key, int player){
		statsDict[key].value[player]++;
	}

	public void SetValue(string key, int player, int value){
		if(statsDict[key].value[player] < value)
		statsDict[key].value[player] = value;
	}

	void ChooseStats(){
		foreach(var s in statsDict.Values){
			int max = -1, player = -1;
			for(int i = 0; i < 4; i++){
				if(s.value[i] > max){
					max = s.value[i];
					player = i;
				}
			}
			if(player > -1) HighestStats[player].Add(s.name);
		}

		for(int i = 0; i < 4; i++){
			int index = Random.Range(0, HighestStats[i].Count);
			StatsCard[i].Find("TitleText").GetComponent<Text>().text = statsDict[HighestStats[i][index]].title;
			StatsCard[i].Find("DescriptionText").GetComponent<Text>().text = statsDict[HighestStats[i][index]].description;
			StatsCard[i].Find("ValueText").GetComponent<Text>().text = statsDict[HighestStats[i][index]].value.ToString();
		}
	}

	int ReturnHighestValue(int[] array){
		int index = 0;
		int selectedPlayer = 0;
		int highestValue = 0;
		foreach(int i in array){
			index++;
			if(i > highestValue){
				highestValue = i;
				selectedPlayer = index;
			}
		}

		return selectedPlayer;
	}
}
