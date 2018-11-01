using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    
	[SerializeField] private GameObject PauseWindow;
	private PauseInputController m_input;

	void Start()
    {
		m_input = GetComponent<PauseInputController>();
	}

	// Update is called once per frame
	void Update ()
    {
		GetPauseInput();
	}

	void GetPauseInput()
    {
        bool doPause = false;
		for(int i = 1; i <= 4; i++)
            doPause |= m_input.GetPause(i);

        if (doPause)
        {
            if (GameMgr.IsPaused)
            {
                GameMgr.PlayGame();
                HidePauseWindow();
            }
            else
            {
                GameMgr.PauseGame();
                ShowPauseWindow();
            }
        }
    }

	void ShowPauseWindow()
    {
		PauseWindow.SetActive(true);
	}

    void HidePauseWindow()
    {
        PauseWindow.SetActive(false);
    }
}
