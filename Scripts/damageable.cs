using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class damageable : MonoBehaviour
{
        Animator anim;
        Rigidbody2D rb;

        [SerializeField] private int _health = 100;
        [SerializeField] private int _maxHealth = 100;

        private bool _isAlive = true;

        public int Health
        {
                get
                {
                        return _health;
                }
                set
                {
                        _health=value;
                        if (_health <= 0)
                        {
                                IsAlive = false;
                        }
                }
        }
        public int MaxHealth
        {
                get => _maxHealth;
                set => _maxHealth=value;
        }
        public bool IsAlive
        {
                get
                {
                        return _isAlive;
                }
                set
                {
                        _isAlive=value;
                        anim.SetBool("isAlive", value);
                }
        }

        void Awake()
        {
                anim = GetComponent<Animator>();
                rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
                if (!_isAlive)
                {
                        rb.gravityScale = 1f;
                }
        }

        public bool Hit(int damage)
        {
                if (IsAlive)
                {
                        Health -= damage;

                        anim.SetTrigger("hit");
                        CharacterEvents.characterDamaged.Invoke(gameObject, damage);
                        return true;
                }
                return false;
        }

        public bool Heal(int healthRestore)
        {
                if (IsAlive && Health < MaxHealth)
                {
                        int maxHeal = Mathf.Max(MaxHealth - Health, 0);
                        int actualHeal = Mathf.Min(maxHeal, healthRestore);
                        Health += actualHeal;
                        CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
                        return true;
                }
                return false;
        }
}
