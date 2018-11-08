using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerVibrationEvtCtlr : RuntimeMonoBehaviour
{

    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private PlayerInputCtlr m_input;
    private float m_delayDeath = .5f;
    private float m_delayDamage = .5f;

    private float m_maxIntensityStun = 1f;
    private float m_maxIntensityDeath = 4f;

    // ======================================================================================
    protected override void StartPhase()
    {
        base.StartPhase();
        m_input = this.gameObject.GetComponent<PlayerInputCtlr>();
    }

    // ======================================================================================
    public void MSG_Death()
    {
        StartCoroutine(Vibrate(m_delayDeath, m_maxIntensityDeath));
    }

    // ======================================================================================
    public void MSG_Damage()
    {
        StartCoroutine(Vibrate(m_delayDamage, m_maxIntensityStun));
    }

    // ======================================================================================
    public void MSG_Stun(float _stunDelay)
    {
        StartCoroutine(Vibrate(_stunDelay, m_maxIntensityStun));
    }

    // ======================================================================================
    private IEnumerator Vibrate(float _delay, float _maxIntesity)
    {
        float timer = _delay;
        float intensity = _maxIntesity;

        while (timer > 0)
        {
            if (timer/_delay < .8f)
                intensity = Mathf.Lerp(0, intensity, timer / _delay);

            InputMgr.VibrateController((int)m_input.m_nbPlayer, intensity, intensity);

            yield return null;

            timer -= Time.deltaTime;
        }
    }
}
