using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingDirections : MonoBehaviour
{
        Animator anim;
        CapsuleCollider2D coll;

        [SerializeField] private float groundDistance;
        [SerializeField] private float wallDistanceRight;
        [SerializeField] private float wallDistanceLeft;
        [SerializeField] private float ceilingDistance;
        [SerializeField] LayerMask groundMask;

        private void Awake()
        {
                anim = GetComponent<Animator>();
                coll = GetComponent<CapsuleCollider2D>();
        }

        void Update()
        {
                CheckGrounded();
                CheckWallsRight();
                CheckWallsLeft();
                CheckCeiling();
        }

        private void CheckGrounded()
        {
                RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, groundDistance, groundMask);

                if (hit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.down * groundDistance, Color.green);
                        anim.SetBool("isGrounded", true);
                }
                else if (!hit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.down * groundDistance, Color.red);
                        anim.SetBool("isGrounded", false);
                }
        }

        private void CheckWallsRight()
        {
                RaycastHit2D hitRight = Physics2D.Raycast(coll.bounds.center, Vector2.right, wallDistanceRight, groundMask);

                if (hitRight)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.right * wallDistanceRight, Color.green);
                        anim.SetBool("isTouchWallRight", true);
                }
                else if (!hitRight)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.right * wallDistanceRight, Color.red);
                        anim.SetBool("isTouchWallRight", false);
                }
                
        }

        private void CheckWallsLeft()
        {
                RaycastHit2D hitLeft = Physics2D.Raycast(coll.bounds.center, Vector2.left, wallDistanceLeft, groundMask);

                if (hitLeft)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.left * wallDistanceLeft, Color.green);
                        anim.SetBool("isTouchWallLeft", true);
                }
                else if (!hitLeft)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.left * wallDistanceLeft, Color.red);
                        anim.SetBool("isTouchWallLeft", false);
                }
        }

        private void CheckCeiling()
        {
                RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.up, ceilingDistance, groundMask);

                if (hit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.up * ceilingDistance, Color.green);
                        anim.SetBool("isTouchCeiling", true);
                }
                else if (!hit)
                {
                        Debug.DrawRay(coll.bounds.center, Vector2.up * ceilingDistance, Color.red);
                        anim.SetBool("isTouchCeiling", false);
                }
        }
}
