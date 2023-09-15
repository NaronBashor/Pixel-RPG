using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour
{
        Animator anim;

        private void Awake()
        {
                anim = GetComponentInParent<Animator>();
        }

        public void OnTriggerEnter2D(Collider2D coll)
        {
                if (coll.CompareTag("Player"))
                {
                        anim.SetBool("playerDetected", true);
                }
        }

        public void OnTriggerExit2D(Collider2D coll)
        {
                anim.SetBool("playerDetected", false);
        }
}