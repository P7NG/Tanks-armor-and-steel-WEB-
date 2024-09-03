using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelControl : MonoBehaviour
{
    public int CountTarget = 1;
    public UnityEvent Event;
    public int MissionComplete;
    private void Update()
    {
        //_missionComplete = MissionComplete;

        if(MissionComplete == CountTarget)
        {
            Win();
        }
    }

    public void Complete()
    {
        MissionComplete++;
    }

    public void Win()
    {
        Event?.Invoke();
    }
}


