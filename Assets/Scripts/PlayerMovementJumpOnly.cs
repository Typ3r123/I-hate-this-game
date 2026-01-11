using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovementJumpOnly : MonoBehaviour
{
    //------- Основные компоненты -------
    public Rigidbody2D rb;
    public Animator anim;

    //------- Параметры движения -------
    public int speed = 3;
    public float deceleration = 10f;

    //------- Параметры прыжка -------
    public int jumpForce = 10;

    //------- Двойной прыжок (галочка!) -------
    public bool enableDoubleJump = false;
    private int extraJumpsValue;
    private const int extraJumps = 1;

    //------- Для проверки земли -------
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;

    //------- Для движения и отражения -------
    private Vector2 moveVector;
    public bool faceRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;

        if (enableDoubleJump)
            extraJumpsValue = extraJumps;
        else
            extraJumpsValue = 0;
    }

    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
    }

    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");

        // Движение только если игрок в воздухе (не на земле)
        if (!onGround && moveVector.x != 0)
        {
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(
                Mathf.MoveTowards(rb.velocity.x, 0, deceleration * Time.deltaTime),
                rb.velocity.y
            );
        }

        anim.SetFloat("moveX", Mathf.Abs(rb.velocity.x));
    }

    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            faceRight = !faceRight;
        }
    }

    public JumpSwitcher blockSwitcher;

    void Jump()
    {
        if (onGround && enableDoubleJump)
        {
            extraJumpsValue = extraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            blockSwitcher?.ToggleState();

            if (onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (enableDoubleJump && extraJumpsValue > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                extraJumpsValue--;
            }
        }
    }

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
}
