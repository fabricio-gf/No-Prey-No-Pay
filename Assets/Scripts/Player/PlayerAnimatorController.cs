using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    // --------------------------------- PUBLIC AUX ENUMS -------------------------------- //
    public enum eStates
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Dashing,
        Attack,
        WallSliding,
        //LedgeGrabbed,
        //LedgeMoving,
        WallEjecting,
        Dead,
    }

    public enum eDirections
    {
        Left,
        Right
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

    // on death
    private string   m_onDeathTriggerParam  = "OnDeath";
    
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private Transform m_transform;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake()
    {
        if (m_animator == null)
            m_animator = this.GetComponent<Animator>();

        Debug.Assert(m_animator != null, this.gameObject.name + " - PlayerAnimatorController : MISSING ANIMATOR!");

        m_transform = this.transform;
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

            case eStates.Attack:
                StartAttacking();
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
        }
    }

    // ======================================================================================
    public void SetDirection(eDirections _direction)
    {
        // TODO : Create Blend Trees for != directions
        switch(_direction)
        {
            case eDirections.Left:
                m_transform.localScale = new Vector3(-1, 1, 1);
                break;
            case eDirections.Right:
                m_transform.localScale = new Vector3(1, 1, 1);
                break;
        }
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
    }

    // ======================================================================================
    protected void StartAttacking()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, true);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
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
    }

    // ======================================================================================
    protected void StartLedgeGrabbed()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, true);
        m_animator.SetBool(m_isLedgeMoving, false);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        // TODO
    }

    // ======================================================================================
    protected void StartLedgeMoving()
    {
        m_animator.SetBool(m_isDashingBoolParam, false);
        m_animator.SetBool(m_isFallingBoolParam, false);
        m_animator.SetBool(m_isJumpingBoolParam, false);
        m_animator.SetBool(m_isRunningBoolParam, false);
        m_animator.SetBool(m_isAttackingBoolParam, false);
        m_animator.SetBool(m_isSlidingBoolParam, false);
        m_animator.SetBool(m_isLedgeGrabbed, false);
        m_animator.SetBool(m_isLedgeMoving, true);
        m_animator.SetBool(m_isEjectingBoolParam, false);
        // TODO
    }

    // ======================================================================================
    protected void StartDead()
    {
        m_animator.SetTrigger(m_onDeathTriggerParam);
    }
}
