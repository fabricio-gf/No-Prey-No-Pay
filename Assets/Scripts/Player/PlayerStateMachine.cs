using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerAnimatorController), typeof(PlayerAttack))]
public class PlayerStateMachine : RuntimeMonoBehaviour
{
    // --------------------------------- PUBLIC AUX ENUMS -------------------------------- //
    public enum eStates
    {
        Idle,
        Walking,
        Jumping,            // Normal
        Falling,
        Dashing,
        Attack,
        WallSliding,        // Wall
        WallEjecting,       // Wall
        //LedgeGrabbed,
        //LedgeMoving,
        Stunned,
        Dead,
    }

    public DamageBehaviour m_damageBhv;
    // ------------------------------- PROTECTED ATTRIBUTES ------------------------------ //
    protected PlayerController          m_playerCtl;
    protected PlayerAttack              m_playerAttack;
    protected PlayerAnimatorController  m_playerAnimCtl;

    //// -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    //private float                       m_minSpeedToWalkAnim    = .2f;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public eStates      State               { get; protected set; }
    public PlayerAttack.eWeapon HandWeapon  { get { return m_playerAttack.EquipWeap; } }


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    override protected void StartPhase()
    {
        State           = eStates.Idle;
        
        m_playerCtl     = this.gameObject.GetComponent<PlayerController>();
        m_playerAttack  = this.gameObject.GetComponent<PlayerAttack>();
        m_playerAnimCtl = this.gameObject.GetComponent<PlayerAnimatorController>();
    }

    // ======================================================================================
    override protected void UpdatePhase()
    {
        UpdateStateMachine();
        UpdateAnimator();
    }

    // ======================================================================================
    protected void UpdateStateMachine()
    {
        if (m_damageBhv.IsDead)
            State       = eStates.Dead;
        else if (m_damageBhv.IsStunned)
            State       = eStates.Stunned;
        else if (m_playerAttack.IsAttacking)
            State       = eStates.Attack;
        else if (m_playerCtl.IsDashing)
            State       = eStates.Dashing;
        else if (m_playerCtl.IsEjecting)
            State       = eStates.WallEjecting;
        else if (m_playerCtl.IsGrounded)
        {
            if (Mathf.Abs(m_playerCtl.Velocity.x) != 0)
                State   = eStates.Walking;
            else
                State   = eStates.Idle;
        }
        else if (m_playerCtl.IsWallSliding)
            State       = eStates.WallSliding;
        else if (m_playerCtl.IsJumping)
        {
            if (m_playerCtl.Velocity.y > 0)
                State   = eStates.Jumping;
            else
                State   = eStates.Falling;
        }
        else if (!m_playerCtl.IsGrounded && m_playerCtl.Velocity.y < 0)
            State       = eStates.Falling;
    }

    // ======================================================================================
    private void UpdateAnimator()
    {
        switch (State)
        {
            case eStates.Attack:
                
                switch (m_playerAttack.EquipWeap)
                {
                    case PlayerAttack.eWeapon.Fists:
                        m_playerAnimCtl.StartAttack(PlayerAnimatorController.eAttackType.Fists, m_playerAttack.AttackDirection.y, m_playerCtl.IsJumping);
                        break;
                    case PlayerAttack.eWeapon.Pistol:
                        m_playerAnimCtl.StartAttack(PlayerAnimatorController.eAttackType.Pistol, m_playerAttack.AttackDirection.y, m_playerCtl.IsJumping);
                        break;
                    case PlayerAttack.eWeapon.Saber:
                        m_playerAnimCtl.StartAttack(PlayerAnimatorController.eAttackType.Saber, m_playerAttack.AttackDirection.y, m_playerCtl.IsJumping);
                        break;
                }
                break;
            case eStates.Idle:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Idle);
                break;
            case eStates.Walking:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Walking);
                break;
            case eStates.Jumping:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Jumping);
                break;
            case eStates.Falling:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Falling);
                break;
            case eStates.WallSliding:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.WallSliding);
                break;
            case eStates.WallEjecting:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.WallEjecting);             // TO DO : Wall Eject
                break;
            case eStates.Dashing:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Dashing);
                break;
            case eStates.Dead:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Dead);
                break;
            case eStates.Stunned:
                m_playerAnimCtl.SetState(PlayerAnimatorController.eStates.Stunned);
                break;
        }

        switch (m_playerCtl.ForwardDir)
        {
            case PlayerController.eDirection.Left:
                m_playerAnimCtl.SetDirection(PlayerAnimatorController.eDirections.Left);
                break;
            case PlayerController.eDirection.Right:
                m_playerAnimCtl.SetDirection(PlayerAnimatorController.eDirections.Right);
                break;
        }
    }
    
    //public void MSG_Death()
    //{
    //    State = eStates.Dead;
    //}

    public void MSG_Respawn()
    {
        State = eStates.Idle;
    }
}
