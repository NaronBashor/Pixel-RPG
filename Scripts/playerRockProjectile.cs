using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRockProjectile : MonoBehaviour
{
        [SerializeField] private int damage = 10;
        [SerializeField] private AudioSource rockHitEnemy;

        private float clipLength;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.gameObject.tag == "Enemy")
                {
                        rockHitEnemy.Play();                        
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<CircleCollider2D>().enabled = false;
                        clipLength = rockHitEnemy.clip.length;
                        StartCoroutine(Delay());
                        IEnumerator Delay()
                        {
                                yield return new WaitForSeconds(clipLength);
                                Destroy(gameObject);
                        }
                }
                if (collision.gameObject.tag == "Ground")
                {
                        rockHitEnemy.Play();
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<CircleCollider2D>().enabled = false;
                        clipLength = rockHitEnemy.clip.length;
                        StartCoroutine(Delay());
                        IEnumerator Delay()
                        {
                                yield return new WaitForSeconds(clipLength);
                                Destroy(gameObject);
                        }
                }
        }
}
