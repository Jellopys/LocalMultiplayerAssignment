using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Sprite _weaponIcon;
    private Vector3 _GUILocation = new Vector3 (-40, 300, 0);
    private Vector3 _power;
    private float _damage = 2f;
    private bool _currentlyHolding = false;

    //AMMO
    private WeaponGUIInfo _weaponGUI;
    private int _maxAmmunition = 10;
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

        _currentlyHolding = isHolding;

        if (isHolding)
        {
            StartCoroutine(ShootBullets());
        }
    }

    IEnumerator ShootBullets()
    {
        while (_currentlyHolding)
        {
            if (_currentAmmunition <= 0) 
            {
                yield break;
            }

            _currentAmmunition -= 1;
            UpdateAmmoText();

            _power = _spawnPoint.transform.forward * 700f;
            GameObject projectile = Instantiate(_bullet, _spawnPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Bullet>().SetProjectilePower(_power, _damage);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
