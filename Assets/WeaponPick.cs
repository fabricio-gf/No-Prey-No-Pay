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
            print(WeaponList.Count);
            if (WeaponList.Count > 0)
            {
                PickupWeapon();
            }
        }
        IsGrabing = InputMgr.GetButton((int)m_input.m_nbPlayer, InputMgr.eButton.GRAB);
    }

    private void PickupWeapon()
    {
        print("watch start");
        var watch = System.Diagnostics.Stopwatch.StartNew();

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
            m_attack.EquipWeap = (PlayerAttack.eWeapon)WeaponList[currWeapon].GetComponent<Pickable>().m_weapnType;
            WeaponObject = WeaponList[currWeapon];
            WeaponList.Remove(WeaponObject);
            Destroy(WeaponObject);

            //WeaponObject.transform.position = this.transform.position + new Vector3(0, 0.25f, 0);
            //ToggleWeaponActive(WeaponObject, true);
            //WeaponObject.GetComponent<Projectile>().MoveProjectileAtAngle();
        }
        else
        {
            m_attack.EquipWeap = (PlayerAttack.eWeapon)WeaponList[currWeapon].GetComponent<Pickable>().m_weapnType;
            WeaponObject = WeaponList[currWeapon];
            WeaponList.Remove(WeaponObject);
            Destroy(WeaponObject);
        }

        StartCoroutine(GrabDelay());
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        print("time elapsed " + elapsedMs);
    }

    private IEnumerator GrabDelay()
    {
        yield return new WaitForSeconds(m_grabCooldown);
        IsGrabing = false;
    }
}
