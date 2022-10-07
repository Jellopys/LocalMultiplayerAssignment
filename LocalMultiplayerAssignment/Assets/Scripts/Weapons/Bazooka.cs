using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bazooka : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;
    private Vector3 _trajectory;
    private float _bazookaPower;
    private float _bazookaMaxPower = 800f;
    private bool _isChargingBazooka;
    private bool _reverse;
    private int _damage = 20;

    // AMMO + UI
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private Sprite _keybindIcon;
    private WeaponGUIInfo _weaponGUI;
    private Vector3 _GUILocation = new Vector3 (-130, 300, 0);
    private int _maxAmmunition = 2;
    private int _currentAmmunition;

    void Awake()
    {
        _currentAmmunition = _maxAmmunition;
    }

    public void SetupWeaponUI(Transform playerProfileGUI, GameObject weaponUIPrefab)
    {
        Vector3 spawnLocation = new Vector3(50, 0, 0);
        GameObject weaponGUI = Instantiate(weaponUIPrefab, spawnLocation, Quaternion.identity);
        _weaponGUI = weaponGUI.GetComponent<WeaponGUIInfo>();
        weaponGUI.GetComponent<RectTransform>().SetParent(playerProfileGUI);
        weaponGUI.GetComponent<RectTransform>().localPosition = _GUILocation;
        weaponGUI.GetComponent<WeaponGUIInfo>().InitWeaponUI(_currentAmmunition, _weaponIcon, _keybindIcon);
    }

    public void Reload()
    {
        _currentAmmunition = _maxAmmunition;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        _weaponGUI.UpdateAmmo(_currentAmmunition);
    }

    public void Shoot(bool isHolding)
    {
        if (_currentAmmunition <= 0) { return; } // NO AMMO
        
        UIManager.GetInstance().SetIsHolding(isHolding);
        _trajectory = _spawnPoint.transform.forward * _bazookaPower + _spawnPoint.transform.up * 300;
        _isChargingBazooka = isHolding;

        if (isHolding)
        {
            _bazookaPower = 0;
        }
        else if (!isHolding)
        {
            _currentAmmunition -= 1;
            UpdateAmmoText();
            GameObject projectile = Instantiate(_projectile, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            projectile.GetComponent<Bullet>().SetProjectilePower(_trajectory, _damage);
        }
    }

    void Update()
    {
        if (_isChargingBazooka)
        {
            if (_reverse == false && _bazookaPower <= _bazookaMaxPower)
            {
                _bazookaPower = _bazookaPower + _bazookaMaxPower * Time.deltaTime;

                if (_bazookaPower >= _bazookaMaxPower)
                    _reverse = true;

            }
            else if (_reverse == true && _bazookaPower >= 0)
            {
                _bazookaPower = _bazookaPower - _bazookaMaxPower * Time.deltaTime;

                if (_bazookaPower <= 0)
                    _reverse = false;
            }
        }
    }
}
