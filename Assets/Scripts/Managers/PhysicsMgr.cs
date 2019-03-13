using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMgr : RuntimeMonoBehaviour
{
    // ======================================================================================
    protected override void StartPhase()
    {
        Physics2D.autoSimulation = true;
    }

    // ======================================================================================
    protected override void OnPause()
    {
        Physics2D.autoSimulation = false;
    }

    // ======================================================================================
    protected override void OnPlay()
    {
        Physics2D.autoSimulation = true;
    }
}
