using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	[SerializeField] private Vector2[] SpawnPoints = new Vector2[4];
	[SerializeField] private GameObject[] CharPrefabs;
	[SerializeField] private Transform PlayerParentObject;

	public void SpawnPlayers(List<PlayerInfo> PlayersToSpawn){
		int i = 0;
		foreach(var pi in PlayersToSpawn){
			var obj = Instantiate(CharPrefabs[(int)pi.SelectedCharacter], SpawnPoints[i], Quaternion.identity, PlayerParentObject);
			obj.GetComponent<ChangeColor>().color = pi.SelectedColor;
			obj.GetComponent<ChangeColor>().ManualValidate();
			obj.GetComponent<PlayerInputCtlr>().m_nbPlayer = (PlayerInputCtlr.ePlayer)(pi.ControllerNumber+1);
			i++;
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		for(int i = 0; i < 4; i++){
        	Gizmos.DrawCube((Vector3)SpawnPoints[i], new Vector3(0.5f,0.5f,0));
		}
	}
}
