using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
        Animator anim;

        [SerializeField] private AudioSource coinCollect;
        [SerializeField] private AudioSource keyCollect;
        [SerializeField] private AudioSource foodCollect;

        [SerializeField] private int healthRestore = 20;

        public void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision != null)
                {
                        if (collision.CompareTag("Coin"))
                        {
                                coinCollect.Play();
                                coinText.totalCoins += 1;
                                Destroy(collision.gameObject);
                        }

                        if (collision.CompareTag("Key"))
                        {
                                keyCollect.Play();
                                collision.gameObject.GetComponent<Animator>().SetTrigger("coinPickup");
                                keyCounter.totalKeys += 1;
                                StartCoroutine(destroyObj());
                                IEnumerator destroyObj()
                                {
                                        yield return new WaitForSeconds(.4f);
                                        Destroy(collision.gameObject);
                                }
                        }

                        if (collision.CompareTag("Food"))
                        {
                                damageable damageable = GetComponent<damageable>();

                                bool wasHealed = damageable.Heal(healthRestore);
                                if (wasHealed)
                                {
                                        foodCollect.Play();
                                        Destroy(collision.gameObject);
                                }
                        }
                }                
        }
}
