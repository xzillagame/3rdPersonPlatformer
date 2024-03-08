using Cinemachine;
using System;
using System.Diagnostics.SymbolStore;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    //Component References
    private Rigidbody playerRB;

    [SerializeField]
    private CinemachineVirtualCamera mainCam;

    [SerializeField] private Animator animController;


    [Header("Player Stats ScriptableObject")]
    [SerializeField]
    private PlayerScriptableObjectStats PlayerStats;

    [Header("Player Movement Variables")]
    
    //Player Stats
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpForceMultipler;
    [SerializeField] private float gravityValue;
    [SerializeField] private float rayDistToGround;
    [SerializeField] private float rayDistToWall;


    [Header("RayCast Interaction Layers")]
    [SerializeField] LayerMask floorDetection;
    [SerializeField] LayerMask wallDetection;
    [SerializeField] LayerMask wallReflectDetection;

    //Logic Variables
    private bool isOnGround;
    private bool isTouchingWall = false;
    private bool jumpPressed = false;
    private bool isWallJumping = false;
    private bool isDamaged = false;

    private bool fallingTriggerSent = false;

    private float wallJumpCooldownTimerMax = 0.5f;
    private float wallJumpCooldownCountdown = 0f;


    //Input
    private float forwardInputAxis;
    private float rightInputAxis;


    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    void Update()
    {


        isOnGround = Physics.Raycast(transform.position,Vector3.up * -1, rayDistToGround,floorDetection);

        #region Wall Jump Cooldown

        wallJumpCooldownCountdown = Mathf.Clamp(wallJumpCooldownCountdown + Time.deltaTime, 0f, wallJumpCooldownTimerMax);
        if (wallJumpCooldownCountdown >= wallJumpCooldownTimerMax)
        {
            isTouchingWall = Physics.Raycast(transform.position, transform.forward, rayDistToWall, wallDetection);
        }
        else
        {
            isTouchingWall = false;
        }

        #endregion

        forwardInputAxis = Input.GetAxis("Vertical");
        rightInputAxis = Input.GetAxis("Horizontal");

        if (forwardInputAxis != 0f || rightInputAxis != 0f)
        {
            animController.SetBool("isMoving", true);
        }
        else
        {
            animController.SetBool("isMoving", false);

        }



        if (isOnGround)
        {
            jumpPressed = false;
            isTouchingWall= false;
            isWallJumping = false;
            fallingTriggerSent = false;
            //animController.SetBool("isOnGround",true);
        }



        jumpPressed = Input.GetButton("Jump");

        if (isOnGround && Input.GetButton("Jump"))
        {
            animController.SetTrigger("isJumping");
        }





        Debug.DrawLine(transform.position,transform.position + transform.up * -rayDistToGround, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.forward * rayDistToWall, Color.red);

    }


    private void FixedUpdate()
    {


        //Custom Gravity
        playerRB.AddForce(0f, -gravityValue, 0f, ForceMode.Force);


        //Stop player except for falling until animation is finished
        if (isDamaged)
        {
            playerRB.velocity = new Vector3(0f, playerRB.velocity.y, 0f);
            return;
        }



        Ray myRay = new Ray(transform.position, transform.forward * rayDistToWall);


        if (isOnGround && jumpPressed)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (Physics.Raycast(myRay, out RaycastHit hit, rayDistToWall, wallDetection | wallReflectDetection) //| wallReflectDetection
            && jumpPressed)
        {

            Vector3 wallJumpVec = Vector3.zero;


            //Bit shift operator to comapre object collided with layer to proper wall collision layer
            if ( 1 << hit.collider.gameObject.layer == wallDetection)
            {
                wallJumpVec = hit.normal;
            }
            else if(1 << hit.collider.gameObject.layer == wallReflectDetection)
            {
                wallJumpVec = transform.forward - 2 * Vector3.Dot(transform.forward, hit.normal) * hit.normal;
            }

            playerRB.velocity = new Vector3(0f, 0f, 0f);
            playerRB.AddForce(wallJumpVec * 13 + new Vector3(0, jumpForce * 1.25f, 0), ForceMode.Impulse);

            transform.forward = wallJumpVec;


            animController.SetTrigger("isJumping");



            wallJumpCooldownCountdown = 0f;
            isWallJumping = true;
        }
        else if (!isWallJumping) //if !isWallJumping
        {
            #region Player Grounded Movement From Input

            #region Vertical Movement

            Vector3 forwardRealtiveToCamera = mainCam.transform.forward;
            forwardRealtiveToCamera.y = 0f;
            forwardRealtiveToCamera.Normalize();

            forwardRealtiveToCamera *= forwardInputAxis;

            #endregion

            #region Horizontal Movement

            Vector3 rightRelativeToCamera = mainCam.transform.right;
            rightRelativeToCamera.y = 0f;
            rightRelativeToCamera.Normalize();

            rightRelativeToCamera *= rightInputAxis;

            #endregion

            #region Final Movement Calculation
            Vector3 finalMovement = (forwardRealtiveToCamera + rightRelativeToCamera);

            finalMovement *= playerSpeed;
            finalMovement.y = playerRB.velocity.y;

            playerRB.velocity = finalMovement;
            #endregion

            #endregion
        }



        //Player vertical and horizontal oritention relative to camera
        if (!isWallJumping && !isTouchingWall) 
        { 

            Vector3 newFwd = forwardInputAxis * mainCam.transform.forward;
            Vector3 newRight = rightInputAxis * mainCam.transform.right;

            Vector3 camRelative = newFwd + newRight;
            camRelative.y = 0f;
            camRelative.Normalize();
            if (camRelative != Vector3.zero)
                transform.forward = camRelative;
        }




        //Cap y velocity from gravity
        Vector3 currVeloicty = playerRB.velocity;
        currVeloicty.y = Mathf.Clamp(currVeloicty.y, -gravityValue, Mathf.Infinity);
        playerRB.velocity = currVeloicty;


        //
        if (fallingTriggerSent == false)
        {
            animController.SetFloat("fallingValue", playerRB.velocity.y);
            fallingTriggerSent = true;
        }



    }

    //TakesDamage Function called from Damageable event
    public void PlayerTakesDamage(int damage)
    {
        animController.SetTrigger("isHurt");
        isDamaged = true;
        PlayerStats.DamagePlayer(damage);
    }

    //ResetIsDamaged called from unitychanHurt01 animation event
    public void ResetIsDamaged()
    {
        Debug.Log("In Reset");
        isDamaged = false;
    }


}
