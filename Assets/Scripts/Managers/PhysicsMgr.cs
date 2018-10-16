using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMgr : MonoBehaviour
{
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static PhysicsMgr m_manager;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        Debug.Assert(m_manager == null, this.name + " - PhysicsMgr : Mgr must be unique!");

        m_manager = this;
    }
}
