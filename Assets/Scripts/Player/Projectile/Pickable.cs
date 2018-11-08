using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private float m_rotationSpeed;

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
        m_rotationSpeed = 0;
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

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, m_rotationSpeed));
    }

    //void OnTriggerEnter2D(Collider2D other){
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponPick>().WeaponList.Add(this.gameObject);
        }
        else
            m_rotationSpeed = 0;
    }

    //	void OnTriggerExit2D(Collider2D other){
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<WeaponPick>().WeaponList.Remove(this.gameObject);
        }
    }

    public void SetRotationSpeed (float rotationSpeed)
    {
        m_rotationSpeed = rotationSpeed;
    }

}
