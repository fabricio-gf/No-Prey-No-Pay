using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectInputController : MenuInputController {

	private bool[] m_submitTriggerList = new bool[4];
	private bool[] m_previousTriggerList = new bool[4];
	private bool[] m_pauseTriggerList = new bool[4];
	private bool[] m_changeColorTriggerList = new bool[4];

	void Start(){
		for(int i = 0; i < 4; i++){
			m_submitTriggerList[i] = false;
			m_previousTriggerList[i] = false;
			m_pauseTriggerList[i] = false;
			m_changeColorTriggerList[i] = false;

		}
	}

	public override void Update()
    {
		for(int i = 0; i < 4; i++){
        // reset triggers when button released
        m_submitTriggerList[i]     = !InputManager.GetMenuButton(i, InputManager.eMenuButton.SUBMIT)      || m_submitTriggerList[i];
        m_previousTriggerList[i]   = !InputManager.GetMenuButton(i, InputManager.eMenuButton.PREVIOUS)    || m_previousTriggerList[i];
        m_pauseTriggerList[i]      = !InputManager.GetMenuButton(i, InputManager.eMenuButton.PAUSE)       || m_pauseTriggerList[i];
        m_changeColorTriggerList[i] = !InputManager.GetMenuButton(i, InputManager.eMenuButton.CHANGE_COLOR) || m_changeColorTriggerList[i];
		}
	}
	    
    public bool GetSubmit(int player)
    {
        if (m_submitTriggerList[player] && InputManager.GetMenuButton(player, InputManager.eMenuButton.SUBMIT))
        {
            m_submitTriggerList[player] = false;
            return true;
        }

        return false;
    }

	
    public bool GetPrevious(int player)
    {
        if (m_previousTriggerList[player] && InputManager.GetMenuButton(player, InputManager.eMenuButton.PREVIOUS))
        {
            m_previousTriggerList[player] = false;
            return true;
        }

        return false;
    }

	
    public bool GetPause(int player)
    {
        if (m_pauseTriggerList[player] && InputManager.GetMenuButton(player, InputManager.eMenuButton.PAUSE))
        {
            m_pauseTriggerList[player] = false;
            return true;
        }

        return false;
    }


    public bool GetChangeColor(int player)
    {
        if (m_changeColorTriggerList[player] && InputManager.GetMenuButton(player, InputManager.eMenuButton.CHANGE_COLOR))
        {
            m_changeColorTriggerList[player] = false;
            return true;
        }

        return false;
    }
}
