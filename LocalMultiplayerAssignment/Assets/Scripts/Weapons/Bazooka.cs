using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bazooka : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxAmmunition = 1;
    [SerializeField] private Sprite _weaponIcon;
    private Vector3 _GUILocation = new Vector3 (-130, 300, 0);
    private GameObject _instigator;
    private bool currentlyHolding = false;
    private Vector3 _trajectory;
    private float bazookaPower;
    private float bazookaMaxPower = 800f;
    private bool isChargingBazooka;
    private bool reverse;
    private int damage = 20;
    private int _currentAmmunition;
    private WeaponGUIInfo _weaponGUI;

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
        weaponGUI.GetComponent<WeaponGUIInfo>().InitWeaponUI(_currentAmmunition, _weaponIcon);
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
        
        currentlyHolding = isHolding;
        UIManager.GetInstance().SetIsHolding(isHolding);
        _trajectory = _spawnPoint.transform.forward * bazookaPower + _spawnPoint.transform.up * 300;
        
        isChargingBazooka = isHolding;
        if (isHolding)
        {
            bazookaPower = 0;
        }
        else if (!isHolding)
        {
            _currentAmmunition -= 1;
            UpdateAmmoText();

            GameObject projectile = Instantiate(_projectile, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            projectile.GetComponent<Bullet>().SetProjectilePower(_trajectory, damage);
        }
    }

    void Update()
    {
        if (isChargingBazooka)
        {
            
            if (reverse == false && bazookaPower <= bazookaMaxPower)
            {
                bazookaPower = bazookaPower + bazookaMaxPower * Time.deltaTime;

                if (bazookaPower >= bazookaMaxPower)
                    reverse = true;

            }
            else if (reverse == true && bazookaPower >= 0)
            {
                bazookaPower = bazookaPower - bazookaMaxPower * Time.deltaTime;

                if (bazookaPower <= 0)
                    reverse = false;
            }
        }
    }
}
