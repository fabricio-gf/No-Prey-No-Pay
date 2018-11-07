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

	[SerializeField] private Sprite[] CharacterSprites;
	[SerializeField] private Sprite[] OverlaySprites;
	[SerializeField] private PlayerInfo[] infos;

	[SerializeField] private GameObject StatsWindow;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	void Start(){
		InitializeDictionary();
		InitializeListArray();
	}

	void InitializeDictionary(){
		foreach(var s in StatsList){
			s.value = new int[4];
			statsDict.Add(s.name, s);
		}
	}

	void InitializeListArray(){
		for(int i = 0; i < 4; i++){
			HighestStats[i] = new List<string>();
		}
	}

	public void IncrementValue(string key, int player){
		statsDict[key].value[player]++;
	}

	public void SetValue(string key, int player, int value){
		if(statsDict[key].value[player] < value)
		statsDict[key].value[player] = value;
	}

	public void DisplayStats(){
		StatsWindow.SetActive(true);
		foreach(var s in statsDict.Values){
			int max = -1, player = -1;
			for(int i = 0; i < 4; i++){
				if(s.value[i] > max){
					max = s.value[i];
					player = i;
				}
			}

			if(player > -1) {
				HighestStats[player].Add(s.name);
			}
		}

		for(int i = 0; i < 4; i++){
			if(infos[i].isSelected){

				StatsCard[i].gameObject.SetActive(true);
				
				// change portrait sprite and color
				Transform portrait = StatsCard[i].Find("PlayerPortrait");
				portrait.GetComponent<Image>().sprite = CharacterSprites[(int)infos[i].SelectedCharacter];
				portrait.Find("SuitOverlay").GetComponent<Image>().sprite = OverlaySprites[(int)infos[i].SelectedCharacter];
				portrait.GetComponent<ChangeColor>().color = infos[i].SelectedColor;
				portrait.GetComponent<ChangeColor>().ManualValidate();

				if(HighestStats[i].Count == 0){
					StatsCard[i].Find("TitleText").GetComponent<Text>().text = statsDict["average"].title;
					StatsCard[i].Find("DescriptionText").GetComponent<Text>().text = statsDict["average"].description;
					//StatsCard[i].Find("ValueText").GetComponent<Text>().text = statsDict["average"].value.ToString();	
				}
				else{
					//change texts for title, description and value of highest achievement (chosen randomly between the highest ones)
					int index = Random.Range(0, HighestStats[i].Count-1);
					StatsCard[i].Find("TitleText").GetComponent<Text>().text = statsDict[HighestStats[i][index]].title;
					StatsCard[i].Find("DescriptionText").GetComponent<Text>().text = statsDict[HighestStats[i][index]].description;
					//StatsCard[i].Find("ValueText").GetComponent<Text>().text = statsDict[HighestStats[i][index]].value.ToString();
				}
			}
			else{
				StatsCard[i].gameObject.SetActive(false);
			}
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
