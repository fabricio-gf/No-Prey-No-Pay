using UnityEngine;

public class PlayerAnimatorController : RuntimeMonoBehaviour
{
    // --------------------------------- PUBLIC AUX ENUMS -------------------------------- //
    public enum eStates
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Dashing,
        WallSliding,
        //LedgeGrabbed,
        //LedgeMoving,
        WallEjecting,
        Dead,
        Stunned
    }

    public enum eDirections
    {
        Left,
        Right
    }

    public enum eAttackType
    {
        Fists   = 0,
        Pistol  = 1,
        Saber   = 2
    }

    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public Animator m_animator;

    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    // basic locomotion
    private string m_isRunningBoolParam     = "IsRunning";
    private string m_isDashingBoolParam     = "IsDashing";
    private string m_isJumpingBoolParam     = "IsJumping";
    private string m_isFallingBoolParam     = "IsFalling";

    // special locomotion
    private string m_isEjectingBoolParam    = "IsEjecting";
    private string m_isSlidingBoolParam     = "IsSliding";
    private string m_isLedgeGrabbed         = "IsLedgeGrabbed";
    private string m_isLedgeMoving          = "IsLedgeMoving";

    // attack
    private string m_isAttackingBoolParam   = "IsAttacking";
    private string m_attackTypeParam        = "AttackType";
    private string m_upDirectionParam       = "UpDirection";
    private string m_onAirParam             = "OnAirInAttk";

    // stun
    private string m_isStunnedBoolParam     = "IsStunned";

    // on death
    private string   m_onDeathTriggerParam  = "OnDeath";
    
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private Transform m_transform;
    private eDirections m_currDir;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake()
    {
        if (m_animator == null)
            m_animator = this.GetComponent<Animator>();

        Debug.Assert(m_animator != null, this.gameObject.name + " - PlayerAnimatorController : MISSING ANIMATOR!");

        m_transform = this.transform;
        m_currDir = eDirections.Right;
    }

    protected override void UpdatePhase()
    {
        base.UpdatePhase();
        m_animator.speed = GameMgr.TimeRatio;
    }
    // ======================================================================================
    public void SetState(eStates _state)
    {
        switch (_state)
        {
            case eStates.Idle:
                StartIdle();
                break;

            case eStates.Walking:
                StartWalking();
                break;

            case eStates.Jumping:
                StartJumping();
                break;

            case eStates.Falling:
                StartFalling();
                break;

            case eStates.Dashing:
                StartDashing();
                break;

            // TODO
            case eStates.WallSliding:
                StartWallSliding();
                break;

            case eStates.WallEjecting:
                StartWallEjecting();
                break;

            //case eStates.LedgeGrabbed:
            //    StartLedgeGrabbed();
            //    break;
            //case eStates.LedgeMoving:
            //    StartLedgeMoving();
            //    break;
            case eStates.Dead:
                StartDead();
                break;

            case eStates.Stunned:
                StartStunned();
                break;
        }
    }

    // ======================================================================================
    public void SetDirection(eDirections _direction)
    {
        // TODO : Create Blend Trees for != directions
        //switch(_direction)
        //{
        //    case eDirections.Left:
        //        m_transform.localScale = new Vector3(-1, 1, 1);
        //        break;
        //    case eDirections.Right:
        //        m_transform.localScale = new Vector3(1, 1, 1);
        //        break;
        //}
        if (m_currDir != _direction)
        {
            Vector3 scale = m_transform.localScale;
            scale.x *= -1;
            m_transform.localScale = scale;
            m_currDir = _direction;
        }
    }

    // ======================================================================================
    public void StartAttack(eAttackType _attackType, float _upDirection, bool _onAir)
    {
        m_animator.SetBool(m_isAttackingBoolParam, true);
        m_animator.SetFloat(m_onAirParam, _onAir ? 1.0f : 0.0f);

        m_animator.SetFloat(m_attackTypeParam, (int)_attackType);
        m_animator.SetFloat(m_upDirectionParam, _upDirection);

    }

    // ======================================================================================
    public void StopAttack()
    {
        m_animator.SetBool(m_isAttackingBoolParam, false);
    }

    // ======================================================================================
    // PROTECTED MEMBERS
    // ======================================================================================
    protected void StartIdle()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartWalking()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, true);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartJumping()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, true);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartFalling()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, true);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartDashing()
    {
        m_animator.SetBool(m_isDashingBoolParam, true);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartAttacking()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartWallSliding()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, true);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }

    // ======================================================================================
    protected void StartWallEjecting()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, true);
        m_animator.SetBool(m_isStunnedBoolParam, false);
    }
    
    // ======================================================================================
    protected void StartDead()
    {
        m_animator.SetTrigger(m_onDeathTriggerParam);
    }

    // ======================================================================================
    protected void StartStunned()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        m_animator.SetBool(m_isStunnedBoolParam, true);
    }

    // ======================================================================================
    protected override void OnPlay()
    {
        m_animator.speed = 1;
    }

    // ======================================================================================
    protected override void OnPause()
    {
        m_animator.speed = 0;
    }
}
