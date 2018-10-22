using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputCtlr : MonoBehaviour
{
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private bool m_submitTrigger;
    private bool m_previousTrigger;
    private bool m_pauseTrigger;

    private bool m_upTrigger;
    private bool m_downTrigger;
    private bool m_leftTrigger;
    private bool m_rightTrigger;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Update()
    {
        // reset triggers when button released
        m_submitTrigger     = !InputMgr.GetMenuButton(InputMgr.eMenuButton.SUBMIT)      || m_submitTrigger;
        m_previousTrigger   = !InputMgr.GetMenuButton(InputMgr.eMenuButton.PREVIOUS)    || m_previousTrigger;
        m_pauseTrigger      = !InputMgr.GetMenuButton(InputMgr.eMenuButton.PAUSE)       || m_pauseTrigger;

        m_upTrigger         = !InputMgr.GetMenuButton(InputMgr.eMenuButton.UP)          || m_upTrigger;
        m_downTrigger       = !InputMgr.GetMenuButton(InputMgr.eMenuButton.DOWN)        || m_downTrigger;
        m_leftTrigger       = !InputMgr.GetMenuButton(InputMgr.eMenuButton.LEFT)        || m_leftTrigger;
        m_rightTrigger      = !InputMgr.GetMenuButton(InputMgr.eMenuButton.RIGHT)       || m_rightTrigger;
    }

    // ======================================================================================
    public bool GetSubmit()
    {
        if (m_submitTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.SUBMIT))
        {
            m_submitTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetPrevious()
    {
        if (m_previousTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.PREVIOUS))
        {
            m_previousTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetPause()
    {
        if (m_pauseTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.PAUSE))
        {
            m_pauseTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetUp()
    {
        if (m_upTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.UP))
        {
            m_upTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetDown()
    {
        if (m_downTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.DOWN))
        {
            m_downTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetLeft()
    {
        if (m_leftTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.LEFT))
        {
            m_leftTrigger = false;
            return true;
        }

        return false;
    }


    // ======================================================================================
    public bool GetRight()
    {
        if (m_rightTrigger && InputMgr.GetMenuButton(InputMgr.eMenuButton.RIGHT))
        {
            m_rightTrigger = false;
            return true;
        }

        return false;
    }
}
