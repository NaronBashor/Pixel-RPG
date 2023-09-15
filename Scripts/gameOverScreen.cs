using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverScreen : MonoBehaviour
{
        public Text pointsText;

        public void Setup(int Coins)
        {
                gameObject.SetActive(true);
                pointsText.text = Coins.ToString() + " Coins";
        }
}
