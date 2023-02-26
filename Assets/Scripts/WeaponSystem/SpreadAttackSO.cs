using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.WeaponSystem
{
    [CreateAssetMenu(menuName ="Attacks/SpreadAttack")]
    public class SpreadAttackSO : AttackPatternSO
    {
        [SerializeField] GameObject _projectile;

        [SerializeField] float _angleDegrees = 5f;
        public override void PerformAttack(Transform shootingStartPoint)
        {
            Instantiate(_projectile, shootingStartPoint.position, shootingStartPoint.rotation);

            Instantiate(_projectile, shootingStartPoint.position, shootingStartPoint.rotation
               * Quaternion.Euler(Vector3.forward * _angleDegrees));

            Instantiate(_projectile, shootingStartPoint.position, shootingStartPoint.rotation
             * Quaternion.Euler(Vector3.forward * -_angleDegrees));
        }
    }
}