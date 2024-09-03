using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTowerRotate : MonoBehaviour
{
    [SerializeField] private float _towerRotateSpeed;
    [SerializeField] private FixedJoystick _stick;

    private Vector3 direction;
    private int _clickCount = 0;
    public bool _returnPhase = false;

    public void Rotate()
    {
        transform.Rotate(direction * _towerRotateSpeed);
        direction = Vector3.zero;
    }

    private void FixedUpdate()
    {
        ChangeDirectionTowerX();
        ChangeDirectionTowerY();

        RotateZ();

        if (_returnPhase)
        {
            ReturnTower();
        }
    }

    private void RotateZ()
    {
        float setX = transform.localEulerAngles.x;
        if(transform.localEulerAngles.x > 4 && transform.localEulerAngles.x < 300)
        {
            setX = 3.95f;
        }
        if(transform.localEulerAngles.x < 348 && transform.localEulerAngles.x > 50)
        {
            setX = -11.95f;
        }
        
        transform.localEulerAngles = new Vector3(setX, transform.localEulerAngles.y, 0);

        
    }

    public void ChangeDirectionTowerX()
    {

        if (_stick.Direction.x == 0 && !_returnPhase)
        {
            direction = Vector3.zero;
        }
        else if (_stick.Direction.x > 0.2f)
        {
            direction = transform.up;
            _returnPhase = false;

        }
        else if (_stick.Direction.x < -0.2f)
        {
            direction = -transform.up;
            _returnPhase = false;

        }

        Rotate();
    }

    public void ChangeDirectionTowerY()
    {
       
        //Debug.Log(transform.localRotation.y);
        if (_stick.Direction.y == 0)
        {
            direction = Vector3.zero;
        }
        else if (_stick.Direction.y > 0.2f && (transform.localEulerAngles.x > 330 || transform.localEulerAngles.x <= 20.5))
        {
            direction = Vector3.left * 0.3f;
           
        }
        else if (_stick.Direction.y < -0.2f && (transform.localEulerAngles.x > 340 || transform.localEulerAngles.x <= 20))
        {

            direction = Vector3.right * 0.3f;            
        }

        Rotate();
    }

    private IEnumerator ReturnTowerTimer()
    {
        _clickCount++;
        if(_clickCount >= 2)
        {
            //Return tower;
            _returnPhase = true;

            _clickCount = 0;
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            _clickCount = 0;
        }
    }

    public void ReturnTowerCall()
    {
        StartCoroutine(ReturnTowerTimer());
    }

    private void ReturnTower()
    {
        Debug.Log(gameObject.transform.localEulerAngles.y);
        
        if(gameObject.transform.localEulerAngles.y > 0)
        {
            direction = -transform.up;
        }
        if (gameObject.transform.localEulerAngles.y > 180)
        {
            direction = transform.up;
        }
        if (gameObject.transform.localEulerAngles.y > -1 && gameObject.transform.localEulerAngles.y < 1)
        {
            direction = Vector3.zero;
            _returnPhase = false;
        }

        direction *= 3;
    }
}
