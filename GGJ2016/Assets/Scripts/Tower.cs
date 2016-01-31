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
        public int fireSpeed;
        TowerType towerType;

        public Tower( int damage, int fireSpeed, TowerType towerType )
        {

            this.damage = damage;
            this.fireSpeed = fireSpeed;
            this.towerType = towerType;

        }
        

        public void Attack()
        {
        }

    }
}
