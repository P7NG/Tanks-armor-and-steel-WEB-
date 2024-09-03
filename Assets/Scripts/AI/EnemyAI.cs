using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Transform _target;
    [SerializeField] private float _agroDistance;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _hearDistance;
    [SerializeField] private TankTowerRotate _tankTowerRotate;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _trackRepairTime;
    [SerializeField] private CanvasController _canvas;

    [SerializeField] private SoundController _soundController;
    private TankShooting _tankShooting;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Rigidbody _rb;
    private int _currentPoint;
    private bool _isMovement = false;
    private Vector3 _lastPlayerPosition;
    [SerializeField] private bool _isFinded = false;
    [SerializeField] private bool _isAttack = false;
    private bool _canMove = true;

   [SerializeField] private Phase _phase = Phase.Idle;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerCenter");
        _agent = GetComponent<NavMeshAgent>();
        _tankShooting = GetComponent<TankShooting>();
        _rb = GetComponent<Rigidbody>();
        _soundController = GetComponent<SoundController>();
        SetStartPatrolPoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerCenter")
        {
            _isFinded = true;
            RaycastHit hit;
            Vector3 startRayPoint = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            Ray ray = new Ray(startRayPoint, _player.transform.position - startRayPoint);
            Physics.Raycast(ray, out hit);
            Debug.Log(_agent.destination);
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            if (hit.collider.gameObject.tag == "Player")
            {               
                _agent.SetDestination(_player.transform.position);
                _phase = Phase.Agro;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCenter")
        {
            _isFinded = false;
        }

    }


    public IEnumerator RepairTrack()
    {
        _canMove = false;
        _canvas.ChangeTrackImage(true);
        float speed = _agent.speed;
        _agent.speed = 0;
        yield return new WaitForSeconds(_trackRepairTime);
        _canMove = true;
        _canvas.ChangeTrackImage(false);
        _agent.speed = speed;
    }

    private void Update()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.constraints = RigidbodyConstraints.None;

        if(_phase == Phase.Attack && Vector3.Distance(_target.transform.position, transform.position) < _stopDistance)
        {
            //_agent.Stop();
        }
        
        if (_phase == Phase.Idle && Vector3.Distance(transform.position, _target.transform.position) < _hearDistance && TankController.IsMovement)
        {
            StartSearching();
        }
        
        if (_phase == Phase.Search)
        {
            _agent.SetDestination(_lastPlayerPosition);
            if(Vector3.Distance(transform.position, _lastPlayerPosition) < 5)
            {
                _phase = Phase.Idle;
                SetStartPatrolPoint();
            }
        }

        if (_isFinded)
        {
            StartCoroutine(CheckTarget());
        }

        if(_phase == Phase.Agro )
        {
            _agent.stoppingDistance = _stopDistance;
            _agent.SetDestination(_player.transform.position);
            CheckTarget();
        }
        if(_phase == Phase.Agro && Vector3.Distance(transform.position, _target.transform.position) > _agroDistance)
        {
            _agent.stoppingDistance = _stopDistance;
            _phase = Phase.Idle;
            SetStartPatrolPoint();
        }

        if(_phase == Phase.Agro && Vector3.Distance(transform.position, _target.transform.position) < _attackDistance)
        {
            _agent.stoppingDistance = _stopDistance;
            Attack();
        }


        if (_phase == Phase.Agro && Vector3.Distance(transform.position, _target.transform.position) > _attackDistance * 1.5)
        {
            _agent.stoppingDistance = _stopDistance;
            _phase = Phase.Agro;
        }

        if(_phase == Phase.Idle)
        {
            Patrol();
        }

        if(Mathf.Abs(_rb.velocity.magnitude) > 2f && !_isMovement)
        {
            _soundController.SlowSoundStart("Track");
            _isMovement = true;
        }
        else if(Mathf.Abs(_rb.velocity.magnitude) < 2f && _isMovement)
        {
            _soundController.StopSoundSlow();
            _isMovement = false;
        }
    }

    private void Patrol()
    {
        if (_patrolPoints.Length == 0) return;
        _agent.stoppingDistance = 5;
        if (Vector3.Distance(_patrolPoints[_currentPoint].position, transform.position) < 10)
        {
            if (_currentPoint + 1 >= _patrolPoints.Length)
            {
                _currentPoint = 0;
            }
            else
            {
                _currentPoint += 1;
            }
            _agent.SetDestination(_patrolPoints[_currentPoint].position);
        }
    }

    private void SetStartPatrolPoint()
    {
        if (_patrolPoints.Length == 0) return;
        _agent.SetDestination(_patrolPoints[_currentPoint].position);
    }

    public void StartSearching()
    {
        _phase = Phase.Search;
        _lastPlayerPosition = _target.transform.position;
        _agent.stoppingDistance = 5;

        
    }

    private void Attack()
    {
        
        _agent.stoppingDistance = _stopDistance;
        _agent.SetDestination(_player.transform.position);

        _phase = Phase.Attack;
        _tankShooting.GunShooting();
        _tankShooting.MashineGunShoot();
        StartCoroutine(CheckTarget());

        var direction = transform.position - _player.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-direction), Time.deltaTime * _rotateSpeed);
    }

    private IEnumerator CheckTarget()
    {
        yield return new WaitForSeconds(0.02f);

        RaycastHit hit;
        Vector3 startRayPoint = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        Ray ray = new Ray(startRayPoint, _player.transform.position - startRayPoint);
        Physics.Raycast(ray, out hit);
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        _agent.stoppingDistance = _stopDistance;
        if (hit.collider != null)
        {
           
            if (hit.collider.gameObject.tag == "Player")
            {
                _agent.SetDestination(_player.transform.position);
                _isAttack = true;
                _phase = Phase.Agro;
            }
            else
            {
                _agent.SetDestination(_player.transform.position);
                _isAttack = false;
            }
        }
    }

    private IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(1);
        Vector3 target = _agent.destination;
        _rb.velocity = Vector3.zero;
        _agent.SetDestination(target);
    }
}

public enum Phase
{
    Idle,
    Search,
    Agro,
    Attack
}
