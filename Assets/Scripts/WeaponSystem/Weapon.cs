using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] AttackPatternSO _attackpattern;
        [SerializeField] Transform _shootingStartPoint;
        public bool shootingDelayed;

        public GameObject projectile;

        public AudioSource gunAudio;

        [SerializeField] List<AttackPatternSO> _weapons;
        int _weaponIndex;
        [SerializeField] AudioClip _weaponSwapSFX;
        private void Start()
        {
            if (shootingDelayed)
                StartCoroutine(DelayShooting());
        }
        public void SwapWeapon() 
        {
            _weaponIndex++;
            _weaponIndex = _weaponIndex >= _weapons.Count ? 0 : _weaponIndex;
            _attackpattern = _weapons[_weaponIndex];
            gunAudio.PlayOneShot(_weaponSwapSFX);
        }
        public void PerformAttack() 
        {
            if (shootingDelayed == false)
            {
                shootingDelayed = true;
                gunAudio.PlayOneShot(_attackpattern.attackSFX);
                _attackpattern.PerformAttack(_shootingStartPoint);
                StartCoroutine(DelayShooting());
            }
        }
        private IEnumerator DelayShooting()
        {
            yield return new WaitForSeconds(_attackpattern.attackDelay);
            shootingDelayed = false;
        }
    }
}