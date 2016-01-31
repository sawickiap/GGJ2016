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

        public Spell( Enemy targetEnemy, Tower tower )
        {

            this.targetEnemy = targetEnemy;
            this.tower = tower;

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
                }
                else
                {
                    Vector3 newSpellPos = spellPos + tower.fireSpeed / 100.0f * distanceDiffVector;
                    this.gameObject.transform.position = newSpellPos;
                }

            }
            

        }

    }
}
