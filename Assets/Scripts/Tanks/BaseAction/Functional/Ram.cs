using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ram : MonoBehaviour
{
    [SerializeField] private float _damage = 2000;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        Health health = other.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.ChangeHealth(_damage);
        }
    }
}
