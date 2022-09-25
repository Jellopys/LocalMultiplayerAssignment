using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _projectile;
    private GameObject _instigator;
    private bool currentlyHolding = false;
    private Transform spawnPoint;
    private Vector3 _trajectory;
    private float bazookaPower;
    private bool isChargingBazooka;
    private bool reverse;

    public void Shoot(Transform projectileSpawnPoint, bool isHolding)
    {
        // TODO 
        // Refactor how I get projectileSpawnPoint

        spawnPoint = projectileSpawnPoint;
        UIManager.GetInstance().SetIsHolding(isHolding);
        _trajectory = projectileSpawnPoint.transform.forward * bazookaPower + projectileSpawnPoint.transform.up * 300;
        currentlyHolding = isHolding;

        
        isChargingBazooka = isHolding;
        if (isHolding)
        {
            bazookaPower = 0;
        }
        else if (!isHolding)
        {
            GameObject projectile = Instantiate(_projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            projectile.GetComponent<Bullet>().SetProjectilePower(_trajectory);
        }
    }

    void Update()
    {
        if (isChargingBazooka)
        {
            
            if (reverse == false && bazookaPower <= 700)
            {
                bazookaPower = bazookaPower + 700f * Time.deltaTime;

                if (bazookaPower >= 700)
                    reverse = true;

            }
            else if (reverse == true && bazookaPower >= 0)
            {
                bazookaPower = bazookaPower - 700f * Time.deltaTime;

                if (bazookaPower <= 0)
                    reverse = false;
            }
        }
    }
}
