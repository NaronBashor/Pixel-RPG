using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tinyMonsterAttack : MonoBehaviour
{
        [SerializeField] private int damage = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Player"))
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                }
        }
}
