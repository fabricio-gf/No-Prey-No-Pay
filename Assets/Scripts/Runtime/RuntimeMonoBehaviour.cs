using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeMonoBehaviour : MonoBehaviour
{
    private bool m_isPaused = false;
    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start ()
    {
        StartPhase();
	}

    // ======================================================================================
    public void Update ()
    {
        // handle pause state
        if (m_isPaused && (!GameMgr.IsPaused) && IsActive())
        {
            m_isPaused = false;
            OnPlay();
        }
        else if (!m_isPaused && (GameMgr.IsPaused || !IsActive()))
        {
            m_isPaused = true;
            OnPause();
        }

        // return if paused or inactive
        if (m_isPaused)
            return;


        UpdatePhase();
	}

    // ======================================================================================
    public void FixedUpdate()
    {
        if (GameMgr.IsPaused || !IsActive())
            return;

        FixedUpdatePhase();
    }

    // ======================================================================================
    // PROTECTED MEMBERS - TO OVERRIDE
    // ======================================================================================
    protected virtual bool IsActive()
    {
        return enabled;
    }

    // ======================================================================================
    // BASIC MESSAGES
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

    // ======================================================================================
    // ACTIVATION MESSAGES MESSAGES
    protected virtual void OnPause()
    {

    }

    // ======================================================================================
    protected virtual void OnPlay()
    {

    }
}
