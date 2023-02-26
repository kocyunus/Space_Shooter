using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.WeaponSystem
{
    [CreateAssetMenu(menuName ="Attacks/DubleBarrelAttack")]
    public class DubleBarrelAttackSO : AttackPatternSO
    {
        [SerializeField] float _offsetFromShootingPoint =0.3f;
        [SerializeField] GameObject _projectile;
        public override void PerformAttack(Transform shootingStartPoint)
        {
            Vector3 offset = shootingStartPoint.rotation * new Vector3(_offsetFromShootingPoint, 0, 0);

            Vector3 point1 = shootingStartPoint.position + offset;
            Vector3 point2 = shootingStartPoint.position - offset;

            Instantiate(_projectile, point1, shootingStartPoint.rotation);
            Instantiate(_projectile, point2, shootingStartPoint.rotation);
        }
    }
}