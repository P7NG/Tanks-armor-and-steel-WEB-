using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag != "Player"  && other.gameObject.tag != "Enemy") || other.isTrigger) return;
        _event?.Invoke();
        other.GetComponent<Armor>().BrakeTrack();
    }
}
