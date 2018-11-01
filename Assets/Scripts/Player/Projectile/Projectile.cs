using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : RuntimeMonoBehaviour {


    // --------------------------- PROTECTED CONFIG ATTRIBUTES --------------------------- //
     protected Vector3                 DirectionVector     = Vector3.zero;
	[SerializeField]
    protected bool                    isFalling           = true;
	[SerializeField]
    protected float                   m_gravSpeed         = 0;
	protected float                   m_collisionEpsilon  = 0.001f;
    protected PlayerInputCtlr.ePlayer origin;

	[Range(0, 1)]
    protected float                    m_gravityRatio      = .4f;

	[Header("Dimensions")]
    protected float                    m_width             = .1f;
    protected float                    m_height            = .1f;

    // ------------------------------------- ACCESSORS ----------------------------------- //
    public bool IsGrounded { get; protected set; }
    public bool IsWallSnapped { get; protected set; }

    protected override void StartPhase(){
	}

	// Update is called once per frame
	protected override void UpdatePhase () {
		if(!isFalling){
			transform.Translate(DirectionVector * Time.deltaTime);
			return;
		}
		//UpdateTransform();
	}

	public void SetDirection(Vector3 direction){
		DirectionVector = direction;
	}

    public void SetOrigin(PlayerInputCtlr.ePlayer player)
    {
        origin = player;
    }

	// ideally direction == 1 is right, and direction == -1 is left
	// it can also be changed to a bool with 0 and 1
	public void MoveProjectile(Vector3 direction){
		SetDirection(direction);
		isFalling = false;
	}

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
		if(!isFalling && other.tag == "Player"){
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

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // OBS : JOAO CODE FOR TEST ONLY!!!!!! --> BUT IS WORKING PERFECTLY FOR PLAYER DETECTION!
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (!isFalling && other.gameObject.layer == LayerMask.NameToLayer("Players"))
        //{
        //    if (tag == "Lethal")
        //    {
        //        other.GetComponent<DamageBehaviour>().TakeDamage(origin);
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        other.GetComponent<DamageBehaviour>().GetStunned();
        //        SetDirection(Vector3.zero);
        //        isFalling = true;
        //    }
        //}

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

    // ======================================================================================
    /*private bool UpdateCollisions(Vector2 _startPos, Vector2 _endPos)
    {
        Vector3 finalPos = CheckCollision(_startPos, _endPos);

        if (IsWallSnapped)
        {
             = 0;
        }

        this.transform.position = finalPos;

        return true;
    }*/

    // ======================================================================================
    /*private Vector3 CheckCollision(Vector3 _startPos, Vector3 _endPos)
    {
        RaycastHit hitInfo;
        Vector3 direction   = _endPos - _startPos;
        Vector3 finalEndPos = _endPos;

        if (direction.y < 0)
        {
            if (Physics.Raycast(_startPos, direction + m_collisionEpsilon * direction.normalized, out hitInfo, direction.magnitude + m_collisionEpsilon, ~(1 << this.gameObject.layer)))
            {
                GameObject gnd = hitInfo.collider.gameObject;

                if (gnd != null)
                {
                    finalEndPos.y = gnd.transform.up() + m_collisionEpsilon;
                }
            }
        }

        finalEndPos.x = Mathf.Clamp(finalEndPos.x, SceneMgr.MinX + m_width / 2, SceneMgr.MaxX - m_width / 2);
        finalEndPos.y = Mathf.Clamp(finalEndPos.y, SceneMgr.MinY, SceneMgr.MaxY - m_height);
        return finalEndPos;
    }

    private bool CheckWalls ()
    {
        Vector3 lWall = this.transform.position - ( m_collisionEpsilon + m_width / 2 ) * Vector3.right;
        Vector3 rWall = this.transform.position + ( m_collisionEpsilon + m_width / 2 ) * Vector3.right;

        if (lWall.x <= SceneMgr.MinX || rWall.x >= SceneMgr.MaxX)
            return true;

        return Physics.Raycast(this.transform.position, -(m_collisionEpsilon - m_width / 2) * Vector3.right) || Physics.Raycast(this.transform.position, -(m_collisionEpsilon - m_width / 2) * Vector3.right + (m_collisionEpsilon + m_width / 2) * Vector3.right);
    }*/

    // ======================================================================================
    // PUBLIC MEMBERS - ICollidable INTERFACE FOR COLLISION DETECTION
    // ======================================================================================
    public void OnTouchingWall(Vector2 _normal)
    {
        IsWallSnapped = true;
        IsGrounded = IsGrounded;
    }

    // ======================================================================================
    public void OnTouchingGround(Vector2 _normal)
    {
        IsWallSnapped = IsWallSnapped;
        IsGrounded = true;
    }
}
