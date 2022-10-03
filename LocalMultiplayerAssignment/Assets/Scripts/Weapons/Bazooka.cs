using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;
    private GameObject _instigator;
    private bool currentlyHolding = false;
    private Vector3 _trajectory;
    private float bazookaPower;
    private float bazookaMaxPower = 800f;
    private bool isChargingBazooka;
    private bool reverse;
    private int damage = 20;

    public void Shoot(bool isHolding)
    {

        UIManager.GetInstance().SetIsHolding(isHolding);
        _trajectory = _spawnPoint.transform.forward * bazookaPower + _spawnPoint.transform.up * 300;
        currentlyHolding = isHolding;

        
        isChargingBazooka = isHolding;
        if (isHolding)
        {
            bazookaPower = 0;
        }
        else if (!isHolding)
        {
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
