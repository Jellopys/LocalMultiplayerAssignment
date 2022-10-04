using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RayGun : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LineRenderer _rayLine;
    [SerializeField] private int _maxAmmunition = 1;
    [SerializeField] private Sprite _weaponIcon;
    private Vector3 _GUILocation = new Vector3 (50, 300, 0);
    private EnumWeapon enumWeapon = EnumWeapon.RayGun;
    private GameObject _instigator;
    private bool _currentlyHolding = false;
    private Vector3 _trajectory;
    private float _rayPower;
    private bool _isCharging;
    private bool _reverse;
    private float _damage;
    private float _maxDamage = 30f;
    private float _rayMaxRange = 20f;
    private float _rayDuration = 0.2f;
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

        _isCharging = isHolding;
        UIManager.GetInstance().SetIsHolding(isHolding);

        if (isHolding)
        {
            _rayPower = 0f;
            _damage = 0f;
        }
        else if (!isHolding)
        {
            _currentAmmunition -= 1;
            UpdateAmmoText();

            StartCoroutine(ShootRay());
            RaycastHit hit;
            Vector3 rayOrigin = _spawnPoint.transform.position;
            _rayLine.SetPosition(0, rayOrigin);
            
            if (Physics.Raycast(rayOrigin, _spawnPoint.transform.forward, out hit, _rayPower))
            {
                _rayLine.SetPosition(1, hit.point);

                if (!hit.transform.gameObject.GetComponentInParent<PlayerHealth>()) { return; }

                hit.rigidbody.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(_damage);
            }
            else if (!Physics.Raycast(rayOrigin, _spawnPoint.transform.forward, out hit, _rayPower))
            {
                _rayLine.SetPosition(1, rayOrigin + (_spawnPoint.transform.forward * _rayPower));
            }
            
        }
    }

    IEnumerator ShootRay()
    {
        _rayLine.enabled = true;
        yield return new WaitForSeconds(_rayDuration);
        _rayLine.enabled = false;
    }

    void Update()
    {
        if (!_isCharging) { return; }

        if (_reverse == false && _rayPower <= _rayMaxRange) // Increase range of ray & damage
        {
            _rayPower = _rayPower + _rayMaxRange * Time.deltaTime;
            _damage = _damage + _maxDamage * Time.deltaTime;

            if (_rayPower >= _rayMaxRange)
                _reverse = true;

        }
        else if (_reverse == true && _rayPower >= 0) // Decrease range of ray & damage
        {
            _rayPower = _rayPower - _rayMaxRange * Time.deltaTime;
            _damage = _damage - _maxDamage * Time.deltaTime;

            if (_rayPower <= 0)
                _reverse = false;
        }
    }
}
