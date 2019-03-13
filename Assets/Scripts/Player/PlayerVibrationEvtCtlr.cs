using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerVibrationEvtCtlr : RuntimeMonoBehaviour
{
    // ----------------------------------- PUBLIC TEST ----------------------------------- //
    public AnimationCurve m_intensity = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));

    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private PlayerInputCtlr m_input;
    private float m_delayDeath = .5f;
    private float m_delayDamage = .5f;

    private float m_maxIntensityStun = 4f;
    private float m_maxIntensityDeath = 10f;
    private float m_maxIntensityDamage = 7f;

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
        StartCoroutine(Vibrate(m_delayDamage, m_maxIntensityDamage));
    }

    // ======================================================================================
    public void MSG_Stun(float _stunDelay)
    {
        StartCoroutine(Vibrate(_stunDelay, m_maxIntensityStun));
    }

    // ======================================================================================
    private IEnumerator Vibrate(float _delay, float _maxIntesity)
    {
        float timer = 0;

        while (timer < _delay)
        {
            InputMgr.VibrateController((int)m_input.m_nbPlayer, m_intensity.Evaluate(timer/_delay)*_maxIntesity, m_intensity.Evaluate(timer/_delay)*_maxIntesity);

            yield return null;

            timer += Time.deltaTime;
        }
    }
}
