using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Bullet" && other.GetComponent<BulletBehaviour>().owner == Owner.Player)
        {
            other.gameObject.transform.rotation = this.transform.rotation;
        }
    }
}
