using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class P_Controller : MonoBehaviour
{
    public static P_Controller instance;
    CharacterController Player;
    public GameObject playerBase;
    private UI_Controller uiController;

    public Animator anim;
    public GameObject gameOver;

    public int sceneIndex;

    [Range(70.0f, 120.0f)] public float FOV = 70.0f;


    [Header("Stats")]
    public float maxHealth;
    public float maxStamina;
    public float maxCrouchSpeed;
    public float maxWalkSpeed;
    public float maxRunSpeed;
    public float maxJumpSpeed;
    public float maxStaminaRegenSpeed;
    public float maxStaminaDepleteSpeed;
    public float maxFallSpeed;
    public float currentHealth;
    public float Health;
    public float Stamina;

    public int Coins;

    public Light flashlight;


    [Header("Private Stats")]
    
    private float crouchSpeed;
    private float walkSpeed;
    private float runSpeed;
    private float jumpSpeed;
    private float StaminaRegenSpeed;
    private float StaminaDepleteSpeed;
    private float StaminaRegenValue;
    private float FallSpeed;
    private float playerHeight;
    private float verticalSpeed;
    private float lastY;

    [Header("Player Levels")] 
    public int maxHealthLevel;
    public int maxStaminaLevel;
    public int maxStaminaRegenLevel;
    public int maxStaminaDepleteLevel;

    [Header("World Settings")]
    private float gravity = 20.0f;
    private float FallingThreshold = -0.00001f;

    [Header("Camera")]
    private float lookSpeed = 2.0f;
    private float lookLimit = 90.0f;
    private float rotationX;

    [Header("Movement")] private float moveX;
    private float moveZ;

    [Header("Actions")] public bool canMove;
    public bool canStandUp;
    public bool isCrouching;
    public bool isMoving;
    public bool isRunning;
    public static bool isJumping;
    public bool isFalling;
    public bool isGrounded;
    public bool isDepleted;
    public bool isRestoring;
    public bool isStanding;
    public bool isDead;


    [Header("Objects")] public Camera playerCamera;


    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Application.targetFrameRate = 1024;
        uiController = FindObjectOfType<UI_Controller>();

        gameOver.SetActive(false);

        Player = GetComponent<CharacterController>();

        lastY = transform.position.y;
        playerHeight = Player.height;

        Health = maxHealth;
        Stamina = maxStamina;
        crouchSpeed = maxCrouchSpeed;
        walkSpeed = maxWalkSpeed;
        runSpeed = maxRunSpeed;
        jumpSpeed = maxJumpSpeed;
        StaminaRegenSpeed = maxStaminaRegenSpeed;
        StaminaDepleteSpeed = maxStaminaDepleteSpeed;


        StaminaRegenValue = (maxStamina / 4.0f);

        canMove = true;
        canStandUp = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (UI_PauseMenu.gameIsPaused || P_UpgradeShop.isUpgradeMenuOpen || dialogues.isDialogue)
        {
            canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
//        print(Coins);
        // Debug.LogFormat("Stamina: <color=#ff0000ff>{0}</color>" + Coins, Stamina);

        playerCamera.fieldOfView = FOV;

        if (canMove)
        {
            Crouch();
            Movement();
            StaminaFunc();
            Camera();
            IsFalling();
            CheckObjectAbove();
            ApplyUI();
            Flashlight();
        }
        Death();
        currentHealth = Health;
    }

    private void Movement()
    {
        isGrounded = Player.isGrounded;

        if (!isDepleted && isMoving && canStandUp) isRunning = Input.GetButton("Run");
        else isRunning = false;


        moveX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        moveZ = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");


        if (moveX > 0.0001f || moveX < -0.0001f || moveZ > 0.0001f || moveZ < -0.0001f) isMoving = true;
        else isMoving = false;

        verticalSpeed -= gravity * Time.deltaTime;
        if (verticalSpeed < maxFallSpeed) verticalSpeed = maxFallSpeed;
        if (isGrounded && transform.position.y < lastY) verticalSpeed = 0;

        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping && canStandUp && !isFalling &&
            Stamina >= ((Stamina / Stamina) * 5))
        {
            isCrouching = false;
            Stamina -= ((Stamina / Stamina) * 5);
            verticalSpeed = jumpSpeed;
            isJumping = true;
        }
        else if (isFalling) isJumping = false;
        else if (isGrounded) isJumping = false;
        
        


        if (isMoving)
        {
            //FindObjectOfType<AudioManager_Master>().PlayAudio("player_footstep1");

        }

        

        Player.Move((moveX * transform.forward + moveZ * transform.right + transform.up * verticalSpeed) *
                    Time.deltaTime);
    }

    private void Camera()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void IsFalling()
    {
        float Distance = (transform.position.y - lastY) * Time.deltaTime;
        lastY = transform.position.y;
        isFalling = Distance < FallingThreshold;
    }

    private void StaminaFunc()
    {
        if (isRunning && !isDepleted)
        {
            Stamina -= 1 * (Time.deltaTime * StaminaDepleteSpeed);
            if (Stamina <= 0) isDepleted = true;
        }

        if (isDepleted)
        {
            if (Stamina <= StaminaRegenValue) Stamina += 1 * (Time.deltaTime * StaminaRegenSpeed);
            else if (Stamina >= StaminaRegenValue) isDepleted = false;
        }

        if (!isRunning) Stamina += 1 * (Time.deltaTime * StaminaRegenSpeed);
        if (Stamina >= maxStamina) Stamina = maxStamina;
    }

    private void Crouch()
    {
        if (Input.GetButtonDown("Crouch") && !isFalling && !isJumping && !isRunning && canStandUp)
        {
            isCrouching = !isCrouching;
        }

        if (isCrouching)
        {
            Player.height = (playerHeight / 2);
            walkSpeed = maxCrouchSpeed;
            isStanding = false;
        }
        else
        {
            Player.height = playerHeight;
            walkSpeed = maxWalkSpeed;
            isStanding = true;
        }

        if (isRunning) isCrouching = false;
    }

    private void CheckObjectAbove()
    {
        RaycastHit hit;

        if (isCrouching)
        {
            if (Physics.Raycast(playerCamera.transform.position, Vector3.up, out hit, 1.0f))
            {
                // print("Found an object - name: " + hit.transform.name);
                //Debug.DrawRay(playerCamera.transform.position, Vector3.up * 1.0f, Color.red);
                canStandUp = false;
            }
            else canStandUp = true;
        }
        else canStandUp = true;
    }

    private void Flashlight()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            if (flashlight.enabled) flashlight.enabled = false;
            else flashlight.enabled = true;
        }
    }
    
    private void ApplyUI()
    {
        // playerUI.SetHealth(Health, maxHealth);
        // playerUI.SetStamina(Stamina, maxStamina);
        uiController.SetHealth(Health, maxHealth);
        uiController.SetStamina(Stamina, maxStamina);
        uiController.SetCoins(Coins);
        //  playerUI.SetCoins(Coins);
    }

    public void TakeHealth(float health)
    {
        Health -= health;
        anim.SetTrigger("GetHit");
    }

    public void TakeCoins(int coins)
    {
        Coins -= coins;
    }

    public void setMaxHealth(float maxHealthValue)
    {
        maxHealth = maxHealthValue;
        Health = maxHealth;
        //print(Health + " / " + maxHealth);
    }

    public void setMaxStamina(float maxStaminaValue)
    {
        maxStamina = maxStaminaValue;
        Stamina = maxStamina;
        //print(Stamina + " / " + maxStamina);
    }

    public void setStaminaRegen(float maxStaminaRegen)
    {
        maxStaminaRegenSpeed = maxStaminaRegen;
        //print(maxStaminaRegenSpeed);
    }

    public void setCursor(bool active)
    {
        if(active == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Death()
    {
        if (Health < 0)
        {
            canMove = false;
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}