using System.Collections;
using System.Collections.Generic;
using Spaceship;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float chasingThresholdDistance;
        [SerializeField] private EnemySpaceship enemySpaceship;

        private PlayerSpaceship spawnedPlayerShip;

        public void Init(PlayerSpaceship playerSpaceship)
        {
            spawnedPlayerShip = playerSpaceship;
        }
        
        private void Update()
        {
            MoveToPlayer();
            enemySpaceship.Fire();
            this.transform.LookAt(new Vector3(0, 0, 50));
        }

         private void MoveToPlayer()
         {
             // TODO: Implement this later
             var distanceToPlayer = Vector2.Distance(spawnedPlayerShip.transform.position, transform.position);
             if (distanceToPlayer < chasingThresholdDistance)
             {
                 var direction = (spawnedPlayerShip.transform.position - transform.position);
                 direction.Normalize();
                 var distance = direction * enemySpaceship.Speed * Time.deltaTime;
                 gameObject.transform.Translate(distance);
             }
         }
         
    }    
}

