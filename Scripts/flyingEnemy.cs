using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class flyingEnemy : MonoBehaviour
{
        Animator anim;
        BoxCollider2D coll;
        SpriteRenderer sprite;
        Rigidbody2D rb;

        itemDrop itemDrop;

        Vector3 oldPosition;

        private bool _playerDetectedRight;
        private bool _playerDetectedLeft;
        private bool _canMove;
        private bool _isAlive;
        private float facingDir;

        private float nextFireTime;
        private int damage;

        [SerializeField] private int rockAttackSpeed;
        [SerializeField] private float fireRate;
        [SerializeField] Transform projectileLocation;
        [SerializeField] GameObject rockProjectile;
        [SerializeField] LayerMask playerMask;

        [SerializeField] private float leftLineOfSight;
        [SerializeField] private float rightLineOfSight;
        [SerializeField] private bool leftPlayerHit;
        [SerializeField] private bool rightPlayerHit;

        GameObject target;

        public bool PlayerDetectedRight
        {
                get
                {
                        return anim.GetBool("playerDetectedRight");
                }
        }

        public bool PlayerDetectedLeft
        {
                get
                {
                        return anim.GetBool("playerDetectedLeft");
                }
        }

        public bool IsAlive
        {
                get
                {
                        return anim.GetBool("isAlive");
                }
        }

        private bool CanMove
        {
                get
                {
                        return anim.GetBool("canMove");
                }
        }

        private void Awake()
        {
                anim = GetComponent<Animator>();
                sprite = GetComponent<SpriteRenderer>();
                coll = GetComponent<BoxCollider2D>();
                itemDrop = GetComponent<itemDrop>();
                rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
                Vector3 direction = (transform.position - oldPosition).normalized;
                oldPosition = transform.position;
                facingDir = direction.x;
        }

        private void Update()
        {
                RaycastDetectorsLeft();
                RaycastDetectorsRight();

                if (PlayerDetectedRight || PlayerDetectedLeft)
                {
                        LocalScale();
                        if (nextFireTime < Time.time)
                        {
                                EnemyAttack();
                                anim.SetTrigger("isAttack");
                        }
                }

                if (facingDir < 0)
                {
                        sprite.flipX = false;
                }
                else if (facingDir > 0)
                {
                        sprite.flipX = true;
                }

                if (!IsAlive)
                {
                        itemDrop.DroppedLoot();
                }
        }        

        private void EnemyAttack()
        {
                target = GameObject.FindGameObjectWithTag("Player");
                Vector2 rockProjectileSpeed = (target.transform.position - transform.position).normalized * rockAttackSpeed;
                GameObject rockInstance = Instantiate(rockProjectile, projectileLocation.position, Quaternion.identity);
                rockInstance.GetComponent<Rigidbody2D>().AddRelativeForce(rockProjectileSpeed);
                nextFireTime = Time.time + fireRate;
        }

        private void LocalScale()
        {
                if (leftPlayerHit)
                {
                        sprite.flipX = false;
                }
                else if (rightPlayerHit)
                {
                        sprite.flipX = true;
                }
        }

        public void RaycastDetectorsRight()
        {
                Vector2 bottomRight = new Vector2(coll.bounds.max.x, coll.bounds.min.y);
                RaycastHit2D rightHit = Physics2D.Raycast(bottomRight, Vector2.right, rightLineOfSight, playerMask);
                if (rightHit == GameObject.FindGameObjectWithTag("Player"))
                {
                        Debug.DrawRay(bottomRight, Vector2.right * rightLineOfSight, Color.green);
                        rightPlayerHit = true;
                        anim.SetBool("playerDetectedRight", true);
                }
                else if (!rightHit == GameObject.FindGameObjectWithTag("Player"))
                {
                        Debug.DrawRay(bottomRight, Vector2.right * rightLineOfSight, Color.red);
                        rightPlayerHit = false;
                        anim.SetBool("playerDetectedRight", false);
                }
        }

        public void RaycastDetectorsLeft()
        {
                Vector2 bottomLeft = new Vector2(coll.bounds.min.x, coll.bounds.min.y);
                RaycastHit2D leftHit = Physics2D.Raycast(bottomLeft, Vector2.left, leftLineOfSight, playerMask);
                if (leftHit == GameObject.FindGameObjectWithTag("Player"))
                {
                        Debug.DrawRay(bottomLeft, Vector2.left * leftLineOfSight, Color.green);
                        leftPlayerHit = true;
                        anim.SetBool("playerDetectedLeft", true);
                }
                else if (!leftHit == GameObject.FindGameObjectWithTag("Player"))
                {
                        Debug.DrawRay(bottomLeft, Vector2.left * leftLineOfSight, Color.red);
                        leftPlayerHit = false;
                        anim.SetBool("playerDetectedLeft", false);
                }
        }
}
