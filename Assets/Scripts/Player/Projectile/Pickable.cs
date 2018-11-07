using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    [HideInInspector]
    public enum WeaponType
    {
        FISTS,
        PISTOL,
        SABER
    };

    public WeaponType m_weapnType { get; private set; }

    private void Awake()
    {
        switch (name)
        {
            case "Gun(Clone)":
                m_weapnType = WeaponType.PISTOL;
                break;
            case "Saber(Clone)":
                m_weapnType = WeaponType.SABER;
                break;
        }
    }
    //void OnTriggerEnter2D(Collider2D other){
    void OnTriggerEnter2D(Collider2D other)
    {
        print("Hello");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponPick>().WeaponList.Add(this.gameObject);
        }
    }

    //	void OnTriggerExit2D(Collider2D other){
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponPick>().WeaponList.Remove(this.gameObject);
        }
    }
}
