using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceAttackDamage : MonoBehaviour
{
        [SerializeField] private int damage = 60;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Player"))
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 600));
                        StartCoroutine(Delay());
                        IEnumerator Delay()
                        {
                                yield return new WaitForSeconds(.6f);
                                Destroy(gameObject);
                        }
                }
        }
}
