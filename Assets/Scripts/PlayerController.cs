
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput; 
    private TouchingDirections touchingDirections;
    
    public float jumpImpulse = 10f;
    
    public bool IsFacingRight { get { return _isFacingRight; } private set {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        } }
    
     Rigidbody2D rb;
     Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

 
// x and y movement input
    public void OnMove(InputAction.CallbackContext context)
    { 
        moveInput = context.ReadValue<Vector2>();
        
        IsMoving = moveInput != Vector2.zero;
        
        SetFacingDirection(moveInput); 
    }

    private void SetFacingDirection(Vector2 moveInput)
        {
            if(moveInput.x < 0){
                transform.localScale = new Vector2(-0.73f, 0.67564f);
            }else if(moveInput.x > 0){
                transform.localScale = new Vector2(0.73f, 0.67564f);
            }
        }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    } 
    
    
    [SerializeField]
    private bool _isMoving = false;
       public bool IsMoving { get
            {
                return _isMoving;
            }
            private set
            {
                _isMoving = value;
                animator.SetBool(AnimationStrings.isMoving, value);
            }
        }
        public bool _isFacingRight = true;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("AirFilter"))
            {
                Debug.Log("Collected air filter!");

                GameManager.Instance.AddOxygen(5f);
                GameManager.Instance.AddScore(1);

                other.tag = "Untagged"; // Preventing double scoring
                Destroy(other.gameObject);
            }
        }
}
