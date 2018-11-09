using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAndHide : MonoBehaviour
{
    public float m_blinkDuration;
    public AnimationCurve m_blinks;
    public float m_blinkThres;

    private Canvas m_canvas;
    private float m_timer;

	// Use this for initialization
	void Start ()
    {
        m_canvas = this.gameObject.GetComponent<Canvas>();
        m_timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;
        m_canvas.enabled = m_blinks.Evaluate(m_timer) > m_blinkThres ? true : false;

        if (m_timer > m_blinkDuration)
        {
            m_canvas.enabled = false;
            this.enabled = false;
        }
	}
}
