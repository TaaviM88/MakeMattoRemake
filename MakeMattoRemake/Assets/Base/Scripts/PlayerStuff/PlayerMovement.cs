using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float inputDirection; // x value of our movevector
    private float verticalVelocity; // Y value of our move vector
    public int playerID = 0;
    public float jumpForce = 10.0f;
    public float speed = 5.0f;
    public float gravity = 1.0f;
    public float hoverHeight = 0.2f;
    private bool secondJumpAvail = false;
    private bool _facingRight = true;
    public float value;

    public Vector3 initialPos;

    bool isRunning;
    bool CanPlayerMove = true;


    //AnimeController _animeScript;
    private Animator anime;
    private Rigidbody _rb;
    private Vector3 moveVector;
    //lastmotionilla lukittiin hypyn suunta
    private Vector3 lastMotion;
    private CharacterController controller;

    private Quaternion lookLeft;
    private Quaternion lookRight;

    private string attackButtonAxisName = "";
    private string jumpButtonAxisName = "";
    /*Animaatio STATET---------------------------------------------
	 * State 0 = Ase stance(idle)
	 * State 1 = Run with hands
	 * State 2 = Jump Up
	 * State 3 = Falling
	 * State 4 = attackk
	 * State 5 = walljump
	 *--------------------------------------------------------------
	*/
    // Use this for initialization

    void Awake()
    {
        //DisablePlayerMovement();
    }

    void Start()
    {
        initialPos = transform.position;
       
        controller = GetComponent<CharacterController>();
        //anime = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        //Save direction where character is looking
        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);

        //Flip player 2
        if(playerID == 2)
        {
            transform.rotation = lookLeft;
            _facingRight = false;
        }

        IsControllerGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z != initialPos.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z);  
        }

        if (CanPlayerMove == true)
        {

            //LIIKKUMINEN JA FLIP, Run ja Idle animaatio ----------------------------------------------------------------
            IsControllerGrounded();
            moveVector = Vector3.zero;
            switch(playerID)
            {
                #region Player 1
                case 1:
                    inputDirection = Input.GetAxis("Horizontal") * speed;
                    value = Input.GetAxis("Horizontal");
                    attackButtonAxisName = "Fire1";
                    jumpButtonAxisName = "Jump";
                    break;
                #endregion
                #region Player 2
                case 2:
                    //Do the player 2  controllers
                    inputDirection = Input.GetAxis("Horizontal2") * speed;
                    value = Input.GetAxis("Horizontal2");
                    attackButtonAxisName = "Fire2";
                    jumpButtonAxisName = "Fire3";
                    break;
                #endregion
                default:
                    inputDirection = Input.GetAxis("Horizontal") * speed;
                    value = Input.GetAxis("Horizontal");
                    attackButtonAxisName = "Fire1";
                    jumpButtonAxisName = "Jump";
                    Debug.Log($"PlayerID = {playerID} does not match any switch case so set to player 1 controls");
                    break;
            }

           /* inputDirection = Input.GetAxis("Horizontal") * speed;
            value = Input.GetAxis("Horizontal");*/
            //Debug.Log(value);
            if (value > 0)
            {

                if (_facingRight == false)
                {
                    Flip();
                }
                if (IsControllerGrounded())
                {
                    isRunning = true;
                   // anime.SetInteger("State", 1);
                }
            }

            if (value < 0)
            {
                if (_facingRight == true)
                {
                    Flip();
                }
                if (IsControllerGrounded())
                {
                    isRunning = true;
                    //anime.SetInteger("State", 1);
                }
            }
            if (value == 0 && verticalVelocity == 0)
            {
                isRunning = false;
                //anime.SetInteger("State", 0);
            }

            //----------------------------------------------------------------------------

            //HYÖKKÄYS ANIMAATIO (hyökkäys komento itsessään on playeruseskill.cs)

            if (Input.GetButtonDown(attackButtonAxisName))
            {
                //anime.SetInteger("State", 4);
                //AudioManager.audioManager.WandSwing();
            }

            //----------------------------------------------------------------------------
            //HYPPY, TUPLAHYPPY ja molempien animaatiot
            if (IsControllerGrounded())
            {
                verticalVelocity = 0;

                if (Input.GetButtonDown(jumpButtonAxisName))
                {
                    //anime.SetInteger("State", 2);
                    verticalVelocity = jumpForce;
                    //Kun ilmassa secondjump on aktiivinen
                    secondJumpAvail = true;
                }
                moveVector.x = inputDirection;
            }
            else
            {

                if (Input.GetButtonDown(jumpButtonAxisName))
                {
                    if (secondJumpAvail)
                    {
                        //anime.SetInteger("State", 2);
                        verticalVelocity = jumpForce;
                        secondJumpAvail = false;
                    }
                }

                verticalVelocity -= gravity * Time.deltaTime;
                //Jos haluat vapaan liikkumisen ja vapaan hyppy suunnan, ota kaksi seuraavaa käyttöön
                moveVector.x = inputDirection;
                moveVector.y = inputDirection;
            }

            moveVector.y = verticalVelocity;
            //  moveVector = new Vector3(inputDirection, verticalVelocity, 0);
            controller.Move(moveVector * Time.deltaTime);
            lastMotion = moveVector;


        }

    }
    
    void FixedUpdate()
    {
        //tässä FALLING animaation laukaisu rigidbodylla

        //Debug.Log(moveVector.y);
        if (moveVector.y != 0f /*&& Input.GetButtonUp("P1Jump")*/)
        {
            // Debug.Log("hyppasin, menen up ja hyppy ei pohjassa");
            //anime.SetInteger("State", 3);

        }
    }
    //-------------------------------------------------------------------------------------

    // GROUNDED CHECK RAYCASTILLA
    // Raycast grounded check https://www.youtube.com/watch?v=8Cado6CzZUA&list=PLLH3mUGkfFCWwekOW1OMxyyIgc-Qm1OhI&index=10
    private bool IsControllerGrounded()
    {

        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;
        rightRayStart.x += controller.bounds.extents.x;

        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.blue);

        if (Physics.Raycast(rightRayStart, Vector3.down, (controller.height / 2) + hoverHeight))
        {
            //Debug.Log("osuu varpaat");
            return true;
        }

        if (Physics.Raycast(leftRayStart, Vector3.down, (controller.height / 2) + hoverHeight))
        {
            //Debug.Log("osuu kantapaa");
            return true;
        }

        //Debug.Log("ei osu maahan");
        return false;
    }
    //------------------------------------------------------------------------------------

    //TÄSSÄ WALLJUMP JA SEN ANIMAATIO
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (CanPlayerMove == true)
        {
            if (controller.collisionFlags == CollisionFlags.Sides)
            {
                if (Input.GetButtonDown(jumpButtonAxisName))
                {
                    //anime.SetInteger("State", 5);
                    moveVector = hit.normal * speed;
                    verticalVelocity = jumpForce;
                }

            }
        }
    }
    //--------------------------------------------------------------------------------------

    //TÄSSÄ MÄÄRITELTIIN FLIPPI
    private void Flip()
    {
        //_facingRight = !_facingRight;
        if(transform.rotation != lookRight)
        {
            transform.rotation = lookRight;
            _facingRight = true;
        }
        else
        {
            transform.rotation = lookLeft;
            _facingRight = false;
        }
        /*Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;*/
    }

    public void EnablePlayerMovement()
    {
        CanPlayerMove = true;
        //pelaaja voi liikkua

    }
    public void DisablePlayerMovement()
    {
        CanPlayerMove = false;
        //pelaaja ei voi liikkua

    }
}
