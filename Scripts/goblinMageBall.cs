using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinMageBall : MonoBehaviour
{
        [SerializeField] private int damage = 10;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.gameObject.tag == "Player")
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                        Destroy(gameObject);
                }
                if (collision.gameObject.tag == "Ground")
                {
                        Destroy(gameObject);
                }
        }
}
