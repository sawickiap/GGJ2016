using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Models.Animated.Scripts
{
    class EnemyController : MonoBehaviour
    {

        public bool attacking;
        public bool dead;
        public bool idle;
        public float speed;
        public bool takingDamage;
        public bool knockback;


        private Animator animator;

        // Use this for initialization
        void Start()
        {

            speed = 0.0f;
            dead = false;
            takingDamage = false;
            knockback = false;
            attacking = false;
            idle = true;

            animator = GetComponentInChildren<Animator>();

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Speed = 1.5f;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Attacking = true;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Dead = true;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                TakingDamage = true;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                Knockback = true;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                Idle = true;
            }

        }


        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                this.speed = value;
                animator.SetFloat("Speed", speed);
            }
        }

        public bool Attacking
        {
            get
            {
                return attacking;
            }
            set
            {

                this.attacking = value;
                if (attacking)
                {
                    animator.SetTrigger("Attacking");
                }

            }
        }

        public bool Dead
        {
            get
            {
                return dead;
            }
            set
            {

                this.dead = value;
                if (dead)
                {
                    animator.SetTrigger("Dead");
                }

            }
        }

        public bool Idle
        {
            get
            {
                return idle;
            }
            set
            {

                this.idle = value;
                if (idle)
                {
                    animator.SetTrigger("Idle");
                }

            }
        }

        public bool Knockback
        {
            get
            {
                return knockback;
            }
            set
            {

                this.knockback = value;
                if (knockback)
                {
                    animator.SetTrigger("Knockback");
                }

            }
        }

        public bool TakingDamage
        {
            get
            {
                return takingDamage;
            }
            set
            {

                this.takingDamage = value;
                if (takingDamage)
                {
                    animator.SetTrigger("TakingDamage");
                }

            }
        }


    }
}
