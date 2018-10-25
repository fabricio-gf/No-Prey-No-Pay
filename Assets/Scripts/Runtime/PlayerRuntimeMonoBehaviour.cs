using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuntimeMonoBehaviour : RuntimeMonoBehaviour
{
    // ======================================================================================
    // PUBLIC METHODS - PLAYER MSGS
    // ======================================================================================
    public void MSG_OnExclusiveEventStart(MonoBehaviour _respo)
    {
        if (_respo != this)
            IsActive = false;
    }

    // ======================================================================================
    public void MSG_OnExclusiveEventEnd(MonoBehaviour _respo)
    {
        if (_respo != this)
            IsActive = true;
    }
}
