using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midasTouch : MonoBehaviour
{
        Animator anim;
        Collider2D coll;
        Rigidbody2D rb;

        private int damage = 25;

        private void Awake()
        {
                anim = GetComponent<Animator>();
                coll = GetComponent<Collider2D>();
                rb = GetComponent<Rigidbody2D>();
                anim.SetTrigger("midasTouch");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Enemy"))
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                }
                if (collision.CompareTag("Ground"))
                {                        
                        rb.velocity = Vector2.zero;
                        StartCoroutine(midasTouchCooldown());
                }
        }

        private IEnumerator midasTouchCooldown()
        {
                yield return new WaitForSeconds(.6f);
                Destroy(gameObject);
        }
}
