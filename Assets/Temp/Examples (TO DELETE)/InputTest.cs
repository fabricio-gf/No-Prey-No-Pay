using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private MenuInputController m_input;

    private void Start()
    {
        m_input = this.gameObject.GetComponent<MenuInputController>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_input.GetChangeColor())
            Debug.Log("change color");
        if (m_input.GetSubmit())
            Debug.Log("submit");
        if (m_input.GetPrevious())
            Debug.Log("previous");
        if (m_input.GetPause())
            Debug.Log("pause");
        if (m_input.GetUp())
            Debug.Log("up");
        if (m_input.GetDown())
            Debug.Log("down");
        if (m_input.GetLeft())
            Debug.Log("left");
        if (m_input.GetRight())
            Debug.Log("right");
    }
}
