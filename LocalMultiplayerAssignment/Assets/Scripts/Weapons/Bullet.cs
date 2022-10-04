using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    private GameObject _instigator;
    private float _damage = 0f;
    private float _lifetime = 3f;

    public void SetProjectilePower(Vector3 power, float damage)
    {
        _damage = damage;
        _rigidbody.AddForce(power);
    }

    void Awake()
    {
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, _rigidbody.velocity, Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponentInParent<PlayerHealth>())
        {  
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(_damage);
            // TODO: EXPLOSION HERE
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
