using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCtlr : MonoBehaviour
{
    private ICollidable[]   m_collidables;

    private GameObject      m_touchingWall;
    private GameObject      m_touchingGround;
    private bool            m_isTouchingGround;
    private bool            m_isTouchingWall;

    // ======================================================================================
    public virtual void Awake()
    {
        m_collidables = this.gameObject.GetComponents<ICollidable>();
    }

    // ======================================================================================
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            if (contact.normal == Vector2.up)
                OnTouchingGround(collision.gameObject, contact.normal);
            else if (Vector2.Dot(contact.normal, Vector2.up) == 0)
                OnTouchingWall(collision.gameObject, contact.normal);
        }
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
    private void OnTouchingGround(GameObject _ground, Vector2 _normal)
    {
        m_isTouchingGround  = true;
        m_touchingGround    = _ground;

        foreach (ICollidable collidable in m_collidables)
            collidable.OnTouchingGround(_normal);
    }

    // ======================================================================================
    private void OnTouchingWall(GameObject _wall, Vector2 _normal)
    {
        m_isTouchingWall    = true;
        m_touchingWall      = _wall;

        foreach (ICollidable collidable in m_collidables)
            collidable.OnTouchingWall(_normal);
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
