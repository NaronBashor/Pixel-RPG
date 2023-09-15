using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class enemyGoblinController : MonoBehaviour
{
        Rigidbody2D rb;
        Animator anim;
        Collider2D coll;
        SpriteRenderer sprite;
        itemDrop itemDrop;

        [SerializeField] private groundDetectorLeft groundDetectorLeft;
        [SerializeField] private groundDetectorRight groundDetectorRight;
        [SerializeField] private float groundDistance;
        [SerializeField] private float wallDistance;
        [SerializeField] private float enemyWalkSpeed;
        [SerializeField] private float leftLineOfSight;
        [SerializeField] private float rightLineOfSight;            
        [SerializeField] private bool leftPlayerHit;
        [SerializeField] private bool rightPlayerHit;
        [SerializeField] LayerMask groundMask;
        [SerializeField] LayerMask playerMask;
        [SerializeField] GameObject playerObject;
        [SerializeField] private float fireRate;
        [SerializeField] private float ballAttackSpeed;
        [SerializeField] GameObject ballPrefab;

        private float nextFireTime;

        Vector2 walkDirection;
        
        private int _dirX;
        private Transform player;

        private bool _isMoving;
        private bool _isTouchWallLeft;
        private bool _isTouchWallRight;
        [SerializeField] private bool _groundDetectRight;
        [SerializeField] private bool _groundDetectLeft;
        private bool _isGrounded;
        private bool _isFacingRight;
        private bool _isAlive;
        private bool _canMove;
        private bool flip;

        private bool CanMove
        {
                get
                {
                        return anim.GetBool("canMove");
                }
        }

        private int DirX
        {
                get
                {
                        return _dirX;
                }
                set
                {
                        _dirX = value;
                        anim.SetInteger("dirX", value);
                }
        }

        private bool IsFacingRight
        {
                get
                {
                        return anim.GetBool("isFacingRight");
                }
        }

        public bool IsAlive
        {
                get
                {
                        return anim.GetBool("isAlive");
                }
        }

        private bool IsGrounded
        {
                get
                {
                        return anim.GetBool("isGrounded");
                }
        }

        private bool GroundDetectRight
        {
                get
                {
                        return _groundDetectRight;
                }
                set
                {
                        _groundDetectRight = value;
                        anim.SetBool("groundDetectRight", value);
                }
        }

        private bool GroundDetectLeft
        {
                get
                {
                        return _groundDetectLeft;
                }
                set
                {
                        _groundDetectLeft = value;
                        anim.SetBool("groundDetectLeft", value);
                }
        }

        private bool IsTouchWallLeft
        {
                get
                {
                        return anim.GetBool("isTouchWallLeft");
                }
        }

        private bool IsTouchWallRight
        {
                get
                {
                        return anim.GetBool("isTouchWallRight");
                }
        }

        private bool IsMoving
        {
                get
                {
                        return _isMoving;
                }
                set
                {
                        _isMoving = value;
                        anim.SetBool("isMoving", value);
                }
        }

        private void Awake()
        {
                rb = GetComponent<Rigidbody2D>();
                anim = GetComponent<Animator>();
                coll = GetComponent<Collider2D>();
                sprite = GetComponent<SpriteRenderer>();
                itemDrop = GetComponent<itemDrop>();
                player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
                RaycastDetectorsRight();
                RaycastDetectorsLeft();
                if (rightPlayerHit || leftPlayerHit)
                {
                        LocalScale();
                        Attack();
                        anim.SetBool("canMove", false);
                }
                if (!rightPlayerHit && !leftPlayerHit)
                {
                        Movement();
                        anim.SetBool("canMove", true);
                }
                if (!IsAlive)
                {
                        itemDrop.DroppedLoot();
                }
        }

        private void FixedUpdate()
        {
                GroundDetectRight = groundDetectorRight.detectedCollidersRight.Count > 0;
                GroundDetectLeft = groundDetectorLeft.detectedCollidersLeft.Count > 0;

                IsMoving = rb.velocity != Vector2.zero;

                anim.SetBool("isMoving", true);
                if (rb.velocity.x > 0)
                {
                        sprite.flipX = true;
                }
                else if (rb.velocity.x < 0)
                {
                        sprite.flipX = false;
                }
                DirX = (int)Mathf.Abs(rb.velocity.x);
        }

        private void Attack()
        {
                anim.SetBool("isMoving", true);
                if (sprite.flipX && nextFireTime < Time.time)
                {
                        anim.SetTrigger("isAttack");
                        Vector2 ballSpeed = new Vector2(2 * ballAttackSpeed, 0);
                        GameObject ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);
                        ballInstance.GetComponent<Rigidbody2D>().AddRelativeForce(ballSpeed);
                        nextFireTime = Time.time + fireRate;
                }
                if (!sprite.flipX && nextFireTime < Time.time)
                {
                        anim.SetTrigger("isAttack");
                        Vector2 ballSpeed = new Vector2(-2 * ballAttackSpeed, 0);
                        GameObject ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);
                        ballInstance.GetComponent<Rigidbody2D>().AddRelativeForce(ballSpeed);
                        nextFireTime = Time.time + fireRate;
                }
        }

        private void Movement()
        {
                if (IsAlive && CanMove)
                {
                        rb.velocity = new Vector2(enemyWalkSpeed * walkDirection.x, rb.velocity.y);
                        if (IsGrounded && IsTouchWallRight || !GroundDetectRight)
                        {
                                walkDirection = Vector2.left;
                        }
                        else if (IsGrounded && IsTouchWallLeft || !GroundDetectLeft)
                        {
                                walkDirection = Vector2.right;
                        }
                }
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
                RaycastHit2D rightHit = Physics2D.Raycast(coll.bounds.center, Vector2.right, leftLineOfSight, playerMask);
                if (rightHit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.right * leftLineOfSight, Color.green);
                        rightPlayerHit = true;
                }
                else if (!rightHit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.right * leftLineOfSight, Color.red);
                        rightPlayerHit = false;
                }
        }

        public void RaycastDetectorsLeft()
        {
                RaycastHit2D leftHit = Physics2D.Raycast(coll.bounds.center, Vector2.left, rightLineOfSight, playerMask);
                if (leftHit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.left * rightLineOfSight, Color.green);
                        leftPlayerHit = true;
                }
                else if (!leftHit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.left * rightLineOfSight, Color.red);
                        leftPlayerHit = false;
                }
        }
}
