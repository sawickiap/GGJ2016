using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spell : MonoBehaviour
    {

        public Tower tower;
        public Enemy targetEnemy;
        public float movementSpeed;

        public Spell( Enemy targetEnemy, Tower tower, float movementSpeed )
        {

            this.targetEnemy = targetEnemy;
            this.tower = tower;
            this.movementSpeed = movementSpeed;

        }

        void Update()
        {

            if( targetEnemy != null )
            {

                Vector3 spellPos = this.gameObject.transform.position;
                Vector3 enemyPos = targetEnemy.gameObject.transform.position;

                Vector3 distanceDiffVector = enemyPos - spellPos;
                float distanceDiff = distanceDiffVector.magnitude;
                
                // Go to the target enemy
                if( distanceDiff < 0.1f )
                {
                    Destroy(this.gameObject);

                    targetEnemy.health -= tower.damage;

                    if (targetEnemy.health < 0)
                    {
                        Destroy(targetEnemy.gameObject);
                    }

                    tower.gameController.enemies.Remove(targetEnemy);

                }
                else
                {
                    Vector3 newSpellPos = spellPos + movementSpeed * distanceDiffVector.normalized * Time.deltaTime;
                    this.gameObject.transform.position = newSpellPos;
                }

            }
            else
            {
                Destroy(this.gameObject);
            }
            

        }

    }
}
