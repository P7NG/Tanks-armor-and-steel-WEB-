using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private TankType type = TankType.Enemy;
    
    [Header("Gun")]
    [SerializeField] private GameObject _shell;
    [SerializeField] private Transform _gun;
    [SerializeField] private float _gunReloadTime;
    [SerializeField] private float _startGunReloadTime;


    [Header("MachineGun")]
    [SerializeField] private GameObject _bullet;   
    [SerializeField] private Transform _machineGun;
    [SerializeField] private float _machineGunReloadTime;
    [SerializeField] private float _spreadMachineGun;
    [SerializeField] private CanvasController _canvas;

    [SerializeField] private GameObject _laser;
    [SerializeField] private float _sizeCoef = 1;
    [SerializeField] private GameObject _camera;
    private float _currentMachineGunReloadTime;
    private float _currentGunReloadTime;
    private bool _shooting = false;
    private GameObject point;
    private SoundController _soundController;

    private void Start()
    {
        _currentGunReloadTime = _startGunReloadTime;
        _soundController = GetComponent<SoundController>();
    }

    private void Update()
    {
        _currentMachineGunReloadTime -= Time.deltaTime;
        _currentGunReloadTime -= Time.deltaTime;
        if (_shooting)
        {
            MashineGunShoot();
        }

        if (_canvas != null)
        {
            _canvas.ChangeFillingButton((_gunReloadTime - _currentGunReloadTime)/_gunReloadTime);
        }

        LaserSpawn();
    }

    private void LaserSpawn()
    {
        if (type == TankType.Enemy) return;
        
        if (point != null)
        {
            Destroy(point);
        }

        RaycastHit hit;
        Ray ray = new Ray(_gun.position, _gun.transform.forward);
        Physics.Raycast(ray, out hit, Mathf.Infinity, ~10-20, QueryTriggerInteraction.Ignore);

        
        point = Instantiate(_laser, _gun.transform.position + _gun.transform.forward * 25 , _camera.transform.rotation);
        point.transform.localScale = new Vector3(point.transform.localScale.x * _sizeCoef * Vector3.Distance(transform.position, point.transform.position), point.transform.localScale.x * _sizeCoef * Vector3.Distance(transform.position, point.transform.position), point.transform.localScale.x * _sizeCoef * Vector3.Distance(transform.position, point.transform.position));

        _camera.transform.LookAt(point.transform);
    }

    public void MachineShootOn()
    {
        _shooting = true;
    }

    public void MachineShootOff()
    {
        _shooting = false;
    }

    public void MashineGunShoot()
    {
        if(_currentMachineGunReloadTime <= 0)
        {
            float delta = Random.RandomRange(-_spreadMachineGun, _spreadMachineGun);

            GameObject shell = Instantiate(_bullet, _machineGun.position, new Quaternion(_machineGun.rotation.x, _machineGun.rotation.y + delta, _machineGun.rotation.z, _machineGun.rotation.w));
            _soundController.PlaySoundOneShot("Bullet");
            if (type == TankType.Player) 
            {
                shell.transform.LookAt(point.transform);
                shell.GetComponent<BulletBehaviour>().owner = Owner.Player; 
            }
            _currentMachineGunReloadTime = _machineGunReloadTime;
            
        }
    }

    public void GunShooting()
    {
        if (_currentGunReloadTime <= 0)
        {
            GameObject shell = Instantiate(_shell, _gun.position, _gun.rotation);
            _soundController.PlaySoundOneShot("Shell");

            if (type == TankType.Player)
            {
                shell.transform.LookAt(point.transform);
                shell.GetComponent<BulletBehaviour>().owner = Owner.Player;
            }
            _currentGunReloadTime = _gunReloadTime;
            
        }
    }

}

public enum TankType
{
    Enemy,
    Player
}
