using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius = 20f;
    [SerializeField] private float _force = 500f;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private string[] _tags = { "Environment" };


    public void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        for (int j = 0; j < overlappedColliders.Length; j++)
        {
            Rigidbody rigidbody = overlappedColliders[j].attachedRigidbody;
            if (rigidbody)
            {
                foreach (string i in _tags)
                {

                    if (overlappedColliders[j].gameObject.tag == i)
                    {
                        rigidbody.AddExplosionForce(_force, transform.position, _radius);

                        Health health = overlappedColliders[j].GetComponent<Health>();
                        Armor armor = overlappedColliders[j].GetComponent<Armor>();
                        if (health != null && !overlappedColliders[j].isTrigger)
                        {
                            health.ChangeHealth(_damage);
                        }
                    }
                }
            }
        }
    }
}
