using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetectorRight : MonoBehaviour
{
        Collider2D coll;

        public List<Collider2D> detectedCollidersRight = new List<Collider2D>();

        private void Awake()
        {
                coll = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                detectedCollidersRight.Add(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
                detectedCollidersRight.Remove(collision);
        }
}
