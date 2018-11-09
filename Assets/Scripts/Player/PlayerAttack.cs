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

    // ------------------------------------ PUBLIC --------------------------------------- //
    public bool stompEnable = true;
    public bool canShoot { get { return m_bulletsShot < m_numberOfBullets; } }

    // --------------------------- PROTECTED CONFIG ATTRIBUTES --------------------------- //
    // attack params
    protected float   m_attackCooldown  = 0.4f;
    protected Vector2 m_throwOffset;

    // attack: Fists
    protected float   m_fistStartDelay;
    protected Vector2 PunchOffset;
    protected Vector2 PunchHitboxSize;

    // attack: Saber
    public GameObject ThrowSaberPrefab;
    protected float   m_saberStartDelay;
    protected Vector2 SaberOffset;
    protected Vector2 ThrowSaberOffset;
    protected Vector2 SaberHitboxSize;

    // attack: Pistol
    public GameObject ThrowPistolPrefab;
    public GameObject ProjectilePrefab;
    protected int     m_numberOfBullets;
    protected int     m_bulletsShot;
    protected float   m_pistolStartDelay;
    protected Vector2 ThrowPistolOffset;
    protected Vector2 PistolOffset;

    // attack: Saber
    protected Vector2 StompOffset;
    protected Vector2 StompHitboxSize;

    // stomp
    protected float StompBounciness = 10f;
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    // attack origin
    private PlayerInputCtlr     m_input;
    private PlayerController    m_control;

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

        EquipWeap = eWeapon.Fists;

        PunchOffset.x = 0.75f;
        PunchOffset.y = 0.75f;
        PunchHitboxSize.x = 0.3f;
        PunchHitboxSize.y = 0.5f;
        m_fistStartDelay = 0.3f;

        SaberOffset.x = 0.85f;
        SaberOffset.y = 0.75f;
        ThrowSaberOffset.x = 1.1f;
        ThrowSaberOffset.y = 0.75f;
        SaberHitboxSize.x = 0.75f;
        SaberHitboxSize.y = 0.3f;
        m_saberStartDelay = 0.3f;

        m_numberOfBullets = 1;
        m_bulletsShot = 0;
        PistolOffset.x = 0.9f;
        PistolOffset.y = 0.95f;
        ThrowPistolOffset.x = 1f;
        ThrowPistolOffset.y = 1.35f;
        m_pistolStartDelay = 0.3f;

        StompOffset.x = 0;
        StompOffset.y = 0.1f;
        StompHitboxSize.x = 0.35f;
        StompHitboxSize.y = 0.01f;
    }

    // ======================================================================================
    override protected void FixedUpdatePhase()
    {
        // Attack Subsystem : triggers Attack and generates hitboxes
        UpdateAttackSubsystem();
        if(stompEnable)
            //Stomp Subsystem : creates the stomp hitbox if necessary
            UpdateStompSubsystem();
        //Throw Subsystem : Instantiates a throwable projectile and changes weapons
        UpdateThrowSubsystem();
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
    private void UpdateThrowSubsystem()
    {
        if (IsAttacking)
        {
            return;
        }

        // GET INPUT
        bool doThrow = m_input.GetToss();

        // Try to Trigger Event, if possible
        if (doThrow && !IsAttacking)
            ThrowWeapon();
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
                        StartCoroutine(PunchAttack());
                        break;
                    }
                case eWeapon.Saber:
                    {
                        StartCoroutine(SaberAttack());
                        break;
                    }
                case eWeapon.Pistol:
                    {
                        StartCoroutine(PistolAttack());
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
            GetComponent<Rigidbody2D>().velocity = new Vector3(0,StompBounciness,0);
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
    private IEnumerator PunchAttack()
    {
        yield return new WaitForSeconds(m_fistStartDelay);

        Collider[] hitTargets = Physics.OverlapBox(transform.position + new Vector3(m_attackDirection.x * PunchOffset.x, m_attackDirection.y * 1 + transform.localScale.y * PunchOffset.y, 0), new Vector3(PunchHitboxSize.x, PunchHitboxSize.y, 0.4f));
        for (int i = 0; i < hitTargets.Length; i++)
        {
            if (hitTargets[i].GetComponent<DamageBehaviour>() != null)
                StartCoroutine(hitTargets[i].GetComponent<DamageBehaviour>().GetStunned());
        }
        
        StartCoroutine(AttackDelay());
    }
    // ======================================================================================

    private IEnumerator SaberAttack()
    {
        this.gameObject.SendMessage("MSG_OnExclusiveEventStart", this);

        yield return new WaitForSeconds(m_saberStartDelay);
        Collider[] hitTargets = Physics.OverlapBox(transform.position + new Vector3(m_attackDirection.x * SaberOffset.x, m_attackDirection.y * 1 + transform.localScale.y * SaberOffset.y, 0), new Vector3(SaberHitboxSize.x, SaberHitboxSize.y, 0.4f));
        
        for (int i = 0; i < hitTargets.Length; i++)
        {
            if (hitTargets[i].GetComponent<DamageBehaviour>() != null)
                hitTargets[i].GetComponent<DamageBehaviour>().TakeDamage(this.m_input.m_nbPlayer);
        }

        this.gameObject.SendMessage("MSG_OnExclusiveEventEnd", this);
        StartCoroutine(AttackDelay());
    }
    // ======================================================================================

    private IEnumerator PistolAttack()
    {
        this.gameObject.SendMessage("MSG_OnExclusiveEventStart", this);
        yield return new WaitForSeconds(m_fistStartDelay);
        if (m_bulletsShot < m_numberOfBullets)
        {
            m_bulletsShot++;
            GameObject obj = Instantiate(ProjectilePrefab, transform.position + new Vector3(m_attackDirection.x * PistolOffset.x,m_attackDirection.y * 0.8f + PistolOffset.y, 0), Quaternion.identity);
            obj.GetComponent<Projectile>().SetOrigin(this.m_input.m_nbPlayer);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(m_attackDirection.x * 30f, m_attackDirection.y * 25f, 0);

            Destroy(obj, 2.0f);
        }
        
        this.gameObject.SendMessage("MSG_OnExclusiveEventEnd", this);

        StartCoroutine(AttackDelay());
    }
    // ======================================================================================

    private void ThrowWeapon()
    {
        m_bulletsShot = 0;
        m_attackDirection = new Vector2(m_input.GetHorizontal(), 0);

        if (m_attackDirection.sqrMagnitude == 0)
        {
            m_attackDirection = new Vector2(0, m_input.GetVertical());
            if (m_attackDirection.sqrMagnitude == 0)
                m_attackDirection = new Vector2(m_control.ForwardDir == PlayerController.eDirection.Right ? 1 : -1, 0);
            else
                m_attackDirection.Normalize();
        }
        else
            m_attackDirection.Normalize();

        if (EquipWeap != eWeapon.Fists)
        {
            //this.gameObject.SendMessage("MSG_OnExclusiveEventStart", this);
            GameObject obj;
            switch (EquipWeap)
            {
                case eWeapon.Saber:
                    obj = Instantiate(ThrowSaberPrefab, transform.position + new Vector3(transform.localScale.x * ThrowSaberOffset.x, ThrowSaberOffset.y, 0), Quaternion.identity);
                    break;
                case eWeapon.Pistol:
                    obj = Instantiate(ThrowPistolPrefab, transform.position + new Vector3(transform.localScale.x * ThrowPistolOffset.x, ThrowPistolOffset.y, 0), Quaternion.identity);
                    break;
                default:
                    obj = Instantiate(ProjectilePrefab, transform.position + new Vector3(transform.localScale.x * PistolOffset.x, PistolOffset.y, 0), Quaternion.identity);
                    break;
            }

            obj.GetComponent<Projectile>().SetOrigin(this.m_input.m_nbPlayer);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(m_attackDirection.x * 10f, m_attackDirection.y * 9f + 1f, 0);
            obj.GetComponent<Projectile>().SetRotationSpeed(10f);

            EquipWeap = eWeapon.Fists;

            //this.gameObject.SendMessage("MSG_OnExclusiveEventEnd", this);

            StartCoroutine(AttackDelay());
        }
    }

    public void ReloadShots()
    {
        m_bulletsShot = 0;
    }

    // ======================================================================================
    void OnDrawGizmosSelected()
    {
        // draws gizmos for punch and saber hitboxes
        if (EquipWeap == eWeapon.Fists)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + new Vector3(m_attackDirection.x * PunchOffset.x, m_attackDirection.y * 1 + transform.localScale.y * PunchOffset.y, 0), (Vector3)PunchHitboxSize);
        }
        if (EquipWeap == eWeapon.Saber)
        { 
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + new Vector3(m_attackDirection.x * SaberOffset.x, m_attackDirection.y * 1 + transform.localScale.y * SaberOffset.y, 0), (Vector3)SaberHitboxSize);
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
