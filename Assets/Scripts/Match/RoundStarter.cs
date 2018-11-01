using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStarter : MonoBehaviour {

	[SerializeField] private PlayerSpawner PSpawner;
	[SerializeField] private Countdown countdown;
	[SerializeField] private RoundReferee referee;

	// public WeaponInfo[] WeaponsToSpawn;
	public PlayerInfo[] PlayersToSpawn;

	// Use this for initialization
	void Start () {
		
	}

	void InitializeRound(){
		//PSpawner.PlayersToSpawn = PlayersToSpawn;
		//PSpawner.SpawnPlayers();

		//spawn weapons

		//start cowntdown
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
