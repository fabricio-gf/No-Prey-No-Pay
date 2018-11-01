using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : RuntimeMonoBehaviour
{
    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private Transform   m_transform;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    protected override void StartPhase()
    {
        m_transform = this.transform;

        Debug.Assert(IsEmpty(), this.gameObject.name + " - SpawnPoint : No Object other than its spawnned objs can be in hierarchy");

        SpawnMgr.RegisterSpawnPoint(this);
    }

    // ======================================================================================
    public void SpawnObj (Object _toSpawn)
    {
        GameObject.Instantiate(_toSpawn, m_transform);
    }

    // ======================================================================================
    public bool IsEmpty()
    {
        return m_transform.childCount == 0;
    }

}
