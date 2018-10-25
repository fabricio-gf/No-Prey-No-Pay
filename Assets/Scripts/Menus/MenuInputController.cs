using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputController : MonoBehaviour
{
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private bool m_submitTrigger;
    private bool m_previousTrigger;
    private bool m_pauseTrigger;
    private bool m_changeColorTrigger;

    private bool m_upTrigger;
    private bool m_downTrigger;
    private bool m_leftTrigger;
    private bool m_rightTrigger;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    
    public void Awake(){
        
    }
    public virtual void Update()
    {
        // reset triggers when button released
        m_submitTrigger     = !InputManager.GetMenuButton(InputManager.eMenuButton.SUBMIT)      || m_submitTrigger;
        m_previousTrigger   = !InputManager.GetMenuButton(InputManager.eMenuButton.PREVIOUS)    || m_previousTrigger;
        m_pauseTrigger      = !InputManager.GetMenuButton(InputManager.eMenuButton.PAUSE)       || m_pauseTrigger;
        m_changeColorTrigger = !InputManager.GetMenuButton(InputManager.eMenuButton.CHANGE_COLOR) || m_changeColorTrigger;


        m_upTrigger         = !InputManager.GetMenuButton(InputManager.eMenuButton.UP)          || m_upTrigger;
        m_downTrigger       = !InputManager.GetMenuButton(InputManager.eMenuButton.DOWN)        || m_downTrigger;
        m_leftTrigger       = !InputManager.GetMenuButton(InputManager.eMenuButton.LEFT)        || m_leftTrigger;
        m_rightTrigger      = !InputManager.GetMenuButton(InputManager.eMenuButton.RIGHT)       || m_rightTrigger;
    }

    // ======================================================================================
    public bool GetSubmit()
    {
        if (m_submitTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.SUBMIT))
        {
            m_submitTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetPrevious()
    {
        if (m_previousTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.PREVIOUS))
        {
            m_previousTrigger = false;
            return true;
        }

        return false;
    }


    // ======================================================================================
    public bool GetPause()
    {
        if (m_pauseTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.PAUSE))
        {
            m_pauseTrigger = false;
            return true;
        }

        return false;
    }
    // ======================================================================================
    public bool GetChangeColor()
    {
        if (m_changeColorTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.CHANGE_COLOR))
        {
            m_changeColorTrigger = false;
            return true;
        }

        return false;
    }


    // ======================================================================================
    public bool GetUp()
    {
        if (m_upTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.UP))
        {
            m_upTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetDown()
    {
        if (m_downTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.DOWN))
        {
            m_downTrigger = false;
            return true;
        }

        return false;
    }

    // ======================================================================================
    public bool GetLeft()
    {
        if (m_leftTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.LEFT))
        {
            m_leftTrigger = false;
            return true;
        }

        return false;
    }


    // ======================================================================================
    public bool GetRight()
    {
        if (m_rightTrigger && InputManager.GetMenuButton(InputManager.eMenuButton.RIGHT))
        {
            m_rightTrigger = false;
            return true;
        }

        return false;
    }
}
