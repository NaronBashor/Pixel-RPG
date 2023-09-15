using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victoryFlag : MonoBehaviour
{
        [SerializeField] private AudioSource victoryFlagSound;

        private bool soundPlayed = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Player"))
                {
                        if (!soundPlayed)
                        {
                                victoryFlagSound.Play();
                                soundPlayed = true;
                        }
                        collision.gameObject.GetComponent<Animator>().SetBool("victory", true);
                        StartCoroutine(Delay());
                        IEnumerator Delay()
                        {
                                yield return new WaitForSeconds(2);
                                SceneManager.LoadScene("EndScreen");
                        }
                }
        }
}
