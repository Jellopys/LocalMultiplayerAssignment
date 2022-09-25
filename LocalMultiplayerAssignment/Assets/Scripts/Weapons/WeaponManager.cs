using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<IWeapon> _availableWeapons = new List<IWeapon>();
    private int _activeWeaponSlot = 0;
    private Bazooka _bazooka;
    private Gun _gun;

    void Awake()
    {
        _bazooka = GetComponent<Bazooka>();
        _gun = GetComponent<Gun>();
        AddWeapon(EnumWeapon.Bazooka);
        AddWeapon(EnumWeapon.Gun);
    }

    public void AddWeapon(EnumWeapon weapon)
    {
        if (weapon == EnumWeapon.Bazooka)
        {
            if (_availableWeapons.Contains(_bazooka)) { return; } // ADD AMMO HERE

            _availableWeapons.Add(_bazooka); // ADD WEAPON
        }
        else
        {
            if (_availableWeapons.Contains(_gun)) { return; }

            _availableWeapons.Add(_gun); // ADD WEAPON
        }
    }

    public void Shoot(Transform transform, bool isHolding)
    {
        _availableWeapons[_activeWeaponSlot].Shoot(transform, isHolding);
    }

    public void SwitchWeapon(int weaponType)
    {
        _activeWeaponSlot = weaponType;
    }
}
