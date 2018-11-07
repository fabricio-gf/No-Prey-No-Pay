using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : PlayerRuntimeMonoBehaviour {
    // ------------------------------- PROTECTED ATTRIBUTES ------------------------------ //
    protected int   m_nbLives;
    protected float m_stunDuration = 0.5f;

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    // damaged player
    private PlayerInputCtlr m_player;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public bool IsDead      { get; protected set; }
    public bool IsStunned   { get; protected set; }

    // ======================================================================================
    // PUBLIC MEMBERS - DAMAGE METHODS
    // ======================================================================================
    override protected void StartPhase()
    {
        m_player = this.GetComponentInParent<PlayerInputCtlr>();
        m_nbLives = 1;

        IsDead      = false;
        IsStunned   = false;
    }

    public void RestartPhase(){
        m_nbLives = 3;

        IsDead      = false;
        IsStunned   = false;
    }

    // ======================================================================================
    public void TakeDamage(PlayerInputCtlr.ePlayer player)
    {
        print(m_player.m_nbPlayer + " is taking damage");
        if (!IsActive())
        {
            return;
        }

        if (player != m_player.m_nbPlayer)
        {
            m_nbLives--;
            print(m_player.m_nbPlayer + "is hurt");

            if (m_nbLives <= 0)
            {
                //m_player.enabled = !m_player.enabled;
                IsDead = true;
                m_player.gameObject.SendMessage("MSG_Death");
                //m_deathSFX.Play();
                //gameReferee.addScore(100, (int)player);
                //gameReferee.murderWitness((int)m_player.m_nbPlayer);
                //m_animator.SetState(PlayerAnimatorController.eStates.Dead);
            }
            else
            {
                // m_damageSFX.Play();
            }
        }
    }

    public IEnumerator GetStunned()
    {
        this.gameObject.SendMessage("MSG_OnExclusiveEventStart", this);

        IsStunned = true;
        print(m_player + " is stunned");
        m_player.enabled = !m_player.enabled;
        yield return new WaitForSeconds(m_stunDuration);
        m_player.enabled = !m_player.enabled;
        IsStunned = false;

        this.gameObject.SendMessage("MSG_OnExclusiveEventEnd", this);
    }

    protected override bool IsActive()
    {
        return base.IsActive() && !IsDead;//GameMgr.IsPaused || GameMgr.IsGameOver || IsDead;
    }
}
