using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputCtlr), typeof(PlayerStateMachine))]
public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public float m_jumpMaxSpeed     = 10;

    [Header("Jump")]
    public float m_gravityRatio     = 1.0f;

    [Header("Wall")]
    public float m_maxSlideSpeed    = 5;
    public float m_ejectDist        = 1;
    public float m_ejectMaxSpeed    = 10;
    
    [Header("Walk")]
    public float m_walkAcc         = 10;
    public float m_walkMaxSpeed    = 20;

    [Header("Fall")]
    [Range(0,1)]
    public float m_ratioToWalk      = 1;


    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private Rigidbody2D         m_rb;
    private PlayerInputCtlr     m_input;
    private Vector2             m_snappedWallNormal;

    // ejection
    private float m_ejectTargetPosX   = 0;
    private float m_ejectDirectionX  = 0;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public bool IsGrounded      { get; protected set; }
    public bool IsWallSnapped   { get; protected set; }
    public bool IsEjecting      { get; protected set; }
    public bool IsDashing       { get; protected set; }

    
    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start ()
    {
        m_rb    = this.GetComponent<Rigidbody2D>();
        m_input = this.GetComponent<PlayerInputCtlr>();

        IsGrounded      = false;
        IsWallSnapped   = false;
        IsEjecting      = false;
        IsDashing       = false;
	}

    // ======================================================================================
    public void FixedUpdate()
    {
        // UNSTOPABBLE EVENTS
        if (IsEjecting)
        {
            UpdateEjection();
            UpdateGravity();
            return;
        }
        else if (IsDashing)
        {
            UpdateDash();
            return;
        }

        // GET INPUT
        bool doJump = m_input.GetJump();
        bool doDash = m_input.GetDash();

        // Try to Trigger Event, if possible
        if (doDash && !IsEjecting)
            StartDash();
        else if (doJump && IsGrounded)
            StartJump();
        else if (doJump && IsWallSnapped)
            StartEjection();


        // HORIZONTAL
        if (IsGrounded)
            UpdateWalk();
        else
            UpdateFalling();

        // VERTICAL
        UpdateGravity();
    }

    // ======================================================================================
    private void StartDash()
    {

    }

    // ======================================================================================
    private void UpdateWalk()
    {
        Vector2 velocity    = m_rb.velocity;
        velocity.x          = Mathf.Lerp(velocity.x, m_walkMaxSpeed * m_input.GetHorizontal(), m_walkAcc * GameMgr.DeltaTime);
        m_rb.velocity       = velocity;
    }
    
    // ======================================================================================
    private void UpdateEjection()
    {
        Vector2 velocity    = m_rb.velocity;
        float ratio         = Mathf.Clamp((m_ejectTargetPosX - m_rb.position.x) * m_ejectDirectionX, 0, m_ejectDist);

        velocity.x          = Mathf.Lerp(velocity.x, 0, ratio * GameMgr.DeltaTime);
        m_rb.velocity       = velocity;

        if (ratio < 0.2f)
            IsEjecting = false;
    }

    // ======================================================================================
    private void UpdateFalling()
    {
        Vector2 velocity    = m_rb.velocity;
        velocity.x          = Mathf.Lerp(velocity.x, m_ratioToWalk * m_walkMaxSpeed * m_input.GetHorizontal(), m_ratioToWalk * m_walkAcc * GameMgr.DeltaTime);
        m_rb.velocity       = velocity;
    }

    // ======================================================================================
    private void UpdateDash()
    {
    }

    // ======================================================================================
    private void StartJump()
    {
        IsGrounded = false;
        Vector2 velocity = m_rb.velocity;
        velocity.y = m_jumpMaxSpeed;
        m_rb.velocity = velocity;
    }

    // ======================================================================================
    private void StartEjection()
    {
        IsGrounded = false;
        IsEjecting = true;
        IsWallSnapped = false;

        m_ejectTargetPosX = m_rb.position.x + m_snappedWallNormal.x * m_ejectDist;
        m_ejectDirectionX = m_snappedWallNormal.x;

        Vector2 velocity = m_rb.velocity;
        velocity.y = m_jumpMaxSpeed;
        velocity.x = m_ejectMaxSpeed * m_ejectDirectionX;
        m_rb.velocity = velocity;
    }

    // ======================================================================================
    public void UpdateGravity()
    {
        if (IsGrounded)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
            return;
        }

        Vector3 accGravity = Physics.gravity * m_gravityRatio * GameMgr.DeltaTime;
        m_rb.velocity += new Vector2(accGravity.x, accGravity.y);
    }

    // ======================================================================================
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Enter : " + collision.collider.gameObject.tag);
        switch (collision.collider.gameObject.tag)
        {
            case "Floor":
                IsGrounded      = true;
                IsEjecting      = false;
                break;
            case "Wall":
                IsWallSnapped   = true;
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
}
