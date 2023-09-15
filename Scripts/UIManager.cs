using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
        public GameObject damageTextPrefab;
        public GameObject healthTextPrefab;

        public Canvas gameCanvas;

        private void Awake()
        {
                gameCanvas = FindObjectOfType<Canvas>();                
        }

        private void OnEnable()
        {
                CharacterEvents.characterDamaged += (CharacterTookDamage);
                CharacterEvents.characterHealed += (CharacterHealed);
        }

        private void OnDisable()
        {
                CharacterEvents.characterDamaged -= (CharacterTookDamage);
                CharacterEvents.characterHealed -= (CharacterHealed);
        }

        public void CharacterTookDamage(GameObject character, int damageRecieved)
        {
                Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

                TMP_Text tmptext = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

                tmptext.text = damageRecieved.ToString();
        }

        public void CharacterHealed(GameObject character, int healthRestored)
        {
                Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

                TMP_Text tmptext = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

                tmptext.text = healthRestored.ToString();
        }
}
