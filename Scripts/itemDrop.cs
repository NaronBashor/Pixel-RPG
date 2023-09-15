using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
        [SerializeField] private int minCoinLoot;
        [SerializeField] private int maxCoinLoot;
        [SerializeField] private float thrust;
        [SerializeField] GameObject itemPrefab;

        public bool canLoot = true;

        public void DroppedLoot()
        {
                if (canLoot)
                {
                        canLoot = false;
                        int count = Random.Range(this.minCoinLoot, this.maxCoinLoot);
                        for (int i = 0; i<count; i++)
                        {
                                Vector2 direction = new Vector2(Random.Range(-.5f, .5f), Random.Range(1.4f, 2.5f));
                                GameObject newCoin = Instantiate(itemPrefab, this.transform.position, Quaternion.identity);
                                newCoin.GetComponent<Rigidbody2D>().AddForce(direction* thrust);
                                StartCoroutine(OpenTag());
                                IEnumerator OpenTag()
                                {
                                        yield return new WaitForSeconds(1f);
                                        newCoin.tag = "Coin";
                                }
                        }
                }                
        }
}