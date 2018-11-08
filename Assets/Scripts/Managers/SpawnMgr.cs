using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMgr : RuntimeMonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public Object[]             m_objectsToSpawn;
    [Range(1, 10)]
    public float                m_spawnCooldown = 5f;

    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private static  SpawnMgr    m_manager;

    private List<SpawnPoint>    m_spawnPoints = new List<SpawnPoint>();

    private float               m_timer;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    override protected void StartPhase ()
    {
        Debug.Assert(m_manager == null, " - SpawnMgr - Managers must be unique!");
        m_manager = this;

        Debug.Assert(m_objectsToSpawn.Length > 0, " - SpawnMgr - spawn objects list is empty!");

        m_timer = m_spawnCooldown;
	}

    // ======================================================================================
    override protected void UpdatePhase ()
    {
        List<SpawnPoint> spawnPoints = GetFreeSpawnPoints();

        if (spawnPoints.Count > 0)
        {
            m_timer -= GameMgr.DeltaTime;
            if (m_timer <= 0)
            {
                SpawnRandomObj(spawnPoints[Random.Range(0, spawnPoints.Count)]);
                m_timer = m_spawnCooldown;
            }
        }
	}

    // ======================================================================================
    public static void RegisterSpawnPoint(SpawnPoint _spawnPoint)
    {
#if UNITY_EDITOR
        Debug.Assert(m_manager != null, "SpawnMgr - Manager missing in Scene!");
#endif

        if (_spawnPoint == null)
            return;

        m_manager.m_spawnPoints.Add(_spawnPoint);
        m_manager.SpawnRandomObj(_spawnPoint);      // automatic 1st spawn
    }


    // ======================================================================================
    // PRIVATE MEMBERS
    // ======================================================================================
    private List<SpawnPoint> GetFreeSpawnPoints()
    {
        List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        foreach (SpawnPoint sp in m_spawnPoints)
            if (sp.IsEmpty())
                spawnPoints.Add(sp);

        return spawnPoints;
    }

    private void SpawnRandomObj(SpawnPoint _point)
    {
        Object objToSpawn       = m_objectsToSpawn[Random.Range(0, m_objectsToSpawn.Length)];

        _point.SpawnObj(objToSpawn);
    }
}
