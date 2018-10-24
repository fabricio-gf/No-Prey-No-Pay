using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInputCtlr : MonoBehaviour
{
    // --------------------------------------- ENUMS ------------------------------------- //
    public enum ePlayer
    {
        Player1 = 1,
        Player2 = 2,
        Player3 = 3,
        Player4 = 4
    };

    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //
    public ePlayer m_nbPlayer = ePlayer.Player1;
    
    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public abstract bool GetDash();
    public abstract bool GetJump();
    public abstract bool GetToss();
    public abstract bool GetAttack();
    public abstract bool GetGrab();

    public abstract float GetHorizontal();
    public abstract float GetVertical();
}
