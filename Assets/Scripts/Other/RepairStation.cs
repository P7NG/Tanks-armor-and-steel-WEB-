using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairStation : MonoBehaviour
{
    [SerializeField] private int _healHP;
    [SerializeField] private GameObject _light;
    
    private Health _playerHealth;
    [SerializeField] private GameObject player;

    private void Start()
    {
        _playerHealth = player.GetComponent<Health>();
        _light.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _light.activeInHierarchy)
        {
            _playerHealth.ChangeHealth(-_healHP);
            _light.SetActive(false);
        }
    }
}
