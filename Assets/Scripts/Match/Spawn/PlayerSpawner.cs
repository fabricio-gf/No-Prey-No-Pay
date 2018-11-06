using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    // SINGLETON
    private static PlayerSpawner instance;

	[SerializeField] private Vector2[] SpawnPoints = new Vector2[4];
	[SerializeField] private GameObject[] CharPrefabs;
	[SerializeField] private Transform PlayerParentObject;

    private List<GameObject> m_spawnnedPlayers = new List<GameObject>();

    // ACCESSORS
    public static List<GameObject> SpawnnedPlayers { get { return instance.m_spawnnedPlayers; } }

    public void Awake()
    {
        Debug.Assert(instance == null, this.gameObject.name + " - PlayerSpawnner must be unique!");
        instance = this;
    }

    public static void SpawnPlayers(List<PlayerInfo> PlayersToSpawn) {

        instance.m_spawnnedPlayers.Clear();

        int i = 0;
		foreach(var pi in PlayersToSpawn){
			var obj = Instantiate(instance.CharPrefabs[(int)pi.SelectedCharacter], instance.SpawnPoints[i], Quaternion.identity, instance.PlayerParentObject);
			obj.GetComponent<ChangeColor>().color = pi.SelectedColor;
			obj.GetComponent<ChangeColor>().ManualValidate();
			obj.GetComponent<PlayerInputCtlr>().m_nbPlayer = (PlayerInputCtlr.ePlayer)(pi.ControllerNumber+1);
			i++;

            instance.m_spawnnedPlayers.Add(obj);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		for(int i = 0; i < 4; i++){
        	Gizmos.DrawCube((Vector3)SpawnPoints[i], new Vector3(0.5f,0.5f,0));
		}
	}
}
