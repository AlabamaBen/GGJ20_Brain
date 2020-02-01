﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public Animator myAnimator { get; private set; }

    [SerializeField]
    protected float movementSpeed = 4;
    [SerializeField]
    protected float maxSpeed = 40;


    private Vector2 direction;

    private bool isJumping;


    [SerializeField]
    private RuntimeAnimatorController aController;


    [SerializeField]
    private GameObject landPrefab;
    [SerializeField]
    private GameObject jumpPrefab;

    protected bool isFacingRight;

    //private Shake shake;

    //Other movements variables
    public Rigidbody2D rb { get; set; }
    Vector2 v;

    //Jump variables
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask groundMask;

    private bool isGrounded;
    private bool canJump;

    //Inputs
    float horizontal;
    bool jumpButton;

    public void Start()
    {
        //myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;

        //shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

        //moveAction.Initialize(myAnimator);
    }

    private void Update()
    {
        GetInputs();

        v = rb.velocity;

        Flip(horizontal);

        isGrounded = IsGrounded(rb);

        // v = HandleMovement(horizontal, v);
        v = HandleMovementAcceleration(horizontal, v);

        if (v.y < -0.2)
        {
            //myAnimator.SetBool("Land", true);

        }

        rb.velocity = v;

        if (jumpButton)
        {
            Jump(jumpForce);
        }

    }

    private void GetInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        jumpButton = Input.GetButtonDown("Jump");
    }

    //Makes the player turn the other way
    public void Flip(float horizontal)
    {
        //If needed, the player faces the other direction
        if ((horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight))
        {
            ChangeDirection();
        }
    }

    public Vector2 HandleMovement(float horizontal, Vector2 v)
    {
        v.x = horizontal * movementSpeed;
        //myAnimator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));
        return v;
    }

    public Vector2 HandleMovementAcceleration(float horizontal, Vector2 v)
    {
        if (horizontal != 0)
        {
            if (Mathf.Abs(v.x) < maxSpeed)
            {
                v.x += horizontal * movementSpeed;

            }
            else
            {
                v.x = horizontal * maxSpeed;
            }
        }
        else
        {
            v.x = horizontal;
        }
        return v;
    }

    private void ChangeDirection()
    {
        isFacingRight = !isFacingRight;
        rb.transform.localScale = new Vector3(rb.transform.localScale.x * -1, rb.transform.localScale.y, rb.transform.localScale.z);
    }

    private void Jump(float jumpValue)
    {
        // Player is not grounded when he jump
        if (isGrounded && canJump)
        {
            canJump = false;
            isGrounded = false;
            isJumping = true;
            this.transform.SetParent(null);
            //myAnimator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpValue, ForceMode2D.Impulse);


            AudioManager.instance.Play("Jump");
        }
    }

    public void Bump(float jumpValue)
    {
            canJump = false;
            isGrounded = false;
            isJumping = true;
            //myAnimator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpValue, ForceMode2D.Impulse);

            AudioManager.instance.Play("Jump");
    }

    private bool IsGrounded(Rigidbody2D MyRigidbody)
    {
        if (MyRigidbody.velocity.y <= 0.2)
        {
            //MyRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            foreach (Transform point in groundPoints)
            {
                //Gets the colliders on the player's feet
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, groundMask);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject && (colliders[i].gameObject.CompareTag("Floor")))
                    {
                        if(colliders[i].gameObject.GetComponent<PlatformSliding>() != null)
                        {
                            this.transform.SetParent(colliders[i].gameObject.transform);
                        }
                        //If the colliders collide with something else than the player, then the players is grounded
                        canJump = true;
                        isJumping = false;
                        /*myAnimator.ResetTrigger("Jump");
                        if (myAnimator.GetBool("Land"))
                        {
                            myAnimator.SetBool("Land", false);
                            //AudioManager.instance.Play(gameObject.name + "Land");
                            StartCoroutine("LandEffect");
                        }*/
                        return true;
                    }
                }
            }
        }
        this.transform.SetParent(null);
        return false;
    }

    public void Initialize(Animator MyAnimator)
    {
        //this.myAnimator = MyAnimator;
    }

}
