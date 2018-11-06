using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerInputCtlr : PlayerInputCtlr
{
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private bool m_dashTrigger      = false;
    private bool m_jumpTrigger      = false;
    private bool m_tossTrigger      = false;
    private bool m_attackTrigger    = false;
    private bool m_grabTrigger      = false;

    private bool m_waitingStart     = true;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    protected override void StartPhase()
    {
        base.StartPhase();
        m_waitingStart = true;

        m_dashTrigger   = false;
        m_jumpTrigger   = false;
        m_tossTrigger   = false;
        m_attackTrigger = false;
        m_grabTrigger   = false;
}

    // ======================================================================================
    // PROTECTED MEMBERS
    // ======================================================================================
    override protected void UpdatePhase()
    {
        // reset triggers when button released
        m_dashTrigger   = !InputMgr.GetButton((int)m_nbPlayer, InputMgr.eButton.DASH)   || m_dashTrigger;
        m_jumpTrigger   = !InputMgr.GetButton((int)m_nbPlayer, InputMgr.eButton.JUMP)   || m_jumpTrigger;
        m_grabTrigger   = !InputMgr.GetButton((int)m_nbPlayer, InputMgr.eButton.GRAB)   || m_grabTrigger;
        m_tossTrigger   = !InputMgr.GetButton((int)m_nbPlayer, InputMgr.eButton.TOSS)   || m_tossTrigger;
        m_attackTrigger = !InputMgr.GetButton((int)m_nbPlayer, InputMgr.eButton.ATTACK) || m_attackTrigger;
    }

    // ======================================================================================
    override public bool GetDash()
    {
        if (m_dashTrigger && InputMgr.GetButton((int) m_nbPlayer, InputMgr.eButton.DASH))
        {
            m_dashTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    override public bool GetJump()
    {
        if (m_jumpTrigger && InputMgr.GetButton((int) m_nbPlayer, InputMgr.eButton.JUMP))
        {
            m_jumpTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    override public bool GetToss()
    {
        if (m_tossTrigger && InputMgr.GetButton((int) m_nbPlayer, InputMgr.eButton.TOSS))
        {
            m_tossTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    override public bool GetAttack()
    {
        if (m_attackTrigger && InputMgr.GetButton((int) m_nbPlayer, InputMgr.eButton.ATTACK))
        {
            m_attackTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    override public bool GetGrab()
    {
        if (m_grabTrigger && InputMgr.GetButton((int) m_nbPlayer, InputMgr.eButton.GRAB))
        {
            m_grabTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    override public float GetHorizontal()
    {
        return IsActive() ? InputMgr.GetAxis((int) m_nbPlayer, InputMgr.eAxis.HORIZONTAL) : 0f;
    }

    // ======================================================================================
    override public float GetVertical()
    {
        return IsActive() ? InputMgr.GetAxis((int) m_nbPlayer, InputMgr.eAxis.VERTICAL) : 0f;
    }

    // ======================================================================================
    public void MSG_StartRound()
    {
        m_waitingStart = false;
    }

    // ======================================================================================
    protected override bool IsActive()
    {
        return base.IsActive() && !m_waitingStart;
    }
}
