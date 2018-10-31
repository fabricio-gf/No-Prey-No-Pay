using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : RuntimeMonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public GameObject m_ground;
    
    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private static SceneMgr m_manager;
    
    // ------------------------------------ ACCESSORS ------------------------------------ //
    public static GameObject Ground { get { return m_manager.m_ground; } }

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    override protected void StartPhase ()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - SceneMgr : manager must be unique!");
        m_manager = this;
        Debug.Assert(m_ground != null, this.gameObject.name + " - SceneMgr : scene must have a global ground!");
	}
}
