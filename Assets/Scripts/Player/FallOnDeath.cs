using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionCtlr), typeof(PlayerController), typeof(PlayerAnimatorController))]
public class FallOnDeath : RuntimeMonoBehaviour
{
    // ======================================================================================
    public void MSG_Death()
    {
        StartCoroutine(Fall());
    }

    // ======================================================================================
    private IEnumerator Fall()
    {
        CollisionCtlr collision = this.gameObject.GetComponent<CollisionCtlr>();
        PlayerController player = this.gameObject.GetComponent<PlayerController>();
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        PlayerAnimatorController animator = this.gameObject.GetComponent<PlayerAnimatorController>();

        while (collision.Ground == null)
        {
            if (!IsPaused())
            {
                rb.simulated = true;
                UpdateGravity(player.m_configData.m_gravityRatio, player.m_configData.m_gravityMaxSpeed, rb);
            }
            else
                rb.simulated = false;

            yield return new WaitForFixedUpdate();
        }

        rb.velocity = Vector2.zero;
        rb.simulated = false;
        animator.m_animator.SetBool("IsFalling", false);
    }

    // ======================================================================================
    private void UpdateGravity(float _gravityRatio, float _gravityMaxSpeed, Rigidbody2D _rb)
    {
        Vector3 accGravity = Physics.gravity * _gravityRatio * GameMgr.DeltaTime;
        Vector2 velocity = _rb.velocity;

        velocity.y += accGravity.y;
        velocity.y = velocity.y < -_gravityMaxSpeed ? -_gravityMaxSpeed : velocity.y;

        _rb.velocity = velocity;
    }
}
