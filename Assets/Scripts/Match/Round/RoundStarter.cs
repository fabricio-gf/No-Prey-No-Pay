using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStarter : MonoBehaviour {

    // SINGLETON
    private static RoundStarter instance;
    
	[SerializeField] private Countdown countdown;
    
    public void Awake()
    {
        Debug.Assert(instance == null, this.gameObject.name + " - RoundStarter : must be unique!");
        instance = this;
    }

    public static void InitializeRound(List<PlayerInfo> playersToSpawn){
		PlayerSpawner.SpawnPlayers(playersToSpawn);

        //spawn weapons
        instance.countdown.StartCountdown();
	}

	public static void RestartRound(){
		PlayerSpawner.RespawnPlayers();

        //respawn weapons
        instance.countdown.StartCountdown();
	}
}
