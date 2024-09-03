using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    public LevelControl levelControl;
    public UnityEvent Event;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Ram" || other.gameObject.tag == "Player") && levelControl.MissionComplete >= levelControl.CountTarget-1)
        {
            Event?.Invoke();
        }
    }
}
