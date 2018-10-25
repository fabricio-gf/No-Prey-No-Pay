using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuntimeMonoBehaviour : RuntimeMonoBehaviour
{
    // --------------------------------- PRIVATE ATTRIBUTES ------------------------------ //
    private bool m_waitingExclusiveEvt  = false;

    // ======================================================================================
    // PUBLIC METHODS - PLAYER MSGS
    // ======================================================================================
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
    // PROTECTED METHODS
    // ======================================================================================
    protected override bool IsActive()
    {
        return base.IsActive() && !m_waitingExclusiveEvt;
    }
}
