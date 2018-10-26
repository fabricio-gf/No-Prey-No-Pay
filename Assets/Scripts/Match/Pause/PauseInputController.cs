using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInputController : MenuInputController {

	private bool[] m_pauseTriggerList = new bool[4];

	void Start(){
		for(int i = 0; i < 4; i++){
			m_pauseTriggerList[i] = false;
		}
	}

	public override void Update()
    {
		for(int i = 0; i < 4; i++){
        	// reset triggers when button released
        	m_pauseTriggerList[i]      = !InputManager.GetMenuButton(i, InputManager.eMenuButton.PAUSE)       || m_pauseTriggerList[i];
		}
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

}
