using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] EnumWeapon weapon;

    void Start()
    {
        int randomWeapon = Random.Range(0, 2);
        weapon = (EnumWeapon)randomWeapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<WeaponManager>().AddWeapon(weapon);
        Destroy(gameObject);
    }
}
