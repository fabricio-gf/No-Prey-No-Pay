using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuntimeMonoBehaviour : RuntimeMonoBehaviour
{
    // --------------------------------- PRIVATE ATTRIBUTES ------------------------------ //
    private bool m_waitingExclusiveEvt  = false;
    private bool m_isAlive = true;

    // ======================================================================================
    // PUBLIC METHODS - PLAYER MSGS
    // ======================================================================================
    // EXCLUSIVE EVENT MESSAGES
    // It disables all other PlayerRuntimeMonoBehaviour in the GameObject and give full althority
    // to the caller until a OnExclusiveEventEnd is called
    public void MSG_OnExclusiveEventStart(MonoBehaviour _respo)
    {
        if (_respo != this)
            m_waitingExclusiveEvt = true;
    }

    // ======================================================================================
    public void MSG_OnExclusiveEventEnd(MonoBehaviour _respo)
    {
        if (_respo != this)
            m_waitingExclusiveEvt = false;
    }

    // ======================================================================================
    public void MSG_Death()
    {
        m_isAlive = false;
    }

    // ======================================================================================
    // PROTECTED METHODS
    // ======================================================================================
    protected override bool IsActive()
    {
        return base.IsActive() && !m_waitingExclusiveEvt && m_isAlive;
    }

}
