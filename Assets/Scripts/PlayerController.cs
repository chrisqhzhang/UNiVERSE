using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveDirection;
    private Vector3 _movement;

    [SerializeField] private InputActionReference moveActionToUse;
    
    private float _currentHealth;
    private float _currentSpeed;
    private int _currentCurrency;
    
    [SerializeField] private PlayerStatsSOBase _playerStats;
    // [SerializeField] private CurrencyManager _currencyManager;
    
    private CharacterController controller;
    private Animator anim;
    private Rigidbody rb;
    
    private int isFloatingHash;
    private int isWalkingHash;
    private int isFlippingHash;
    private int isJumpingHash;
    private int isCollidingHash;
    
    // [SerializeField] public float jumpHeight = 0.5f;
    // private float _gravity = -0.00981f;
    
    [Header("Player Stats")]
    [Tooltip("How much the throttle ramps up or down.")]
    [SerializeField] public float speedIncrement = 0.1f;
    [Tooltip("Maximum engine thrust when at 100% throttle.")]
    [SerializeField] public float responsiveness = 1.0f;
    
    private float roll;
    private float pitch;
    private float yaw;
    private bool walk;
    private bool flip;
    private bool jump;
    
    private float resoponseModifier
    {
        get { return (rb.mass / 10.0f) * responsiveness; }
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        _currentHealth = _playerStats.MaxHealth;
        _currentSpeed = _playerStats.MaxSpeed;
        
        print($"Current health: {_currentHealth}, Current Speed: {_currentSpeed}");
        
        isWalkingHash = Animator.StringToHash("isWalking");
        isFloatingHash = Animator.StringToHash("isFloating");
        isFlippingHash = Animator.StringToHash("isFlipping");
        isJumpingHash = Animator.StringToHash("isJumping");
        isCollidingHash = Animator.StringToHash("isColliding");
    }
    
    private void Update()
    {
        // InputHandler();
        
        _moveDirection = moveActionToUse.action.ReadValue<Vector2>();
        print(_moveDirection);
        
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/
    }
    
    private void FixedUpdate()
    {
        MobileInputHandler();
        
        // ForceHandler();
        
        AnimationHandler();
    }
    
    private void InputHandler()
    {
        
        // Set rotational values from our axis inputs
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");
        jump = Input.GetButton("Jump");
        
        // Handle throttle value being sure to clamp it between min and max
        if (Input.GetKey(KeyCode.UpArrow)) _currentSpeed += speedIncrement;
        else if (Input.GetKey(KeyCode.DownArrow)) _currentSpeed -= speedIncrement;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _playerStats.MaxSpeed);
        
        // if (jump) _velocity.y += Mathf.Sqrt(jumpHeight * -1.0f * _gravity);
        // _velocity.y += _gravity * Time.deltaTime;
        // controller.Move(_velocity * Time.deltaTime);

        
    }

    private void MobileInputHandler()
    {
        rb.AddForce(transform.forward * _playerStats.MaxSpeed, ForceMode.Acceleration);
        rb.AddTorque(-transform.right * _moveDirection.y * _currentSpeed);
        rb.AddTorque(transform.up * _moveDirection.x * _currentSpeed);
    }
    
    private void ForceHandler()
    {
        // Apply forces to the player
        rb.AddForce(transform.forward * _playerStats.MaxSpeed, ForceMode.Acceleration);
        rb.AddTorque(transform.forward * roll * resoponseModifier);
        rb.AddTorque(transform.right * pitch * resoponseModifier);
        rb.AddTorque(-transform.up * yaw * resoponseModifier);
    }

    private void AnimationHandler()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        
        bool isGrounded = controller.isGrounded;
        bool isWalking = anim.GetBool(isWalkingHash);
        bool isFloating = anim.GetBool(isFloatingHash);
        bool isFlipping = anim.GetBool(isFlippingHash);
        bool isJumping = anim.GetBool(isJumpingHash);
        bool isColliding = anim.GetBool(isCollidingHash);
        
        walk = Input.GetButton("Walk");
        flip = Input.GetButton("Flip");
        jump = Input.GetButton("Jump");
        
        if (!isWalking && walk) anim.SetBool(isWalkingHash, true);
        if (isWalking && !walk) anim.SetBool(isWalkingHash, false);
        
        if (!isFloating && !isGrounded) anim.SetBool(isFloatingHash, true);
        
        if (!isFlipping && flip) anim.SetBool(isFlippingHash, true);
        if (isFlipping && !flip) anim.SetBool(isFlippingHash, false);
        
        if (!isJumping && jump) anim.SetBool(isJumpingHash, true);
        if (isJumping && !jump) anim.SetBool(isJumpingHash, false);

        if (isColliding && stateInfo.IsName("Surprise") && stateInfo.normalizedTime >= 1.0f)
        {
            anim.SetBool(isCollidingHash, false);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.contacts[0];
        Vector3 position = contact.point;
        
        if (other.collider.CompareTag("Collectable"))
        {
            anim.SetBool(isFlippingHash, true);

            FindObjectOfType<AudioManager>().Play("Collect");
        }
        
        if (other.collider.CompareTag("Destructible"))
        {
            anim.SetBool(isCollidingHash, true);
            
            FindObjectOfType<AudioManager>().Play("Destruct");
        }
        
        if (other.collider.CompareTag("Mobile"))
        {
            other.rigidbody.AddForceAtPosition(transform.forward * 0.5f, position, ForceMode.Impulse);
            other.rigidbody.AddTorque(transform.right * 0.1f, ForceMode.Impulse);
        }
        
        
    }
}