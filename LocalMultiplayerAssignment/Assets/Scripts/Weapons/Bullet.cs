using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    private GameObject _instigator;
    private int _damage = 20;

    public void SetProjectilePower(Vector3 power)
    {
        _rigidbody.AddForce(power);
    }

    void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, _rigidbody.velocity, Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponentInParent<PlayerHealth>())
        {  
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(_damage);
            // TODO: EXPLOSION HERE
            Destroy(this.gameObject);
        }
    }
}
