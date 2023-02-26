using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YK.HealthSystem
{
    public class Health : MonoBehaviour, IHittable
    {
        [field:SerializeField]
        public int currentHealth { get; private set; }
        public UnityEvent _onHit, _onDeath;
        public void GetHit(int damage, GameObject sender)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                _onDeath?.Invoke();
            }
            else
            {
                _onHit?.Invoke();
            }
        }

        public void InitializeHealth(int startingHealth)
        {
            if (startingHealth < 0)
                startingHealth = 0;

            currentHealth = startingHealth;
        }
    }
}