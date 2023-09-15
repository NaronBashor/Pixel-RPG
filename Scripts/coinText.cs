using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coinText : MonoBehaviour
{
        Text text;
        public static int totalCoins;

        // Start is called before the first frame update
        void Start()
        {
                totalCoins = 0;
                text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
                text.text = totalCoins.ToString();
        }
}
