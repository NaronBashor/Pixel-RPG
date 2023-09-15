using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ladderClimbing : MonoBehaviour
{
        private float vertical;
        private float speed = 3f;
        private bool isLadder;
        private bool isClimbing;

        [SerializeField] private AudioSource ladderSound;

        playerController playerController;

        Rigidbody2D rb;
        Animator anim;

        Vector2 moveInput;

        private void Awake()
        {
                rb = GetComponent<Rigidbody2D>();
                anim = GetComponent<Animator>();
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
                moveInput = ctx.ReadValue<Vector2>();
        }

        private void Update()
        {
                vertical = moveInput.y;

                if (isLadder && moveInput.y > 0)
                {
                        isClimbing = true;
                        ladderSound.enabled = true;
                        anim.SetBool("isClimbing", true);
                }
                else if (isLadder && Mathf.Abs(moveInput.y) <= 0)
                {
                        isClimbing = false;
                        ladderSound.enabled = false;
                        anim.SetBool("isClimbing", false);
                }
        }

        private void FixedUpdate()
        {
                if (isClimbing)
                {
                        rb.gravityScale = 0f;
                        rb.velocity = new Vector2(rb.velocity.x, vertical * speed);                   
                }
                else
                {
                        rb.gravityScale = 3f;
                        anim.SetBool("isClimbing", false);
                }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.CompareTag("Ladder"))
                {
                        isLadder = true;
                }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
                if (collision.CompareTag("Ladder"))
                {
                        isLadder = false;
                        isClimbing = false;
                        ladderSound.enabled = false;
                }
        }
}
