using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeMonoBehaviour : MonoBehaviour
{
    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //
    public bool IsActive { get; protected set; }

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start ()
    {
        IsActive = true;
        StartPhase();
	}

    // ======================================================================================
    public void Update ()
    {
        if (GameMgr.IsPaused || !IsActive)
            return;

        UpdatePhase();
	}

    // ======================================================================================
    public void FixedUpdate()
    {
        if (GameMgr.IsPaused || !IsActive)
            return;

        FixedUpdatePhase();
    }

    // ======================================================================================
    // PROTECTED MEMBERS - TO OVERRIDE
    // ======================================================================================
    protected virtual void StartPhase()
    {

    }

    // ======================================================================================
    protected virtual void UpdatePhase()
    {

    }

    // ======================================================================================
    protected virtual void FixedUpdatePhase()
    {

    }
}
