using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroy : MonoBehaviour
{
    [SerializeField] private bool _destroyFromStart = false;
    [SerializeField] private GameObject _destroyObject;
    [SerializeField] private UnityEvent _preAction;
    [SerializeField] private float _timeToDestroy = 0;
    [SerializeField] private UnityEvent _postAction;
    [SerializeField] private bool _needDestroy = true;

    private float _currentTime;

    private void Start()
    {
        if (_destroyFromStart)
        {
            DestroyVoid();
        }
    }

    public void DestroyVoid()
    {
        _preAction?.Invoke();
        StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        _postAction?.Invoke();
        if (_needDestroy)
        {
            Destroy(_destroyObject);
        }
    }
}
