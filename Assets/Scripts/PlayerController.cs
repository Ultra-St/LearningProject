using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //References
    private InputManager inputManager;
    private Rigidbody2D _rb;

    //Player Horizontal Movement Variables
    [SerializeField] private float _maxWalkSpeed = 12f;
    [SerializeField] private float _groundAcceleration = 7f;
    [SerializeField] private float _groundDecceleration = 10f;

    private Vector2 _moveVelocity;

    //Jump Variables
    [SerializeField] private float _maxJumpHeight = 4f;
    [SerializeField] private float _maxJumpDistance = 2f;

    private bool _jumpWasPressed;
    private float verticalVelocity;

    public float Gravity { get; set; }
    public float InitialJumpVelocity { get; set; }

    //Ground Variables

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _raylength;
    [SerializeField] private bool _isGrounded;

    private void OnValidate()
    {
        CalculateValues();
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        IsGrounded();
        JumpCheck();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move(inputManager.GetMoveInput());
        Jump();
    }

    private void Move(Vector2 inputVector)
    {
        Vector2 targetVelocity = Vector2.zero;

        if (inputVector != Vector2.zero)
        {
            targetVelocity = new Vector2(inputVector.x, 0f) * _maxWalkSpeed;
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, _groundAcceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
        }
        else
        {
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, _groundDecceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
        }
    }
    private void JumpCheck()
    {
        if (inputManager.JumpWasPressed() && _isGrounded)
        {
            _jumpWasPressed = true;
        }
    }

    private void Jump()
    {
        if (_jumpWasPressed)
        {
            verticalVelocity = InitialJumpVelocity;
            _jumpWasPressed = false;
        }
        else
        {
            if (_isGrounded)
            {
                verticalVelocity = 0f;
            }
            else
            {
                verticalVelocity += Gravity * Time.fixedDeltaTime;
            }
        }
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, verticalVelocity);
    }

    private void IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, _raylength, _groundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
    
    private void CalculateValues()
    {
        InitialJumpVelocity = (2 * _maxJumpHeight * _maxWalkSpeed) / _maxJumpDistance;
        Gravity = -(2 * _maxJumpHeight * Mathf.Pow(_maxWalkSpeed, 2) / Mathf.Pow(_maxJumpDistance, 2));
    }
    
}
