using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _trackRepairTime;
    [SerializeField] private FixedJoystick _stick;
    [SerializeField] private CanvasController _canvas;
    [SerializeField] private SoundController _soundController;
    private bool _canMove = true;

    private Vector3 _moveDirection;

    private Rigidbody _rb;
    private Vector3 _rotateDirection;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if ((_rb.velocity.x > 1 || _rb.velocity.z > 1 || _rb.velocity.x < -1 || _rb.velocity.z < -1) && !TankController.IsMovement)
        {
            TankController.IsMovement = true;
            _soundController.SlowSoundStart("Track");
        }
        else if((_rb.velocity.magnitude < 2 && _rb.velocity.magnitude > -2))
        {
            TankController.IsMovement = false;
            _soundController.StopSoundSlow();
        }

        ChangeDirectionMovement();
        ChangeDirectionRotation();
    }

    public void ChangeDirectionMovement()
    {
        if(_stick.Direction.y > 0)
        {
            _moveDirection = transform.forward * _stick.Direction.y;
        }
        else if(_stick.Direction.y == 0)
        {
            _moveDirection = Vector3.zero;
        }
        else if(_stick.Direction.y < 0)
        {
            _moveDirection = transform.forward * _stick.Direction.y;
        }


        Move();
    }

    public void ChangeDirectionRotation()
    {
        //0-stop 1-forward 2-back

        if (_stick.Direction.x > 0)
        {
            _rotateDirection = transform.up * _stick.Direction.x;
        }
        else if (_stick.Direction.x == 0)
        {
            _rotateDirection = Vector3.zero;
        }
        else if (_stick.Direction.x < 0)
        {
            _rotateDirection = transform.up *_stick.Direction.x;
        }

        if(_stick.Direction.y < -0.3f)
        {
            _rotateDirection *= -1;
        }
        Rotate();
    }

    public void Move()
    {
        if (Mathf.Abs(_rb.velocity.magnitude) < _maxSpeed)
        {
            if (!_canMove) return;

            _rb.AddForce(_moveDirection * _speed);
            //transform.Translate(_moveDirection);
        }
    }

    public void Rotate()
    {

        transform.Rotate(_rotateDirection*_rotateSpeed);

    }


    public IEnumerator RepairTrack()
    {
        _canMove = false;
        _canvas.ChangeTrackImage(true);
        yield return new WaitForSeconds(_trackRepairTime);
        _canvas.ChangeTrackImage(false);
        _canMove = true;
    }
}
