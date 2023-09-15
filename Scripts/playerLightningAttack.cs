using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLightningAttack : MonoBehaviour
{
        private int damage = 20;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.gameObject.tag == "Enemy")
                {
                        collision.gameObject.GetComponent<damageable>().Hit(damage);
                }
        }
}
