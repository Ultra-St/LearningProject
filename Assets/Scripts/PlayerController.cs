using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    //References
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerControllerValuesSO _playerControllerValues;
    
    private Rigidbody2D _rb;

    private Vector2 _moveVelocity;

    private bool _jumpWasPressed;
    private bool _jumpwasReleased;

    private float verticalVelocity;

    //Ground Variables

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _raylength;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private BoxCollider2D _boxCollider;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsGrounded();
        JumpCheck();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isGrounded)
        {
            Move(_inputManager.GetMoveInput(), _playerControllerValues.groundAcceleration, _playerControllerValues.groundDecceleration);
        }
        else
        {
            Move(_inputManager.GetMoveInput(), _playerControllerValues.airAcceleration, _playerControllerValues.airDeceleration);
        }
        Jump();
    }

    private void Move(Vector2 inputVector, float acceleration, float deceleration)
    {
        Vector2 targetVelocity = Vector2.zero;

        if (inputVector != Vector2.zero)
        {
            targetVelocity = new Vector2(inputVector.x, 0f) * _playerControllerValues.maxWalkSpeed;
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
        }
        else
        {
            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, deceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);
        }
    }
    private void JumpCheck()
    {
        if (_inputManager.JumpWasPressed() && _isGrounded)
        {
            _jumpWasPressed = true;
        }
        if (_inputManager.JumpWasReleased())
        {
            _jumpwasReleased = true;
        }
    }

    private void Jump()
    {
        if (_jumpWasPressed)
        {
            verticalVelocity = _playerControllerValues.InitialJumpVelocity;
            _jumpWasPressed = false;
        }
        else
        {
            if (_isGrounded)
            {
                verticalVelocity = -0.1f;
            }
            else
            {
                if (_jumpwasReleased && verticalVelocity > 0f)
                {
                    verticalVelocity = verticalVelocity / 2;
                }
                if(verticalVelocity <= 0f)
                {
                    verticalVelocity += (_playerControllerValues.Gravity * _playerControllerValues.fallGravityMultiplaier) * Time.fixedDeltaTime;
                }
                else
                {
                    verticalVelocity += _playerControllerValues.Gravity * Time.fixedDeltaTime;
                }
            }
        }
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, verticalVelocity);
        _jumpwasReleased = false;
    }
    private void IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, _boxCollider.size , 0f, Vector2.down, _raylength, _groundLayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
    
}
