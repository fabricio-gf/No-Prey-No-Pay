using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // --------------------------------- PUBLIC AUX ENUMS -------------------------------- //
    public enum eStates
    {
        Idle,
        Walking,
        Jumping,        // Normal || Wall
        Falling,
        Dashing,
        //Attack,
        Sliding,        // Wall
        //LedgeGrabbed,
        //LedgeMoving,
        Dead,
    }

    public enum eDirections
    {
        Left,
        Right
    }
    
    public eStates State { get; set; }
}
