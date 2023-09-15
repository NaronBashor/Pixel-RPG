using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalGameCoins : MonoBehaviour
{
        Text text;
        public static int savedCoins;

        private void Start()
        {
                text = GetComponent<Text>();
        }

        private void Update()
        {
                text.text = savedCoins.ToString();
        }
}
