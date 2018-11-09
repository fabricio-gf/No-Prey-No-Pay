using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAndHide : MonoBehaviour
{
    public GameObject m_objToBlink;

    public float m_blinkDuration;
    public AnimationCurve m_blinks;
    public float m_blinkThres;
    
    private float m_timer;

	// Use this for initialization
	void Start ()
    {
        m_timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;
        m_objToBlink.SetActive (m_blinks.Evaluate(m_timer) > m_blinkThres ? true : false);

        if (m_timer > m_blinkDuration)
        {
            m_objToBlink.SetActive(false);
            this.enabled = false;
        }
	}
}
