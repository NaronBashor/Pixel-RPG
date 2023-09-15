using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyCounter : MonoBehaviour
{
        Text text;
        public static int totalKeys;

        // Start is called before the first frame update
        void Start()
        {
                totalKeys = 0;
                text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
                text.text = totalKeys.ToString();
        }
}
