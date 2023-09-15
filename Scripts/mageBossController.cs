using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageBossController : MonoBehaviour
{
        Rigidbody2D rb;
        BoxCollider2D boxColl;
        CapsuleCollider2D capsColl;
        Animator anim;

        [SerializeField] LayerMask playerMask;
        [SerializeField] GameObject player;
        [SerializeField] private bool reRoll = true;
        [SerializeField] private string prevAttack;
        [SerializeField] GameObject rayPrefab;
        [SerializeField] GameObject fireballPrefab;
        [SerializeField] GameObject icePrefab;
        [SerializeField] GameObject victoryFlag;
        [SerializeField] Transform rayLocation;
        [SerializeField] Transform fireballLocation;
        [SerializeField] private float rayProjectileMultiplier;
        [SerializeField] private float fireballProjectileMultiplier;
        [SerializeField] private float bossWalkSpeed;
        [SerializeField] private float playerDistance;
        [SerializeField] private float attackRange;
        [SerializeField] private float playerDetectZone;

        private bool flag = false;

        public bool PlayerDetected
        {
                get
                {
                        return anim.GetBool("playerDetected");
                }
        }

        public bool CanAttack
        {
                get
                {
                        return anim.GetBool("canAttack");
                }
        }

        public bool IsAlive
        {
                get
                {
                        return anim.GetBool("isAlive");
                }
        }

        public bool CanMove
        {
                get
                {
                        return anim.GetBool("canMove");
                }
        }

        private void Awake()
        {
                rb = GetComponent<Rigidbody2D>();
                boxColl = GetComponent<BoxCollider2D>();
                capsColl = GetComponent<CapsuleCollider2D>();
                anim = GetComponent<Animator>();
        }

        private void Update()
        {
                capsColl.includeLayers.Equals(true);
                playerDistance = Vector2.Distance(player.transform.position, transform.position);

                if (playerDistance < playerDetectZone && playerDistance > attackRange)
                {
                        if (CanMove && playerDistance > 5f)
                        {
                                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, bossWalkSpeed * Time.deltaTime);
                        }
                }
                if (IsAlive && playerDistance <= attackRange)
                {
                        MageAttack();
                }
                else if (!IsAlive && !flag)
                {
                        VictoryFlag();
                        gameObject.layer = LayerMask.NameToLayer("Player");
                }
        }

        public void VictoryFlag()
        {
                flag = true;
                StartCoroutine(Delay());
                IEnumerator Delay()
                {
                        yield return new WaitForSeconds(2);
                        Vector2 flagLocation = transform.position;
                        var flagInstance = Instantiate(victoryFlag, flagLocation, Quaternion.identity);                        
                }
        }

        public void MageAttack()
        {

                if (reRoll)
                {
                        int randomNumber = Random.Range(1, 4);

                        if (randomNumber == 1 && prevAttack != "ice")
                        {
                                reRoll = false;
                                prevAttack = "ice";
                                anim.SetTrigger("iceAttack");
                                IceAttack();
                                Debug.Log("ice attack");
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(4);
                                        reRoll = true;
                                }
                        }
                        else if (randomNumber == 2 && prevAttack != "fire")
                        {
                                reRoll = false;
                                prevAttack = "fire";
                                anim.SetTrigger("fireAttack");
                                FireballAttack();
                                Debug.Log("fire attack");
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(4);
                                        reRoll = true;
                                }
                        }
                        else if (randomNumber == 3 && prevAttack != "ray")
                        {
                                reRoll = false;
                                prevAttack = "ray";
                                anim.SetTrigger("rayAttack");
                                RayAttack();
                                Debug.Log("ray attack");
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(4);
                                        reRoll = true;
                                }
                        }
                }
        }

        public void RayAttack()
        {
                StartCoroutine(Delay());
                IEnumerator Delay()
                {
                        yield return new WaitForSeconds(.6f);
                        if (playerDistance < attackRange)
                        {
                                Vector2 raySpeed = Vector2.left * rayProjectileMultiplier;
                                GameObject rayInstance = Instantiate(rayPrefab, rayLocation.position, Quaternion.identity);
                                rayInstance.GetComponent<Rigidbody2D>().AddRelativeForce(raySpeed);
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(3);
                                        Destroy(rayInstance);
                                }
                        }
                }
        }

        public void IceAttack()
        {
                StartCoroutine(Delay());
                IEnumerator Delay()
                {
                        yield return new WaitForSeconds(.6f);
                        if (playerDistance < attackRange)
                        {
                                GameObject iceInstance = Instantiate(icePrefab, player.transform.position, Quaternion.identity);
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(2);
                                        Destroy(iceInstance);
                                }
                        }
                }
        }

        public void FireballAttack()
        {
                StartCoroutine(Delay());
                IEnumerator Delay()
                {
                        yield return new WaitForSeconds(.4f);
                        if (playerDistance < attackRange)
                        {
                                Vector2 fireballSpeed = Vector2.left * fireballProjectileMultiplier;
                                GameObject fireballInstance = Instantiate(fireballPrefab, fireballLocation.position, Quaternion.identity);
                                fireballInstance.GetComponent<Rigidbody2D>().AddForce(fireballSpeed);
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(4);
                                        Destroy(fireballInstance);
                                }
                        }
                }
        }

        private void OnDrawGizmosSelected()
        {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, attackRange);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, playerDetectZone);
        }
}
