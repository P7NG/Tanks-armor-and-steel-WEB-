using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCamera : MonoBehaviour
{
    
    void Start()
    {
        FinalLevelScript final = GameObject.FindGameObjectWithTag("LevelController").GetComponent<FinalLevelScript>();
        StartCoroutine(Wait(final));
    }

    IEnumerator Wait(FinalLevelScript final)
    {
        yield return new WaitForSeconds(4);
        final.Lose();
    }

}
