using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[RequireComponent(typeof(MenuInputController))]
public class LoadSceneInPauseButton : ScenePicker
{
    // ======================================================================================
    private MenuInputController      m_input;

    // ======================================================================================
    public void Start()
    {
        m_input = this.gameObject.GetComponent<MenuInputController>();
    }

    // ======================================================================================
    public void Update()
    {
        if (m_input.GetPause())
            SceneManager.LoadScene(scenePath);
    }
}