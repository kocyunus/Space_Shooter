using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.WeaponSystem
{
    public abstract class AttackPatternSO : ScriptableObject
    {
        [SerializeField] protected float _attackDelay = 0.2f;
        public float attackDelay => _attackDelay;

        [SerializeField] AudioClip _attackSFX;
        public AudioClip attackSFX => _attackSFX;

        public abstract void PerformAttack(Transform shootingStartPoint);
    }
}