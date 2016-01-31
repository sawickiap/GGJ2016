using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tower : MonoBehaviour
    {

        public enum TowerType
        {
            CANNON_TOWER,
            FIRE_TOWER,
            MAGE_TOWER,
            GHOST_TOWER
        };

        public int damage;
        public float fireSpeed;
        TowerType towerType;
        public float range;

        public Spell spell;
        Enemy currentlyShootedEnemy = null;

        private LinkedList<GameObject> shootedSpells;
        public GameController gameController;

        float nextFireTime;

        void Start()
        {
            nextFireTime = Time.time + fireSpeed;
        }

        public Tower( int damage, int fireSpeed, TowerType towerType )
        {

            this.damage = damage;
            this.fireSpeed = fireSpeed;
            this.towerType = towerType;
            shootedSpells = new LinkedList<GameObject>();

        }
        
        void Update()
        {
            
            if( Time.time > nextFireTime )
            {

                Enemy nearestEnemyInRange = FindNearestEnemyInRange();

                Debug.Log(nearestEnemyInRange);

                if (nearestEnemyInRange != null)
                {
                    Shoot(nearestEnemyInRange);
                }

                nextFireTime += fireSpeed;

            }

            

        }

        private Enemy FindNearestEnemyInRange()
        {

            if( gameController == null || gameController.enemies == null)
            {
                return null;
            }

            if( gameController.enemies.Count == 0 )
            {
                return null;
            }

            Enemy nearestEnemy = null;
            const float MAX_DISTANCE_TO_ENEMY = 1000000.0f;
            float minDistanceToEnemy = MAX_DISTANCE_TO_ENEMY;

            foreach( Enemy enemy in gameController.enemies)
            {

                Vector3 vectorToEnemy = CalculateVectorFromTowerToEnemy(enemy);
                float distToCurrentEnemy = vectorToEnemy.magnitude;

                if( distToCurrentEnemy < minDistanceToEnemy )
                {
                    minDistanceToEnemy = distToCurrentEnemy;
                    nearestEnemy = enemy;
                }

            }

            if( minDistanceToEnemy < MAX_DISTANCE_TO_ENEMY )
            {
                return nearestEnemy;
            }

            return null;

        }

        public void Shoot( Enemy enemy )
        {

            if (enemy == null)
            {
                return;
            }

            float towerToEnemyDistance = 0.0f;

            // Calculate vector from top of the tower to the enemy.
            Vector3 towerToEnemyDir = CalculateVectorFromTowerToEnemy(enemy);
            Vector3 towerPos = this.gameObject.transform.position + new Vector3(0, 1, 0);

            towerToEnemyDistance = towerToEnemyDir.magnitude;
            Vector3 towerToEnemyDirNorm = towerToEnemyDir.normalized;

            // If the enemy is in range - shoot at him.

            if (towerToEnemyDistance <= range)
            {
                Spell newSpell = Instantiate(spell, towerPos, Quaternion.Euler(0, 0, 0)) as Spell;
                if( newSpell != null )
                {
                    newSpell.tower = this;
                    newSpell.targetEnemy = enemy;
                    newSpell.movementSpeed = 2;
                }
            }

        }

        private Vector3 CalculateVectorFromTowerToEnemy( Enemy enemy )
        {

            // Calculate vector from top of the tower to the enemy.
            Vector3 enemyPos = enemy.gameObject.transform.position;
            Vector3 towerPos = this.gameObject.transform.position + new Vector3(0, 1, 0);

            Vector3 towerToEnemyDir = enemyPos - towerPos;

            return towerToEnemyDir;

        }

    }
}
