using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayAttackDamage : MonoBehaviour
{
        [SerializeField] private int damage = 50;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.gameObject.tag == "Player")
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                        StartCoroutine(Delay());
                        IEnumerator Delay()
                        {
                                yield return new WaitForSeconds(.4f);
                                Destroy(gameObject);
                        }
                }
                if (collision.gameObject.tag == "Ground")
                {
                        Destroy(gameObject);
                }
        }
}
