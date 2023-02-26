using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YK.WeaponSystem;

namespace YK.EnemyAI
{
    public class ShootActionAI : MonoBehaviour
    {
        Player _player;
        [SerializeField] Weapon _weapon;
        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _weapon = GetComponentInChildren<Weapon>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_player.isAlive)
            {
                if (_weapon != null)
                    _weapon.PerformAttack();
            }
        }
    }
}