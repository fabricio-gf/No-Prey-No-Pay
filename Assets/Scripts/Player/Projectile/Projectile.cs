using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : RuntimeMonoBehaviour
{


    // --------------------------- PROTECTED CONFIG ATTRIBUTES --------------------------- //
    protected Vector3 DirectionVector = Vector3.zero;
    [SerializeField]
    protected bool isFalling = true;
    [SerializeField]
    protected float m_gravSpeed = 0;
    protected float m_collisionEpsilon = 0.01f;
    private float timeToLive = 2;
    protected PlayerInputCtlr.ePlayer origin;

    [Range(0, 1)]
    protected float m_gravityRatio = .4f;

    [Header("Dimensions")]
    protected float m_width = 0.2f;
    protected float m_height = 0.2f;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public bool IsGrounded { get; protected set; }
    public bool IsWallSnapped { get; protected set; }

    void OnCollisionEnter(Collision collision)
    {
        print("hit");
        var hit = collision.gameObject;
        var health = hit.GetComponent<DamageBehaviour>();
        if (health != null)
        {
            health.TakeDamage(origin);
        }

        Destroy(gameObject);
    }


    /*protected override void StartPhase()
    {
	}

	// Update is called once per frame
	override protected void FixedUpdatePhase()
    {
		if(!isFalling){
			transform.Translate(DirectionVector * GameMgr.DeltaTime);
			return;
		}
    }

    /*private void UpdateCollision()
    {
        Collider[] hitTargets = Physics.OverlapBox(transform.position, new Vector3(m_width, m_height, 0.4f));
        if (hitTargets.Length != 0)
        {
            print("hit");
            for (int i = 0; i < hitTargets.Length; i++)
            {
                hitTargets[i].GetComponent<DamageBehaviour>().TakeDamage(origin);
            }
            Destroy(gameObject);
        }
    }*/

	/*public void SetDirection(Vector3 direction){
		DirectionVector = direction;
	}*/

    public void SetOrigin(PlayerInputCtlr.ePlayer player)
    {
        origin = player;
    }

    // ideally direction == 1 is right, and direction == -1 is left
    // it can also be changed to a bool with 0 and 1
    /*public void MoveProjectile(Vector3 direction)
    {
        SetDirection(direction);
        isFalling = false;
    }*/
}    /*
	public void MoveProjectileAtAngle(){
		float randomAngle = Random.Range(-30f,30f);
		// throw
	}

	// overload for hitting a wall
	// choses random angle based on which side the wall was
	// if direction == 1, chooses angle between 0 and 30 degrees
	// if direction == -1, chooses angle between -30 and 0
	public void MoveProjectileAtAngle(int direction){
		float randomAngle = Random.Range((direction*15 +15), (direction*15 + 15));
		//throw
	}

	void OnTriggerEnter(Collider other){
		if(!isFalling && other.tag == "Hurtbox"){
			if(tag == "Lethal"){
				other.GetComponent<DamageBehaviour>().TakeDamage(origin);
				Destroy(gameObject);
			}
			else{
				other.GetComponent<DamageBehaviour>().GetStunned();
				SetDirection(Vector3.zero);
				isFalling = true;
			}
		}
	}

    
    void OnTriggerEnter2D(Collider2D other)
    {
        // if it has a damageBhv, it must  be a player! (or any other object that
        // can be damaged
        // so, apply damage
        DamageBehaviour damageBhv = other.GetComponent<DamageBehaviour>();
        if (!isFalling && damageBhv != null)
        {
            if (tag == "Lethal")
            {
                damageBhv.TakeDamage(origin);
                Destroy(gameObject);
            }
            else
            {
                damageBhv.GetStunned();
                SetDirection(Vector3.zero);
                isFalling = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Player" + origin)
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(),
                this.gameObject.GetComponent<Collider2D>(), true);
        }

        //Entidade "danificável"
        DamageBehaviour target = col.gameObject.GetComponent<DamageBehaviour>();
        if (target != null && target.name != "Player" + origin)
        {
            target.TakeDamage(origin);
        }
        Destroy(this.gameObject);
        Destroy(this);

        /*if (col.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			Die ();
		}*/
    /*}

    //ESSA PARTE É COPIADA DO PLAYER COM PEQUENAS ALTERAÇÕES//

    private void UpdateTransform()
    {
        // update transform
        Vector3 initialPos  = this.transform.position;

        //bool isWallSnapped  = CheckWalls();
       
        UpdateGravity();

        //UpdateCollisions(initialPos, this.transform.position);

        Vector3 finalPos    = this.transform.position;
    }

    // ======================================================================================
	private bool UpdateGravity()
    {
        if (IsGrounded)
        {
            m_gravSpeed = 0;
        }
        
m_gravSpeed += Physics.gravity.magnitude * m_gravityRatio;

        this.transform.position = this.transform.position + GameMgr.DeltaTime * m_gravSpeed * Vector3.down;
        
        return true;
    }
}*/
