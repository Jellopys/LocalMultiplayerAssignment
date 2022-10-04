using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<IWeapon> _availableWeapons = new List<IWeapon>();
    private int _activeWeaponSlot = 0;
    private Bazooka _bazooka;
    private Gun _gun;
    private RayGun _rayGun;
    [SerializeField] GameObject _bazookaModel;
    [SerializeField] GameObject _pistolModel;
    [SerializeField] GameObject _rayGunModel;


    void Awake()
    {
        _bazooka = GetComponent<Bazooka>();
        _gun = GetComponent<Gun>();
        _rayGun = GetComponent<RayGun>();
        AddWeapon(EnumWeapon.Bazooka);
        AddWeapon(EnumWeapon.Gun);
        AddWeapon(EnumWeapon.RayGun);
    }

    public void AddWeapon(EnumWeapon weapon)
    {
        if (weapon == EnumWeapon.Bazooka)
        {
            if (_availableWeapons.Contains(_bazooka)) { return; } // ADD AMMO IN BRACKETS

            _availableWeapons.Add(_bazooka); // ADD WEAPON
        }
        else if (weapon == EnumWeapon.Gun)
        {
            if (_availableWeapons.Contains(_gun)) { return; }

            _availableWeapons.Add(_gun); // ADD WEAPON
        }
        else // RAYGUN
        {
            if (_availableWeapons.Contains(_rayGun)) { return; }

            _availableWeapons.Add(_rayGun); // ADD WEAPON
        }
    }

    public void SetWeaponsUI(Transform playerProfileGUI, GameObject weaponUIPrefab)
    {
        foreach (IWeapon weapon in _availableWeapons)
        {
            weapon.SetupWeaponUI(playerProfileGUI, weaponUIPrefab);
        }
    }

    public void Reload()
    {
        foreach (IWeapon weapon in _availableWeapons)
        {
            weapon.Reload();
        }
    }

    public void Shoot(bool isHolding)
    {
        _availableWeapons[_activeWeaponSlot].Shoot(isHolding);
    }

    public void SwitchWeapon(int weaponType)
    {
        _activeWeaponSlot = weaponType;

        if (weaponType == 0) // BAZOOKA
        {
            _bazookaModel.SetActive(true);
            _pistolModel.SetActive(false);
            _rayGunModel.SetActive(false);
        }
        else if (weaponType == 1) // PISTOL
        {
            _bazookaModel.SetActive(false);
            _pistolModel.SetActive(true);
            _rayGunModel.SetActive(false);
        }
        else // RAYGUN
        {
            _bazookaModel.SetActive(false);
            _pistolModel.SetActive(false);
            _rayGunModel.SetActive(true);
        }

    }
}
