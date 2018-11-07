using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchReferee : MonoBehaviour {

    // SINGLETON
    public static MatchReferee instance;

    // PRIVATE ATTRIBUTES
	[SerializeField] private MatchInfo matchInfo;
	private int[] Wins;
	private int NumOfPlayers = 0;
	
	[SerializeField] private PlayerInfo[] PlayerInfos = new PlayerInfo[4];

	[SerializeField] private GameObject RoundVictoryWindow;
	[SerializeField] private GameObject MatchVictoryWindow;

    // INTERNAL MEMBERS
    void Awake()
    {
        Debug.Assert(instance == null ,this.gameObject.name + " - MatchReferee : must be unique!");
        instance = this;
    }

    // Use this for initialization
    void Start () {
		//DontDestroyOnLoad(gameObject);
		InitializeScene();
	}

	void InitializeScene(){
		// Get weapon infos
		//RStarter.WeaponsToSpawn = weapons;
		
		RoundStarter.InitializeRound(GetAtivePlayerInfos());

		// Set winners initial info
		Wins = new int[NumOfPlayers];
		for(int i = 0; i < Wins.Length; i++){
			Wins[i] = 0;
		}
	}
	
	public void EndRound(int PlayerNumber){
		Wins[PlayerNumber]++;
		if(Wins[PlayerNumber] >= matchInfo.NumberOfWinsToEnd){
			EndMatch(PlayerNumber);
		}
		else{
            instance.RoundVictoryWindow.SetActive(true);
        	RoundVictory roundVictory = instance.RoundVictoryWindow.GetComponent<RoundVictory>();
        	roundVictory.UpdateVictoryText(PlayerNumber + 1);
            //RoundStarter.RestartRound();
        }
	}

	public void EndMatch(int p_number){
		ToggleMatchVictoryWindow();
		MatchVictoryWindow.GetComponent<MatchVictory>().UpdateVictoryText(p_number+1);
		GameMgr.PauseGame();
	}

	private void ToggleMatchVictoryWindow(){
		MatchVictoryWindow.SetActive(!MatchVictoryWindow.activeSelf);
	}

    private List<PlayerInfo> GetAtivePlayerInfos ()
    {
        List<PlayerInfo> infos = new List<PlayerInfo>();

        // Get player infos
        for (int i = 0; i < PlayerInfos.Length; i++)
        {
            if (PlayerInfos[i].isSelected)
            {
                infos.Add(PlayerInfos[i]);
                NumOfPlayers++;
            }
        }

        return infos;
    }
}
