using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReferee : MonoBehaviour {

    // SINGLETON
	public static RoundReferee instance;

    // PUBLIC ATTRIBUTES
	public float StockLimit;

    // PRIVATE ATTRIBUTES
	[SerializeField] private ScoreDisplay Display;
	[SerializeField] private GameObject VictoryWindow;

    public void Awake(){
        // init singleton
        Debug.Assert(instance == null, this.gameObject.name + " - RoundReferee : must be unique!");
        instance = this;
    }

    public void Start(){
    }

	public static void StartRound(){
		print("ROUND STARTED");

        GameMgr.PlayGame();

        foreach (GameObject player in PlayerSpawner.SpawnnedPlayers)
        {
            player.SendMessage("MSG_StartRound", instance);
        }
	}
	
}
