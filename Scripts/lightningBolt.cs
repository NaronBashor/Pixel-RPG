using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBolt : MonoBehaviour
{
        Animator anim;
        Collider2D coll;
        Rigidbody2D rb;

        private int damage = 50;

        // Start is called before the first frame update
        void Awake()
        {
                anim = GetComponent<Animator>();
                coll = GetComponent<Collider2D>();
                rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Enemy"))
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                }
                if (collision.CompareTag("Ground"))
                {
                        anim.SetTrigger("lightningBoltAttack");
                        rb.velocity = Vector2.zero;                                     
                        StartCoroutine(lightningBoltCooldown());
                }
        }

        private IEnumerator lightningBoltCooldown()
        {
                yield return new WaitForSeconds(.8f);
                Destroy(gameObject);
        }
}
