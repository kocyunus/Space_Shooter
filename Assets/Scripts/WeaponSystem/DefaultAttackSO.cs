using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.WeaponSystem
{
    [CreateAssetMenu(menuName ="Attack/DefaultAttack")]
    public class DefaultAttackSO : AttackPatternSO
    {
        [SerializeField] GameObject _projectile;
        public override void PerformAttack(Transform shootingStartPoint)
        {
            if (_projectile != null && shootingStartPoint!= null)
                Instantiate(_projectile, shootingStartPoint.position, shootingStartPoint.rotation);
        }
    }
}