using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerSoundEvtController : RuntimeMonoBehaviour
{
    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private PlayerStateMachine.eStates  m_state;
    private PlayerStateMachine          m_stateMachine;

    // ======================================================================================
    // PROTECTED MEMBERS
    // ======================================================================================
    protected override void StartPhase()
    {
        base.StartPhase();
        m_stateMachine  = this.gameObject.GetComponent<PlayerStateMachine>();
        m_state         = m_stateMachine.State;
    }

    // ======================================================================================
    protected override void UpdatePhase()
    {
        base.UpdatePhase();

        if (m_state != m_stateMachine.State)
        {
            m_state = m_stateMachine.State;

            switch (m_state)
            {
                case PlayerStateMachine.eStates.Attack:
                    switch (m_stateMachine.HandWeapon)
                    {
                        case PlayerAttack.eWeapon.Fists:
                            SoundMgr.PlaySoundEvent(SoundMgr.eSoundEventType.FistAttk);
                            break;
                        case PlayerAttack.eWeapon.Saber:
                            SoundMgr.PlaySoundEvent(SoundMgr.eSoundEventType.SaberAttk);
                            break;
                        case PlayerAttack.eWeapon.Pistol:
                            SoundMgr.PlaySoundEvent(SoundMgr.eSoundEventType.PistolAttk);
                            break;
                    }
                    break;
                case PlayerStateMachine.eStates.Dashing:
                    SoundMgr.PlaySoundEvent(SoundMgr.eSoundEventType.Dash);
                    break;
                case PlayerStateMachine.eStates.Dead:
                    SoundMgr.PlaySoundEvent(SoundMgr.eSoundEventType.Death);
                    break;
            }

        }
    }
}