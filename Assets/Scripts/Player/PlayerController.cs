using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputCtlr))]
public class PlayerController : MonoBehaviour
{
    // -------------------------------------- ENUMS -------------------------------------- //
    public enum eDirection
    {
        Left,
        Right
    };

    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public PlayerConfig                 m_configData;

    // --------------------------- PROTECTED CONFIG ATTRIBUTES --------------------------- //
    // physics params
    protected float m_gravityRatio         = 1.0f;
    protected float m_gravityMaxSpeed      = 5.0f;

    // jump params
    protected float m_jumpMaxSpeed         = 10;

    // falling params
    protected float m_ratioToWalk          = 1;

    // walking params
    protected float m_walkAcc              = 10;
    protected float m_walkMaxSpeed         = 20;

    // wall params
    protected float m_slideMaxSpeed         = 5;
    // eject params
    protected float m_ejectDist            = 1;
    protected float m_ejectMaxSpeed        = 10;
    protected float m_ejectMinSpeedRatio   = .2f;

    // dash params
    protected float m_dashDist             = 1;
    protected float m_dashMaxSpeed         = 10;
    protected float m_dashMinSpeedRatio    = .2f;


    // -------------------- PROTECTED CONFIG (FINE TUNING) ATTRIBUTES -------------------- //
    protected float m_walkMinSpeedRatio        = .1f;
    protected float m_ejectMinSpeed            = .1f;
    protected float m_dashMinSpeed             = .1f;


    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private Rigidbody2D         m_rb;
    private PlayerInputCtlr     m_input;
    private Vector2             m_snappedWallNormal;

    // jump Subsystem
    private float               m_ejectTargetPosX     = 0;
    private float               m_ejectDirectionX     = 0;

    // dash subsystem
    private Vector2             m_dashTargetPos      = Vector2.zero;
    private Vector2             m_dashDirection      = Vector2.zero;

    // fine tunning : events
    private float               m_minDashEventRatio  = .1f;
    private float               m_minEjectEventRatio = .1f;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public bool IsGrounded      { get; protected set; }
    public bool IsJumping       { get; protected set; }
    public bool IsWallSnapped   { get; protected set; }
    public bool IsWallSliding   { get { return IsWallSnapped && m_input.GetHorizontal() * m_snappedWallNormal.x < 0 && m_rb.velocity.y < 0; } }
    public bool IsEjecting      { get; protected set; }     // exclusive unstopabble event
    public bool IsDashing       { get; protected set; }     // exclusive unstopable event
    public eDirection ForwardDir{ get; protected set; }

    public Vector2 Velocity     { get { return m_rb.velocity; } }


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start ()
    {
        InitializeValues();

        m_rb            = this.GetComponent<Rigidbody2D>();
        m_input         = this.GetComponent<PlayerInputCtlr>();

        IsGrounded      = false;
        IsWallSnapped   = false;
        IsEjecting      = false;
        IsDashing       = false;

        ForwardDir      = eDirection.Right;
    }

    // ======================================================================================
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Enter : " + collision.collider.gameObject.tag);
        switch (collision.collider.gameObject.tag)
        {
            case "Floor":
                IsWallSnapped   = IsWallSnapped;
                IsEjecting      = false;
                IsJumping       = false;
                IsDashing       = IsDashing;
                IsGrounded      = true;
                break;
            case "Wall":
                IsWallSnapped   = true;
                IsEjecting      = false;
                IsJumping       = false;
                IsDashing       = false;
                IsGrounded      = IsGrounded;
                m_snappedWallNormal = collision.GetContact(0).normal;
                break;
        }
    }

    // ======================================================================================
    public void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Exit : " + collision.collider.gameObject.tag);
        switch (collision.collider.gameObject.tag)
        {
            case "Floor":
                IsGrounded = false;
                break;
            case "Wall":
                IsWallSnapped = false;
                break;
        }
    }

    // ======================================================================================
    public void FixedUpdate()
    {
        // Dash Subsystem : triggers Dash and runs it until the end
        UpdateDashSubsystem();
        // Jump Subsystem : triggers Jump and WallEjection and runs it until the end
        UpdateJumpSubsystem();
        // Walk Subsystem : horizontal locomotion in the ground and in the air
        UpdateWalkSubsystem();
        // Gravity Subystem : applies gravity in normal conditions
        UpdateGravitySubsystem();

        // OBS: 2 Special unstopabble events are handled by the system:
        // Dash
        // Ejection
        if (Velocity.x > 0)
            ForwardDir = eDirection.Right;
        else if (Velocity.x < 0)
            ForwardDir = eDirection.Left;
    }

    // ======================================================================================
    // PRIVATE MEMBERS - SUBSYSTEM HANDLERS
    // ======================================================================================
    private void UpdateDashSubsystem()
    {
        if (IsDashing)
        {
            UpdateDash();
            return;
        }

        // GET INPUT
        bool doDash = m_input.GetDash();

        // Try to Trigger Event, if possible
        if (doDash && !IsEjecting && !IsWallSliding)
            StartDash();
    }

    // ======================================================================================
    private void UpdateJumpSubsystem()
    {
        // UNSTOPABBLE EVENTS
        if (IsEjecting)
        {
            UpdateEjection();
            UpdateGravity();
            return;
        }
        else if (IsDashing)
            return;

        // GET INPUT
        bool doJump = m_input.GetJump();

        // Try to Trigger Event, if possible
        if (doJump && IsGrounded)
            StartJump();
        else if (doJump && IsWallSliding)
            StartEjection();
    }

    // ======================================================================================
    private void UpdateWalkSubsystem()
    {
        // UNSTOPABBLE EVENTS
        if (IsEjecting || IsDashing)
            return;


        // HORIZONTAL
        if (IsGrounded)
            UpdateWalk();
        else
            UpdateFalling();
    }

    // ======================================================================================
    private void UpdateGravitySubsystem()
    {
        if (IsEjecting || IsDashing)
            return;

        // VERTICAL
        UpdateGravity();
    }

    // ======================================================================================
    // PRIVATE MEMBERS - SUBSYSTEM EVENT STARTERS
    // ======================================================================================
    private void StartDash()
    {
        IsDashing = true;

        Vector2 velocity = m_rb.velocity;

        m_dashDirection = new Vector2(m_input.GetHorizontal(), m_input.GetVertical());
        if (m_dashDirection.sqrMagnitude == 0)
            m_dashDirection = new Vector2(ForwardDir == eDirection.Right ? 1 : -1, 0);
        else
            m_dashDirection.Normalize();

        m_dashTargetPos = m_rb.position + m_dashDirection * m_dashDist;
        
        velocity = m_dashMaxSpeed * m_dashDirection;
        m_rb.velocity = velocity;

    }

    // ======================================================================================
    private void StartJump()
    {
        IsJumping           = true;
        Vector2 velocity    = m_rb.velocity;
        velocity.y          = m_jumpMaxSpeed;
        m_rb.velocity       = velocity;
    }

    // ======================================================================================
    private void StartEjection()
    {
        IsGrounded          = false;
        IsEjecting          = true;
        IsWallSnapped       = false;

        m_ejectTargetPosX   = m_rb.position.x + m_snappedWallNormal.x * m_ejectDist;
        m_ejectDirectionX   = m_snappedWallNormal.x;

        Vector2 velocity    = m_rb.velocity;
        velocity.y          = m_jumpMaxSpeed;
        velocity.x          = m_ejectMaxSpeed * m_ejectDirectionX;
        m_rb.velocity       = velocity;
    }

    // ======================================================================================
    // PRIVATE MEMBERS - SUBSYSTEM UPDATERS
    // ======================================================================================
    private void UpdateWalk()
    {
        float   speedInput  = m_input.GetHorizontal();
        if (IsWallSnapped && speedInput * m_snappedWallNormal.x < 0)
            return;


        Vector2 velocity = m_rb.velocity;

        velocity.x          = Mathf.Lerp(velocity.x, m_walkMaxSpeed * speedInput, m_walkAcc * GameMgr.DeltaTime);
        if (speedInput == 0)
            velocity.x      = Mathf.Abs(velocity.x) < m_walkMinSpeedRatio * m_walkMaxSpeed ? 0 : velocity.x;
        else
            velocity.x      = Mathf.Abs(velocity.x) < m_walkMinSpeedRatio * m_walkMaxSpeed ? m_walkMinSpeedRatio * m_walkMaxSpeed * (speedInput > 0 ? 1 : -1) : velocity.x;

        m_rb.velocity       = velocity;
    }

    // ======================================================================================
    private void UpdateEjection()
    {
        Vector2 velocity    = m_rb.velocity;
        float ratio         = Mathf.Clamp((m_ejectTargetPosX - m_rb.position.x) * m_ejectDirectionX, 0, m_ejectDist);

        velocity.x          = Mathf.Lerp(m_ejectMinSpeedRatio * m_ejectMaxSpeed * m_ejectDirectionX , velocity.x, ratio);
        m_rb.velocity       = velocity;

        if (ratio < m_minEjectEventRatio)
            IsEjecting      = false;
    }

    // ======================================================================================
    private void UpdateFalling()
    {
        float speedInput = m_input.GetHorizontal();
        if (IsWallSnapped && speedInput * m_snappedWallNormal.x < 0)
            return;

        Vector2 velocity    = m_rb.velocity;
        velocity.x          = Mathf.Lerp(velocity.x, m_ratioToWalk * m_walkMaxSpeed * speedInput, m_ratioToWalk * m_walkAcc * GameMgr.DeltaTime);
        m_rb.velocity       = velocity;
    }

    // ======================================================================================
    private void UpdateDash()
    {
        Vector2 velocity    = m_rb.velocity;
        float ratio         = Mathf.Clamp(Vector2.Dot((m_dashTargetPos - m_rb.position), m_dashDirection), 0, m_dashDist);

        velocity            = m_dashDirection * Mathf.Lerp(m_dashMinSpeedRatio * m_dashMaxSpeed, velocity.magnitude, ratio);
        m_rb.velocity       = velocity;

        if (ratio < m_minDashEventRatio)
            IsDashing       = false;
    }

    // ======================================================================================
    private void UpdateGravity()
    {
        if (IsGrounded)
        {
            if (!IsJumping)
                m_rb.velocity       = new Vector2(m_rb.velocity.x, 0);
        }
        else
        {
            Vector3 accGravity  = Physics.gravity * m_gravityRatio * GameMgr.DeltaTime;
            Vector2 velocity    = m_rb.velocity;

            velocity.y += accGravity.y;

            if (IsWallSliding)
                velocity.y      = velocity.y < -m_slideMaxSpeed ? -m_slideMaxSpeed : velocity.y;
            else
                velocity.y      = velocity.y < -m_gravityMaxSpeed ? -m_gravityMaxSpeed : velocity.y;

            m_rb.velocity       = velocity;
        }
    }

    // ======================================================================================
    private void InitializeValues()
    {
        Debug.Assert(m_configData != null, this.gameObject.name + " - PlayerController : Missing PlayerConfig for parameters init");

        m_gravityRatio          = m_configData.m_gravityRatio;
        m_gravityMaxSpeed       = m_configData.m_gravityMaxSpeed;
        
        m_jumpMaxSpeed          = m_configData.m_jumpMaxSpeed;
        
        m_ratioToWalk           = m_configData.m_ratioToWalk;
        
        m_walkAcc               = m_configData.m_walkAcc;
        m_walkMaxSpeed          = m_configData.m_walkMaxSpeed;

        m_slideMaxSpeed         = m_configData.m_slideMaxSpeed;
        m_ejectDist             = m_configData.m_ejectDist;
        m_ejectMaxSpeed         = m_configData.m_ejectMaxSpeed;

        m_dashDist              = m_configData.m_dashDist;
        m_dashMaxSpeed          = m_configData.m_dashMaxSpeed;

        m_walkMinSpeedRatio     = m_configData.m_walkMinSpeedRatio;
        m_ejectMinSpeedRatio    = m_configData.m_ejectMinSpeedRatio;
        m_dashMinSpeedRatio     = m_configData.m_dashMinSpeedRatio;
    }
}   