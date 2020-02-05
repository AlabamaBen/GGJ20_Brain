using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator characterAnimator;
    public GameObject characterSprite;
    [SerializeField]
    protected float movementSpeed = 4;
    [SerializeField]
    protected float maxSpeed = 40;


    private Vector2 direction;

    [SerializeField]
    private RuntimeAnimatorController aController;


    [SerializeField]
    private GameObject landPrefab;
    [SerializeField]
    private GameObject jumpPrefab;

    protected bool isFacingRight;

    public Camera_Manager camera;

    private bool mustLand;
    private bool checkLand;

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
    [SerializeField]
    private float airControl = 0.2f;

    private bool isGrounded;
    private bool canJump;
    private bool isJumping = false;

    //Inputs
    float horizontal;
    bool jumpButton;

    [SerializeField]
    GameObject footstepFX;

    public void Start()
    {
        characterAnimator = characterSprite.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;

        //shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();

    }

    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void Update()
    {
        GetInputs();

        v = rb.velocity;

        Flip(horizontal);

        isGrounded = IsGrounded(rb);

        v = HandleMovementAcceleration(horizontal, v, isGrounded);

        if (v.y < -0.2 && !checkLand)
        {
            characterAnimator.SetBool("Land", true);
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

    public Vector2 HandleMovementAcceleration(float horizontal, Vector2 v, bool isGrounded)
    {
        if (horizontal != 0)
        {
            float control = 1;
            //Less control in the air
            if (!isGrounded)
            {
                control = airControl;
                AudioManager.instance.Stop("walk");
                footstepFX.SetActive(false);
            }
            else if (isGrounded)
            {
                AudioManager.instance.Play("walk");
                footstepFX.SetActive(true);
            }

            if (Mathf.Abs(v.x) < maxSpeed)
            {
                v.x += horizontal * movementSpeed * control;

            }
            else
            {
                v.x = horizontal * maxSpeed * control;
            }
        }
        else
        {
            v.x = 0;
            footstepFX.SetActive(false);
        }

        characterAnimator.SetFloat("MoveSpeed", Mathf.Abs(horizontal));

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
            characterAnimator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpValue, ForceMode2D.Impulse);

            AudioManager.instance.Play("jump");
        }
    }

    public void Bump(float jumpValue)
    {
        characterAnimator.SetBool("Land", false);
        canJump = false;
        isGrounded = false;
        isJumping = true;
        characterAnimator.SetTrigger("Jump");
        rb.AddForce(Vector2.up * jumpValue, ForceMode2D.Impulse);

        AudioManager.instance.Play("bumper");
    }

    private bool IsGrounded(Rigidbody2D MyRigidbody)
    {
        bool returnTrue=false;
        if (MyRigidbody.velocity.y <= 0.2)
        {
            foreach (Transform point in groundPoints)
            {
                //Gets the colliders on the player's feet
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, groundMask);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject && (colliders[i].gameObject.CompareTag("Floor")))
                    {
                        if (colliders[i].gameObject.GetComponent<PlatformSliding>() != null)
                        {
                            this.transform.SetParent(colliders[i].gameObject.transform);
                        }
                        //If the colliders collide with something else than the player, then the players is grounded
                        canJump = true;
                        isJumping = false;

                        characterAnimator.ResetTrigger("Jump");

                        if (characterAnimator.GetBool("Land")== true)
                        {   
                            mustLand = true;
                            
                        }
                        returnTrue = true;
                        
                    }
                }
            }
            if (mustLand == true && returnTrue)
            {
                checkLand = true;
                characterAnimator.SetBool("Land", false);
                //Camera_Manager.instance.ShakeCamera(Camera_Manager.ShakeCamType.LandShake);
                AudioManager.instance.Play("land");
                mustLand = false;
            }
            if (returnTrue) return true;
        }
        this.transform.SetParent(null);
        checkLand = false;
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            characterAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            characterAnimator.SetLayerWeight(1, 0);
        }
    }

    public void Die()
    {
        transform.position = camera.Current_Room.GetComponent<Room_Behaviour>().Respawn_Point.transform.position;
        //Camera_Manager.instance.ShakeCamera(Camera_Manager.ShakeCamType.DeathShake);

    }
}

