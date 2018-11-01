using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	[SerializeField] private Vector2[] SpawnPoints = new Vector2[4];
	[SerializeField] private GameObject[] CharPrefabs;
	[SerializeField] private Transform Dynamic;
	public List<PlayerInfo> PlayersToSpawn;

	public void SpawnPlayers(){
		int i = 0;
		foreach(var pi in PlayersToSpawn){
			var obj = Instantiate(CharPrefabs[(int)pi.SelectedCharacter], SpawnPoints[i], Quaternion.identity, Dynamic);
			obj.GetComponent<ChangeColor>().color = pi.SelectedColor;
			obj.GetComponent<PlayerInputCtlr>().m_nbPlayer = (PlayerInputCtlr.ePlayer)(pi.ControllerNumber+1);
			i++;
		}
	}
}
