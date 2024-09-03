using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Falling : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private UnityEvent _event;

    private Rigidbody _rb;

    private void Update()
    {
        if(_rb.velocity.magnitude > _force)
        {
            _event?.Invoke();
        }
    }
}
