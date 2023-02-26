using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YK.EnemyAI
{
    public class AimActionAI : MonoBehaviour
    {
        Player _player;
        private void Awake()
        {
            _player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_player.isAlive)
            {
                Vector3 desiredDirection = _player.transform.position - transform.position;
                float desiredAngle = Mathf.Atan2(desiredDirection.y, desiredDirection.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
            }
        }
    }
}