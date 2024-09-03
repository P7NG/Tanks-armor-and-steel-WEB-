using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private float _armorCoeff;
    [SerializeField] private ArmorType _armorType;
    [SerializeField] private TeamType _teamType;
    [Range(1, 10)]
    [SerializeField] private float _explosionChance;
    [Range(1, 10)]
    [SerializeField] private float _loseTrackChance;

    [SerializeField] private Health _healthScript;
    [SerializeField] private Destroy _destroyScript;

    [Header("Enemy")]
    [SerializeField] private EnemyAI _enemy;

    [Header("Player")]
    [SerializeField] private TankMovement _player;

    public void TakeHit(BulletBehaviour bullet)
    {
        float randInt = UnityEngine.Random.Range(5, 20) / 10f;
        bool destroyed = false;

        if (_armorType == ArmorType.Front)
        {
            _healthScript.ChangeHealth(bullet.BulletDamage / _armorCoeff * randInt);
        }
        if (_armorType == ArmorType.Back)
        {
            if (bullet.Type == type.shell)
            {
                float damage = bullet.BulletDamage / _armorCoeff;
                Debug.Log(damage * randInt);

                if (damage * randInt / 100f > _explosionChance)
                {
                    _destroyScript.DestroyVoid();
                    destroyed = true;
                }
            }

            if (destroyed) return;
            _healthScript.ChangeHealth(bullet.BulletDamage / _armorCoeff * randInt);

        }
        if (_armorType == ArmorType.Track)
        {
            if (bullet.Type == type.shell)
            {
                float damage = bullet.BulletDamage / _armorCoeff;

                if (damage / 100f * randInt > _loseTrackChance)
                {
                    //LOSE TRACK
                    if(_teamType == TeamType.Enemy)
                    {
                        StartCoroutine(_enemy.RepairTrack());
                    }
                    else
                    {
                        StartCoroutine(_player.RepairTrack());
                    }
                }
            }
            _healthScript.ChangeHealth(bullet.BulletDamage / _armorCoeff * randInt);
        }
    }

    public void BrakeTrack()
    {
        if (_teamType == TeamType.Enemy)
        {
            StartCoroutine(_enemy.RepairTrack());
        }
        else
        {
            StartCoroutine(_player.RepairTrack());
        }
    }
}



public enum ArmorType
{
    Track,
    Back,
    Front
}

public enum TeamType
{
    Enemy,
    Player
}
