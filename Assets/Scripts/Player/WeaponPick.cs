using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputCtlr), typeof(PlayerAttack))]
public class WeaponPick : MonoBehaviour {

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    // player trying to grab
    private PlayerInputCtlr m_input;
    private PlayerAttack    m_attack;

    // Weapons list
    public List<GameObject> WeaponList = new List<GameObject>();

    private float m_grabCooldown = 0.5f;
    private bool IsGrabing = false;

    // drop parameters
    private Vector2 throwOffset = new Vector2(0, 0.7f);

    // ------------------------------------- PREFABS ------------------------------------ //
    public GameObject Pistol;
    public GameObject Saber;

    // Use this for initialization
    void Start () {
        m_input = this.GetComponent<PlayerInputCtlr>();
        m_attack = this.GetComponent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        // get pick up item input
        if (InputMgr.GetButton((int)m_input.m_nbPlayer, InputMgr.eButton.GRAB) && !IsGrabing)
        {
            if (WeaponList.Count > 0)
            {
                PickupWeapon();
            }
        }
        IsGrabing = InputMgr.GetButton((int)m_input.m_nbPlayer, InputMgr.eButton.GRAB);
    }

    private void PickupWeapon()
    {
        if(WeaponList[0] == null)
        {
            WeaponList.RemoveAt(0);
            return;
        }
        int currWeapon = 0;

        float closestDist = Vector2.Distance(this.transform.position, WeaponList[0].transform.position);
        float currentDist;

        GameObject WeaponObject;

        for (int i = 1; i < WeaponList.Count; i++)
        {
            currentDist = Vector2.Distance(this.transform.position, WeaponList[i].transform.position);
            if (currentDist < closestDist)
            {
                closestDist = currentDist;
                currWeapon = i;
            }
        }

        if (m_attack.EquipWeap != PlayerAttack.eWeapon.Fists)
        {
            GameObject obj;
            switch (m_attack.EquipWeap)
            {
                case PlayerAttack.eWeapon.Pistol:
                    obj = Instantiate(Pistol, transform.position + (Vector3)throwOffset, Quaternion.identity);
                    break;
                case PlayerAttack.eWeapon.Saber:
                    obj = Instantiate(Saber, transform.position + (Vector3)throwOffset, Quaternion.identity);
                    break;
                default:
                    obj = Instantiate(Saber, transform.position + (Vector3)throwOffset, Quaternion.identity);
                    break;
            }

            obj.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, 5f, 0);
            obj.GetComponent<Rigidbody2D>().gravityScale = 1;
            Destroy(obj, 5f);

            m_attack.ReloadShots();
            m_attack.EquipWeap = (PlayerAttack.eWeapon)WeaponList[currWeapon].GetComponent<Pickable>().m_weapnType;
            WeaponObject = WeaponList[currWeapon];
            WeaponList.Remove(WeaponObject);
            Destroy(WeaponObject);
        }
        else
        {
            m_attack.EquipWeap = (PlayerAttack.eWeapon)WeaponList[currWeapon].GetComponent<Pickable>().m_weapnType;
            WeaponObject = WeaponList[currWeapon];
            WeaponList.Remove(WeaponObject);
            Destroy(WeaponObject);
        }

        StartCoroutine(GrabDelay());
    }

    private IEnumerator GrabDelay()
    {
        yield return new WaitForSeconds(m_grabCooldown);
        IsGrabing = false;
    }
}
