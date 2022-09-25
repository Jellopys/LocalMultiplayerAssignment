using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bullet;
    private bool currentlyHolding = false;
    private Transform spawnPoint;
    private Vector3 power;

    public void Shoot(Transform projectileSpawnPoint, bool isHolding)
    {
        // TODO 
        // Refactor how I get projectileSpawnPoint

        spawnPoint = projectileSpawnPoint;
        power = projectileSpawnPoint.transform.forward * 700f + projectileSpawnPoint.transform.up * 300f;
        currentlyHolding = isHolding;

        if (isHolding)
        {
            StartCoroutine(ShootBullets());
        }

    }

    IEnumerator ShootBullets()
    {
        while (currentlyHolding)
        {
            GameObject projectile = Instantiate(_bullet, spawnPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Bullet>().SetProjectilePower(power);

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
