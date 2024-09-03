using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private string[] _tags;


    private void OnTriggerEnter(Collider other)
    {
        if (_tags.Length == 0)
        {
            _event?.Invoke();
        }
        else
        {
            foreach(string tag in _tags)
            {
                if (other.gameObject.tag == tag)
                {
                    _event?.Invoke();
                }
            }
        }
    }
}
