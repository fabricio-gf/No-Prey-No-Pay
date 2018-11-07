using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    // SINGLETON
    private static PlayerSpawner instance;

	[SerializeField] private List<Vector2> SpawnPoints = new List<Vector2>(4);
	[SerializeField] private GameObject[] CharPrefabs;
	[SerializeField] private Transform PlayerParentObject;

    private List<GameObject> m_spawnnedPlayers = new List<GameObject>();
    private List<PlayerInfo> m_spawnnedPlayersInfo = new List<PlayerInfo>();

    // ACCESSORS
    public static List<GameObject> SpawnnedPlayers { get { return instance.m_spawnnedPlayers; } }

    public void Awake()
    {
        Debug.Assert(instance == null, this.gameObject.name + " - PlayerSpawnner must be unique!");
        instance = this;
    }

    public static void SpawnPlayers(List<PlayerInfo> PlayersToSpawn) {

        instance.m_spawnnedPlayers.Clear();
        List<Vector2> shuffledSpawns = instance.SpawnPoints.OrderBy( x => Random.value ).ToList( );

        int i = 0;
		foreach(var pi in PlayersToSpawn){
			var obj = Instantiate(instance.CharPrefabs[(int)pi.SelectedCharacter], shuffledSpawns[i], Quaternion.identity, instance.PlayerParentObject);
			obj.GetComponent<ChangeColor>().color = pi.SelectedColor;
			obj.GetComponent<ChangeColor>().ManualValidate();
			obj.GetComponent<PlayerInputCtlr>().m_nbPlayer = (PlayerInputCtlr.ePlayer)(pi.ControllerNumber+1);
			i++;

            instance.m_spawnnedPlayers.Add(obj);
		}

        instance.m_spawnnedPlayersInfo = PlayersToSpawn;
	}

	public static void RespawnPlayers(){
		for(int i = 0; i < instance.m_spawnnedPlayers.Count; i++){
            //instance.m_spawnnedPlayers[i].transform.position = instance.SpawnPoints[i];
            //instance.m_spawnnedPlayers[i].GetComponent<WeaponPick>().WeaponList = new List<GameObject>();
            //instance.m_spawnnedPlayers[i].GetComponent<PlayerStateMachine>().MSG_Respawn();
            //instance.m_spawnnedPlayers[i].transform.Find("Hurtbox").GetComponent<DamageBehaviour>().RestartPhase();
            //instance.m_spawnnedPlayers[i].GetComponent<PlayerInputCtlr>().enabled = true;
            ////reset other things here
            Destroy(instance.m_spawnnedPlayers[i]);
		}

        SpawnPlayers(instance.m_spawnnedPlayersInfo);
    }

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		for(int i = 0; i < 4; i++){
        	Gizmos.DrawCube((Vector3)SpawnPoints[i], new Vector3(0.5f,0.5f,0));
		}
	}
}
