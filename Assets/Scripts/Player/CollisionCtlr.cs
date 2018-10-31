using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCtlr : RuntimeMonoBehaviour
{
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private ICollidable[]   m_collidables;

    private GameObject      m_touchingWall;
    private GameObject      m_touchingGround;
    private bool            m_isTouchingGround;
    private bool            m_isTouchingWall;
    
    // ------------------------------------ ACCESSORS ------------------------------------ //
    public GameObject       Wall                { get { return m_touchingWall; } }
    public GameObject       Ground              { get { return m_touchingGround; } }

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake()
    {
        m_collidables = this.gameObject.GetComponents<ICollidable>();
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer, true);
    }

    // ======================================================================================
    public void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            if (contact.normal == Vector2.up)
            {
                OnTouchingGround(collision.gameObject, contact.normal, collision.contacts);
                return;
            }
            else if (Vector2.Dot(contact.normal, Vector2.up) == 0)
            {
                OnTouchingWall(collision.gameObject, contact.normal, collision.contacts);
                return;
            }
        }

        OnTouchingAnother(collision.gameObject, contact.normal, collision.contacts);
    }
    
    // ======================================================================================
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            if (m_isTouchingGround && collision.gameObject == m_touchingGround)
                OnLeavingGround();
            if (m_isTouchingWall && collision.gameObject == m_touchingWall)
                OnLeavingWall();
        }
    }

    // ======================================================================================
    // PRIVATE MEMBERS
    // ======================================================================================
    private void OnTouchingGround(GameObject _ground, Vector2 _normal, ContactPoint2D[] _contacts)
    {
        m_isTouchingGround  = true;
        m_touchingGround    = _ground;

        foreach (ICollidable collidable in m_collidables)
            collidable.OnTouchingGround(_normal, _contacts);
    }

    // ======================================================================================
    private void OnTouchingWall(GameObject _wall, Vector2 _normal, ContactPoint2D[] _contacts)
    {
        m_isTouchingWall    = true;
        m_touchingWall      = _wall;

        foreach (ICollidable collidable in m_collidables)
            collidable.OnTouchingWall(_normal, _contacts);
    }

    // ======================================================================================
    private void OnTouchingAnother(GameObject _obj, Vector2 _normal, ContactPoint2D[] _contacts)
    {
        foreach (ICollidable collidable in m_collidables)
            collidable.OnTouchingAnother(_normal, _contacts);

#if UNITY_EDITOR
        if (_obj.layer == LayerMask.NameToLayer("Platforms"))
        {
            if (_obj.GetComponent<PlatformEffector2D>() == null)
                Debug.LogError("The GameObject " + _obj.name + " is in 'Platforms' layer and seems to be a floor... check if it has a PlatformEffector!");
            else
            {
                bool hasUsedByEffector = false;
                foreach (Collider2D col in _obj.GetComponents<Collider2D>())
                    if (col.usedByEffector)
                        hasUsedByEffector |= col.usedByEffector;

                if (!hasUsedByEffector)
                    Debug.LogError("The GameObject " + _obj.name + " is in 'Platforms' layer and seems to be a floor... check if its collider is 'UsedByEffector'!");
            }
        }
#endif
    }

    // ======================================================================================
    private void OnLeavingGround()
    {
        m_isTouchingGround = false;
        m_touchingGround = null;

        foreach (ICollidable collidable in m_collidables)
            collidable.OnLeavingGround();
    }

    // ======================================================================================
    private void OnLeavingWall()
    {
        m_isTouchingWall = false;
        m_touchingWall = null;

        foreach (ICollidable collidable in m_collidables)
            collidable.OnLeavingWall();
    }
}
