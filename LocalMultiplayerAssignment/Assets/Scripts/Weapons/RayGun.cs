using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LineRenderer _rayLine;
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

    public void Shoot(bool isHolding)
    {

        UIManager.GetInstance().SetIsHolding(isHolding);

        _isCharging = isHolding;

        if (isHolding)
        {
            _rayPower = 0;
            _damage = 0f;
        }
        else if (!isHolding)
        {
            RaycastHit hit;
            Vector3 rayOrigin = _spawnPoint.transform.position;
            _rayLine.SetPosition(0, rayOrigin);
            if (Physics.Raycast(rayOrigin, _spawnPoint.transform.forward, out hit, _rayPower))
            {
                _rayLine.SetPosition(1, hit.point);
                if (!hit.rigidbody.gameObject.GetComponentInParent<PlayerHealth>()) { return; }
                hit.rigidbody.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(_damage);
            }
            else if (!Physics.Raycast(rayOrigin, _spawnPoint.transform.forward, out hit, _rayPower))
            {
                _rayLine.SetPosition(1, rayOrigin + (_spawnPoint.transform.forward * _rayPower));
            }
            StartCoroutine(ShootRay());
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

        if (_reverse == false && _rayPower <= _rayMaxRange)
        {
            _rayPower = _rayPower + _rayMaxRange * Time.deltaTime;
            _damage = _damage + _maxDamage * Time.deltaTime;

            if (_rayPower >= _rayMaxRange)
                _reverse = true;

        }
        else if (_reverse == true && _rayPower >= 0)
        {
            _rayPower = _rayPower - _rayMaxRange * Time.deltaTime;
            _damage = _damage - _maxDamage * Time.deltaTime;

            if (_rayPower <= 0)
                _reverse = false;
        }
    }
}
