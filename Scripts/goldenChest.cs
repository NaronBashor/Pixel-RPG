using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class goldenChest : MonoBehaviour
{
        Animator anim;

        [SerializeField] private int minCoinReward = 3;
        [SerializeField] private int maxCoinReward = 8;
        [SerializeField] private float thrust = 20f;
        [SerializeField] GameObject coinPrefab;
        [SerializeField] private AudioSource chestOpen;

        private bool chestIsOpen = false;

        Vector2 newForce;

        private void Awake()
        {
                anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision != null)
                {
                        if (collision.CompareTag("Player") && !chestIsOpen && keyCounter.totalKeys > 0)
                        {
                                chestOpen.Play();
                                anim.SetTrigger("open");
                                int count = Random.Range(this.minCoinReward, this.maxCoinReward);
                                chestIsOpen = true;
                                keyCounter.totalKeys--;
                                StartCoroutine(OpenDelay());
                                IEnumerator OpenDelay()
                                {
                                        yield return new WaitForSeconds(1f);
                                        for (int i = 0; i < count; i++)
                                        {
                                                Vector2 direction = new Vector2(Random.Range(-.5f, .5f), Random.Range(1.4f, 2.5f));
                                                GameObject newItem = Instantiate(coinPrefab, this.transform.position, Quaternion.identity);
                                                newItem.GetComponent<Rigidbody2D>().AddForce(direction * thrust);
                                                StartCoroutine(OpenTag());
                                                IEnumerator OpenTag()
                                                {
                                                        yield return new WaitForSeconds(1f);
                                                        newItem.tag = "Coin";
                                                }
                                        }
                                }
                        }
                }                
        }
}
