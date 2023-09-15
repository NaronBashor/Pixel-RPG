using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDamage : MonoBehaviour
{
        Animator anim;

        private int damage = 5;

        public bool _isAlive;

        public bool IsAlive
        {
                get
                {
                        return anim.GetBool("isAlive");
                }
        }

        private void Awake()
        {
                anim = GetComponent<Animator>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Player"))
                {
                        if (IsAlive)
                        {
                                collision.gameObject.GetComponent<damageable>().Hit(damage);
                        }                        
                }
        }
}
