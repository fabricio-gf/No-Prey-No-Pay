using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputCtlr), typeof(CollisionCtlr))]
public class PlayerAttack : PlayerRuntimeMonoBehaviour
{
    // -------------------------------------- ENUMS -------------------------------------- //
    public enum eDirection
    {
        Left,
        Right
    };

    public enum eWeapon
    {
        Fists,
        Pistol,
        Saber
    };

    // --------------------------- PROTECTED CONFIG ATTRIBUTES --------------------------- //
    // attack params
    protected float   m_attackCooldown  = 0.4f;
    protected Vector2 m_throwOffset;

    // attack: Fists
    protected Vector2 PunchOffset;
    protected Vector2 PunchHitboxSize;

    // attack: Saber
    protected Vector2 SaberOffset;
    protected Vector2 SaberHitboxSize;

    // attack: Pistol
    public GameObject ProjectilePrefab;
    protected Vector2 PistolOffset;

    // attack: Saber
    protected Vector2 StompOffset;
    protected Vector2 StompHitboxSize;
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    // attack origin
    private PlayerInputCtlr     m_input;
    private PlayerController    m_control;
    private LayerMask           playerLayer;

    // attack subsystem
    private Vector2             m_attackDirection = Vector2.zero;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public bool IsAttacking { get; protected set; }
    public eWeapon EquipWeap { get; set; }


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    override protected void StartPhase()
    {
        m_control = this.GetComponent<PlayerController>();
        m_input = this.GetComponent<PlayerInputCtlr>();
        IsAttacking = false;

        playerLayer = LayerMask.GetMask("Players");

        EquipWeap = eWeapon.Pistol;

        PunchOffset.x = 1.5f;
        PunchOffset.y = 0.75f;
        PunchHitboxSize.x = 0.3f;
        PunchHitboxSize.y = 0.5f;

        SaberOffset.x = 0.7f;
        SaberOffset.y = 0.75f;
        SaberHitboxSize.x = 0.75f;
        SaberHitboxSize.y = 0.3f;

        PistolOffset.x = 0.7f;
        PistolOffset.y = 0.75f;

        StompOffset.x = 0;
        StompOffset.y = -0.2f;
        StompHitboxSize.x = 0.35f;
        StompHitboxSize.y = 0.3f;
    }

    // ======================================================================================
    override protected void FixedUpdatePhase()
    {
        // Attack Subsystem : triggers Attack and generates hitboxes
        UpdateAttackSubsystem();
        //Stomp Subsystem : creates the stomp hitbox if necessary
        UpdateStompSubsystem();
        
    }

    // ======================================================================================
    // PRIVATE MEMBERS - SUBSYSTEM HANDLERS
    // ======================================================================================
    private void UpdateAttackSubsystem()
    {
        if (IsAttacking)
        {
            return;
        }

        // GET INPUT
        bool doAttack = m_input.GetAttack();

        // Try to Trigger Event, if possible
        if (doAttack && !IsAttacking)
            StartAttack();
    }

    // ======================================================================================
    private void UpdateStompSubsystem()
    {
        if (this.GetComponent<PlayerController>().IsGrounded || this.GetComponent<PlayerController>().Velocity.y > 0)
        {
            return;
        }

        // Try to Trigger Event, if possible
        if (!this.GetComponent<PlayerController>().IsGrounded && this.GetComponent<PlayerController>().Velocity.y < 0)
            Stomp();
    }
    // ======================================================================================
    // PRIVATE MEMBERS - SUBSYSTEM EVENT STARTERS
    // ======================================================================================
    private void StartAttack()
    {
        m_attackDirection = new Vector2(m_input.GetHorizontal(), 0);

        if (m_attackDirection.sqrMagnitude == 0)
        {
            m_attackDirection = new Vector2( 0 , m_input.GetVertical());
            if (m_attackDirection.sqrMagnitude == 0)
                m_attackDirection = new Vector2(m_control.ForwardDir == PlayerController.eDirection.Right ? 1 : -1, 0);
            else
                m_attackDirection.Normalize();
        }          
        else
            m_attackDirection.Normalize();

        if (!m_control.IsWallSnapped && !m_control.IsWallSliding)
        {
            IsAttacking = true;

            switch (EquipWeap)
            {
                case eWeapon.Fists:
                    {
                        PunchAttack();
                        break;
                    }
                case eWeapon.Saber:
                    {
                        SaberAttack();
                        break;
                    }
                case eWeapon.Pistol:
                    {
                        PistolAttack();
                        break;
                    }
            }

        }
    }

    // ======================================================================================
    private void Stomp()
    {
        Collider[] hitTargets = Physics.OverlapBox(transform.position + new Vector3(transform.localScale.x * StompOffset.x, transform.localScale.y * StompOffset.y,0), new Vector3(StompHitboxSize.x, StompHitboxSize.y, 0.4f));
        for (int i = 0; i < hitTargets.Length; i++)
        {
            if(hitTargets[i].GetComponent<DamageBehaviour>() != null)
            hitTargets[i].GetComponent<DamageBehaviour>().TakeDamage(this.m_input.m_nbPlayer);
        }
    }
    // ======================================================================================
    // PRIVATE MEMBERS - COOLDOWN HANDLERS
    // ======================================================================================
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(m_attackCooldown);
        IsAttacking = false;
    }

    // ======================================================================================
    // PRIVATE MEMBERS - WEAPON ROUTINES
    // ======================================================================================
    private void PunchAttack()
    {
        Collider[] hitTargets = Physics.OverlapBox(transform.position + new Vector3(transform.localScale.x * m_attackDirection.x * PunchOffset.x, m_attackDirection.y * 1 + transform.localScale.y * PunchOffset.y, 0), new Vector3(PunchHitboxSize.x, PunchHitboxSize.y, 0.4f));
        for (int i = 0; i < hitTargets.Length; i++)
        {
            if (hitTargets[i].GetComponent<DamageBehaviour>() != null)
                StartCoroutine(hitTargets[i].GetComponent<DamageBehaviour>().GetStunned());
        }
        
        StartCoroutine(AttackDelay());
    }

    private void SaberAttack()
    {
        this.gameObject.SendMessage("MSG_OnExclusiveEventStart", this);

        Collider[] hitTargets = Physics.OverlapBox(transform.position + new Vector3(transform.localScale.x * m_attackDirection.x * SaberOffset.x, m_attackDirection.y * 1 + transform.localScale.y * SaberOffset.y, 0), new Vector3(SaberHitboxSize.x, SaberHitboxSize.y, 0.4f));
        
        for (int i = 0; i < hitTargets.Length; i++)
        {
            if (hitTargets[i].GetComponent<DamageBehaviour>() != null)
                hitTargets[i].GetComponent<DamageBehaviour>().TakeDamage(this.m_input.m_nbPlayer);
        }

        this.gameObject.SendMessage("MSG_OnExclusiveEventEnd", this);
        StartCoroutine(AttackDelay());
    }

    private void PistolAttack()
    {
        this.gameObject.SendMessage("MSG_OnExclusiveEventStart", this);

        GameObject obj = Instantiate(ProjectilePrefab, transform.position + new Vector3(transform.localScale.x * PistolOffset.x, PistolOffset.y, 0), Quaternion.identity);
        //obj.GetComponent<Projectile>().MoveProjectile(new Vector3(30, 0, 0));
        obj.GetComponent<Projectile>().SetOrigin(this.m_input.m_nbPlayer);
        //obj.GetComponent<Rigidbody>().gravityScale = 0;
        obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 30;

        Destroy(obj, 2.0f);
        this.gameObject.SendMessage("MSG_OnExclusiveEventEnd", this);

        StartCoroutine(AttackDelay());
    }

    void OnDrawGizmosSelected()
    {
        // draws gizmos for punch and saber hitboxes
        if (EquipWeap == eWeapon.Fists)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + (Vector3)PunchOffset, (Vector3)PunchHitboxSize);
        }
        if (EquipWeap == eWeapon.Saber)
        { 
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + new Vector3(m_attackDirection.x*SaberOffset.x, m_attackDirection.y*1 + SaberOffset.y,0), (Vector3)SaberHitboxSize);
        }
        if (EquipWeap == eWeapon.Pistol)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + (Vector3)PistolOffset, new Vector3(0.25f, 0.25f, 0));
        }
        if (!this.GetComponent<PlayerController>().IsGrounded && this.GetComponent<PlayerController>().Velocity.y < 0)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position + (Vector3)StompOffset, (Vector3)StompHitboxSize);
        }
    }
}
