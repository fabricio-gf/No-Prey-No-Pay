using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePickUp : MonoBehaviour
{
    public GameObject m_objGround;
    public WeaponWatcher m_objHand;

    public float m_duration;
    [Range(0f,1f)]
    public float m_ratio;

    private float m_timer;

    public void Start()
    {
        m_timer = 0;
        m_objHand.Drop();
    }

    public void Update()
    {
        if (m_timer > m_duration)
        {
            m_objGround.SetActive(true);
            m_objHand.Drop();
            m_timer = 0;
        }

        if (m_timer > m_ratio * m_duration)
        {
            m_objGround.SetActive(false);
            m_objHand.PickPistol();
        }

        m_timer += Time.deltaTime;

    }
}
