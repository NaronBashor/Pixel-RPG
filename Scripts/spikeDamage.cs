using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeDamage : MonoBehaviour
{
        [SerializeField] private int damage = 200;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Player"))
                {
                        Vector2 knockback = new Vector2(200, 300);
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback);
                }
        }
}
