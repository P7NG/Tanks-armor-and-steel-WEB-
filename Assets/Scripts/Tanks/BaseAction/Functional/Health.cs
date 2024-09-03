using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float Hp;
    private float StartHp;
    [SerializeField] private UnityEvent _deadEvent;

    [SerializeField] private CanvasController _canvas;

    private void Start()
    {
        StartHp = Hp;
    }

    internal void ChangeHealth(float damage)
    {
        Hp -= damage;
        CheckHealth();
    }

    private void Update()
    {
        if (_canvas != null)
        {
            _canvas.ChangeHealthBar(Hp / StartHp);
        }
    }

    private void CheckHealth()
    {
        if (Hp <= 0)
        {
            _deadEvent?.Invoke();
            Hp = 10000;
        }

        if(Hp > StartHp)
        {
            Hp = StartHp;
        }
    }
}
