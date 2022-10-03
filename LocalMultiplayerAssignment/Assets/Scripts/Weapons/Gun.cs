using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _spawnPoint;
    private bool currentlyHolding = false;
    private Vector3 power;
    private int damage = 2;

    public void Shoot(bool isHolding)
    {
        // power = _spawnPoint.transform.forward * 700f;

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
            power = _spawnPoint.transform.forward * 700f;
            GameObject projectile = Instantiate(_bullet, _spawnPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Bullet>().SetProjectilePower(power, damage);

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
