using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeThrow : MonoBehaviour
{

    public GameObject m_objThrow;
    public WeaponWatcher m_objHand;

    public float m_duration;
    [Range(0f, 1f)]
    public float m_ratio;

    private float m_timer;

    public void Start()
    {
        m_timer = 0;
        m_objHand.PickSaber();
    }

    public void Update()
    {
        if (m_timer > m_duration)
        {
            m_objThrow.SetActive(false);
            m_objHand.PickSaber();
            m_timer = 0;
        }

        if (m_timer > m_ratio * m_duration)
        {
            m_objThrow.SetActive(true);
            m_objThrow.transform.Rotate(new Vector3(0,0,100000 * Time.deltaTime));

            m_objHand.Drop();
        }

        m_timer += Time.deltaTime;
    }
}
