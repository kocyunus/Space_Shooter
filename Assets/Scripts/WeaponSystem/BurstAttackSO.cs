using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.WeaponSystem
{
    [CreateAssetMenu(menuName =("Attacks/BurstAttack"))]
    public class BurstAttackSO : AttackPatternSO
    {
        [SerializeField] GameObject _projectile;

        [SerializeField] float offset = 0.75f;
        public override void PerformAttack(Transform shootingStartPoint)
        {
            Vector3 offsetDirection = shootingStartPoint.rotation * new Vector3(0, offset, 0);

            Instantiate(_projectile, shootingStartPoint.position, shootingStartPoint.rotation);

            Instantiate(_projectile, shootingStartPoint.position + offsetDirection, shootingStartPoint.rotation);

            Instantiate(_projectile, shootingStartPoint.position - offsetDirection, shootingStartPoint.rotation);
        }
    }
}