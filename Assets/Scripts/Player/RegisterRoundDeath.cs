using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputCtlr))]
public class RegisterRoundDeath : MonoBehaviour
{
    public void MSG_Death()
    {
        PlayerInputCtlr playerInputCtlr = this.gameObject.GetComponent<PlayerInputCtlr>();
        RoundReferee.RegisterDeath((int) playerInputCtlr.m_nbPlayer);
    }
}
