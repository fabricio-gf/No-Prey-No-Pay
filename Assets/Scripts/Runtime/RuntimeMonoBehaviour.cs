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

        if (!m_isPaused)
            OnPlay();
        else
            OnPause();
	}

    // ======================================================================================
    public void Update ()
    {
        if (IsPaused())
            return;

        UpdatePhase();
	}

    // ======================================================================================
    public void FixedUpdate()
    {
        if (IsPaused())
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

    // ======================================================================================
    // PROTECTED MEMBERS
    // ======================================================================================
    protected bool IsPaused()
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
        return m_isPaused;
    }
}
