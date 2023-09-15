using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class playerController : MonoBehaviour
{
        Rigidbody2D rb;
        Animator anim;
        CapsuleCollider2D coll;
        SpriteRenderer sprite;
        playerEnergy playerEnergy;

        private Vector2 moveInput;

        private float nextRockAttack;
        private float nextLightningAttack;
        private float nextLightningBoltAttack;
        private float nextMidasTouchAttack;

        [SerializeField] private AudioSource jumpSound;
        [SerializeField] private AudioSource deathSound;
        [SerializeField] private AudioSource runSound;
        [SerializeField] private AudioSource lightningAttack;
        [SerializeField] private AudioSource lightningBoltAttack;
        [SerializeField] private AudioSource midasTouchSound;
        [SerializeField] private AudioSource playerRockThrowSound;

        [SerializeField] private int walkSpeed;
        [SerializeField] private int runSpeed;
        [SerializeField] private float jumpForce;

        [SerializeField] gameOverScreen gameOverScreen;

        [SerializeField] GameObject rockProjectile;
        [SerializeField] GameObject lightningBolt;
        [SerializeField] GameObject midasTouch;

        [SerializeField] GameObject ground;

        [SerializeField] private int rockAttackSpeed;
        [SerializeField] private float rockFireRate;
        [SerializeField] private float lightningFireRate;
        [SerializeField] private float lightningBoltFireRate;
        [SerializeField] private float midasTouchFireRate;

        [SerializeField] private float castingArea;
        [SerializeField] Transform playerProjectileLocation;

        private bool deathSoundPlayed = false;

        private bool _isFacingRight;
        private bool _isGrounded;
        private bool _isRunning = false;
        private bool _isMoving = false;
        private bool _isTouchWallRight;
        private bool _isTouchWallLeft;
        private bool _energy;
        private bool _isAlive;
        private bool _victory;

        public bool Victory
        {
                get
                {
                        return anim.GetBool("victory");
                }
        }

        public float Energy
        {
                get
                {
                        return anim.GetFloat("energy");
                }
        }

        public bool IsFacingRight
        {
                get
                {
                        return _isFacingRight;
                }
                set
                {
                        _isFacingRight = value;
                        anim.SetBool("isFacingRight", value);
                }
        }

        public bool IsAlive
        {
                get
                {
                        return anim.GetBool("isAlive");
                }
        }

        public bool IsTouchWallRight
        {
                get
                {
                        return anim.GetBool("isTouchWallRight");
                }
        }

        public bool IsTouchWallLeft
        {
                get
                {
                        return anim.GetBool("isTouchWallLeft");
                }
        }

        public float CurrentMoveSpeed
        {
                get
                {
                        if (IsAlive)
                        {
                                if (IsMoving)
                                {
                                        if (IsRunning)
                                        {
                                                return runSpeed;
                                        }
                                        else
                                        {
                                                return walkSpeed;
                                        }
                                }
                        }
                        return 0;
                }
        }

        public bool IsMoving
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

        public bool IsGrounded
        {
                get
                {
                        return anim.GetBool("isGrounded");
                }
        }

        public bool CanMove
        {
                get
                {
                        return anim.GetBool("canMove");
                }
        }

        public bool IsRunning
        {
                get
                {
                        return _isRunning;
                }
                private set
                {
                        _isRunning = value;
                        anim.SetBool("isRunning", value);
                }
        }

        public void GameOver()
        {
                gameOverScreen.Setup(coinText.totalCoins);
        }

        // Start is called before the first frame update
        public void Awake()
        {
                rb = GetComponent<Rigidbody2D>();
                anim = GetComponent<Animator>();
                coll = GetComponent<CapsuleCollider2D>();
                sprite = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
                PlayerPush();
                if (!IsAlive)
                {
                        rb.velocity = Vector2.zero;
                        GameOver();
                        if (!deathSoundPlayed)
                        {
                                deathSound.Play();
                                deathSoundPlayed = true;
                        }
                }
                if (IsGrounded && IsRunning)
                {
                        runSound.enabled = true;
                }
                else if (!IsGrounded || !IsRunning)
                {
                        runSound.enabled = false;
                }
        }

        // Update is called once per frame
        public void FixedUpdate()
        {
                if (IsAlive && !Victory)
                {
                        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
                        anim.SetFloat("yVelocity", rb.velocity.y);
                        IsFacingRight = !sprite.flipX;
                }
                else if (Victory)
                {
                        StartCoroutine(Delay());
                        IEnumerator Delay()
                        {
                                yield return new WaitForSeconds(.4f);
                                rb.velocity = Vector2.zero;
                                anim.SetBool("isWalking", false);
                                anim.SetBool("isMoving", false);
                                anim.SetBool("isRunning", false);
                        }
                }
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
                if (IsAlive && !Victory)
                {
                        moveInput = ctx.ReadValue<Vector2>();
                        IsMoving = moveInput != Vector2.zero;

                        if (moveInput.x > 0)
                        {
                                anim.SetBool("isWalking", true);
                                sprite.flipX = false;
                        }
                        else if (moveInput.x < 0)
                        {
                                anim.SetBool("isWalking", true);
                                sprite.flipX = true;
                        }
                        else
                        {
                                anim.SetBool("isWalking", false);
                        }
                }
        }

        public void OnRun(InputAction.CallbackContext ctx)
        {
                if (IsAlive && !Victory)
                {
                        if (ctx.started && moveInput.x != 0)
                        {
                                IsRunning = true;
                        }
                        if (ctx.canceled || moveInput.x == 0 || !IsMoving)
                        {
                                IsRunning = false;
                        }
                }
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
                if (IsAlive && !Victory)
                {
                        if (IsAlive)
                        {
                                if (IsGrounded)
                                {
                                        if (ctx.started)
                                        {
                                                jumpSound.Play();
                                                rb.velocity = new Vector2(rb.velocity.x, 2 * jumpForce);
                                        }
                                }
                        }
                }
        }

        public void PlayerPush()
        {
                if (IsGrounded)
                {
                        if (IsTouchWallLeft)
                        {
                                if (IsMoving && moveInput.x < -0.01f)
                                {
                                        anim.SetBool("isPushingLeft", true);
                                }
                                else if (!IsMoving)
                                {
                                        anim.SetBool("isPushingLeft", false);
                                }
                        }
                        else if (!IsTouchWallLeft)
                        {
                                anim.SetBool("isPushingLeft", false);
                        }
                        if (IsTouchWallRight)
                        {
                                if (IsMoving && moveInput.x > 0.01f)
                                {
                                        anim.SetBool("isPushingRight", true);
                                }
                                else if (!IsMoving)
                                {
                                        anim.SetBool("isPushingRight", false);
                                }
                        }
                        else if (!IsTouchWallRight)
                        {
                                anim.SetBool("isPushingRight", false);
                        }
                }
                else if (!IsGrounded)
                {
                        anim.SetBool("isPushingRight", false);
                        anim.SetBool("isPushingLeft", false);
                }
        }

        public void OnRockAttack(InputAction.CallbackContext ctx)
        {
                if (IsAlive && !Victory && nextRockAttack < Time.time)
                {
                        if (ctx.started)
                        {
                                if (IsFacingRight)
                                {
                                        playerRockThrowSound.Play();
                                        Vector2 rockProjectileSpeed = new Vector2(2 * rockAttackSpeed, 0);
                                        GameObject rockInstance = Instantiate(rockProjectile, playerProjectileLocation.position, Quaternion.identity);
                                        rockInstance.GetComponent<Rigidbody2D>().AddRelativeForce(rockProjectileSpeed);
                                        anim.SetTrigger("attacking");
                                        anim.SetTrigger("isRockThrow");
                                        nextRockAttack = Time.time + rockFireRate;
                                }
                                else
                                {
                                        playerRockThrowSound.Play();
                                        Vector2 rockProjectileSpeed = new Vector2(-2 * rockAttackSpeed, 0);
                                        GameObject rockInstance = Instantiate(rockProjectile, playerProjectileLocation.position, Quaternion.identity);
                                        rockInstance.GetComponent<Rigidbody2D>().AddRelativeForce(rockProjectileSpeed);
                                        anim.SetTrigger("attacking");
                                        anim.SetTrigger("isRockThrow");
                                        nextRockAttack = Time.time + rockFireRate;
                                }
                        }
                }
        }

        public void OnLightningAttack(InputAction.CallbackContext ctx)
        {
                float lightningEnergy = 10;
                if (IsAlive && !Victory && Energy > 20 && nextLightningAttack < Time.time)
                {
                        if (ctx.started)
                        {
                                lightningAttack.Play();
                                anim.SetTrigger("attacking");
                                anim.SetTrigger("lightningAttack");
                                playerEnergy playerEnergy = GetComponent<playerEnergy>();
                                playerEnergy.EnergyAttack(lightningEnergy);
                                var childComponent = GetComponentsInChildren<Animator>();
                                foreach (var child in childComponent)
                                {
                                        child.SetTrigger("lightningAttack");
                                }
                                nextLightningAttack = Time.time + lightningFireRate;
                        }
                }
        }

        public void OnLightningBolt(InputAction.CallbackContext ctx)
        {
                float lightningBoltEnergy = 50;
                if (IsAlive && !Victory && Energy > 50 && nextLightningBoltAttack < Time.time)
                {
                        if (ctx.started)
                        {
                                playerEnergy playerEnergy = GetComponent<playerEnergy>();
                                playerEnergy.EnergyAttack(lightningBoltEnergy);
                                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                Vector2 spawnBolt = new Vector2(mousePos.x, 5f);
                                Vector2 boltSpeed = Vector2.down * 500;
                                GameObject lightningBoltInstance = Instantiate(lightningBolt, spawnBolt, Quaternion.identity);
                                lightningBoltInstance.GetComponent<Rigidbody2D>().AddForce(boltSpeed);
                                StartCoroutine(Delay());
                                IEnumerator Delay()
                                {
                                        yield return new WaitForSeconds(.5f);
                                        lightningBoltAttack.Play();
                                }

                                nextLightningBoltAttack = Time.time + lightningBoltFireRate;
                        }
                }
        }

        public void OnMidasTouch(InputAction.CallbackContext ctx)
        {
                float midasTouchEnergy = 50;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distanceFromPlayer = Vector2.Distance(transform.position, mousePos);

                if (IsAlive && !Victory && Energy > 50 && nextMidasTouchAttack < Time.time && distanceFromPlayer < castingArea)
                {
                        if (ctx.started)
                        {
                                midasTouchSound.Play();
                                playerEnergy playerEnergy = GetComponent<playerEnergy>();
                                playerEnergy.EnergyAttack(midasTouchEnergy);
                                GameObject midasTouchInstance = Instantiate(midasTouch, mousePos, Quaternion.identity);

                                nextMidasTouchAttack = Time.time + midasTouchFireRate;
                        }
                }
        }

        private void OnDrawGizmosSelected()
        {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, castingArea);
        }
}
