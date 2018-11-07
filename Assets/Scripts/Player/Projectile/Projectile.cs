using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    // --------------------------- PROTECTED CONFIG ATTRIBUTES --------------------------- //
    protected PlayerInputCtlr.ePlayer origin;


    [Header("Dimensions")]
    protected float m_width = 0.2f;
    protected float m_height = 0.2f;

    public GameObject PistolPref;
    public GameObject SaberPref;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponentInChildren<DamageBehaviour>()!=null)
        {
            if (tag == "Lethal")
            {
                other.GetComponentInChildren<DamageBehaviour>().TakeDamage(origin);
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(other.GetComponentInChildren<DamageBehaviour>().GetStunned());
                GameObject obj;
                switch (name)
                {
                    case "ThrowablePistol(Clone)":
                        obj = Instantiate(PistolPref, transform.position, Quaternion.identity);
                        break;
                    case "ThrowableSaber(Clone)":
                        obj = Instantiate(SaberPref, transform.position, Quaternion.identity);
                        break;
                    default:
                        obj = Instantiate(SaberPref, transform.position, Quaternion.identity);
                        break;
                }

                obj.GetComponent<Rigidbody2D>().velocity = new Vector3((-0.2f)*this.GetComponent<Rigidbody2D>().velocity.x, 5f, 0);
                obj.GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(obj, 5f);
                Destroy(gameObject);
            }
        }
        else
        {
            if (tag == "Lethal")
                Destroy(gameObject);
            else
            {
                GameObject obj;
                switch (name)
                {
                    case "ThrowablePistol(Clone)":
                        obj = Instantiate(PistolPref, transform.position, Quaternion.identity);
                        break;
                    case "ThrowableSaber(Clone)":
                        obj = Instantiate(SaberPref, transform.position, Quaternion.identity);
                        break;
                    default:
                        obj = Instantiate(SaberPref, transform.position, Quaternion.identity);
                        break;
                }

                obj.GetComponent<Rigidbody2D>().velocity = new Vector3((-0.2f) * this.GetComponent<Rigidbody2D>().velocity.x, 5f, 0);
                obj.GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(obj, 5f);
                Destroy(gameObject);
            }
        }
    }

    public void SetOrigin(PlayerInputCtlr.ePlayer player)
    {
        origin = player;
    }
}    