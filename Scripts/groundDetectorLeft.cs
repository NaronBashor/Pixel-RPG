using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetectorLeft : MonoBehaviour
{
        Collider2D coll;

        public List<Collider2D> detectedCollidersLeft = new List<Collider2D>();

        private void Awake()
        {
                coll = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                detectedCollidersLeft.Add(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
                detectedCollidersLeft.Remove(collision);
        }
}
