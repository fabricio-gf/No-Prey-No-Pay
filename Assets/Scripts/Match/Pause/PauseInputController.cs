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
        base.Update();

		for(int i = 0; i < 4; i++){
        	// reset triggers when button released
        	m_pauseTriggerList[i] = !InputMgr.GetMenuButton(i + 1, InputMgr.eMenuButton.PAUSE) || m_pauseTriggerList[i];
		}
	}

    public bool GetPause(int player)
    {
        if (m_pauseTriggerList[player - 1] && InputMgr.GetMenuButton(player, InputMgr.eMenuButton.PAUSE))
        {
            m_pauseTriggerList[player - 1] = false;
            return true;
        }

        return false;
    }

}
