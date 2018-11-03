using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStarter : MonoBehaviour {

	[SerializeField] private PlayerSpawner playerSpawner;
	[SerializeField] private Countdown countdown;

	// public WeaponInfo[] WeaponsToSpawn;
	public List<PlayerInfo> PlayersToSpawn;
	public int StockLimit;
	public float TimeLimit;

	public void InitializeRound(){
		playerSpawner.SpawnPlayers(PlayersToSpawn);

		//spawn weapons

		RoundReferee.instance.NumOfPlayers = PlayersToSpawn.Count;
		countdown.StartCountdown();
	}
}
