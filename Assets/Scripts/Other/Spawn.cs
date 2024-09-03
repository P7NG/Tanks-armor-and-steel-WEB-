using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private bool _thisPosition;
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _spawnPlace;

    public void SpawnVoid()
    {
        if (!_thisPosition)
        {
            Instantiate(_object, _spawnPlace.position, _spawnPlace.rotation);
        }
        else
        {
            Instantiate(_object, transform.position, transform.rotation);
        }

    }
}
