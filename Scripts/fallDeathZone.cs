using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallDeathZone : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision != null)
                {
                        if (collision.CompareTag("Player"))
                        {
                                collision.gameObject.GetComponent<Animator>().SetBool("isAlive", false);
                        }
                }
        }
}
