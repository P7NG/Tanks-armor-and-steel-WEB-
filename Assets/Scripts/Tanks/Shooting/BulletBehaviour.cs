using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public type Type;
    public float BulletDamage;
    public Owner owner;

    [SerializeField] private float _bulletSpeed;    
    [SerializeField] private Destroy _destroyScript;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bulletSpeed /= 100;
        StartCoroutine(TimeDestroy());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _bulletSpeed);
    }

    private IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.gameObject.name == "Ram") return;

        var armor = other.GetComponent<Armor>();
        if(armor != null)
        {
            armor.TakeHit(this);
        }
        else
        {
            Health health = other.GetComponent<Health>();
            if(health != null && Type == type.bullet)
            {
                health.ChangeHealth(BulletDamage);
            }
        }

        _destroyScript.DestroyVoid();

    }
}

public enum type
{
    bullet,
    shell
}

public enum Owner
{
    Enemy,
    Player
}
