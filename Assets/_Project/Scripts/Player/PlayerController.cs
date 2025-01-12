using UnityEngine;
// using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Rotation")]
    public float rotateSpeed = 10;
    private float rotateDifference;
    private float rotateDirection;
    public Animator animator;
    public float dampTime;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;
    

    [Header("Movement")]
    private Vector3 cameraRelativeMovement = new Vector3(0, 0, 0);
    public float MovementForce = 10;
    private Vector2 _input = new Vector2(0, 0);
    // public CinemachineVirtual vcam;
    private Vector3 _currentMovement = new Vector3(0, 0, 0);
    





    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        HandleAnimations();
        RotateTowards(AimManager.instance.GetWorldCursorPosition());
    }




    void FixedUpdate()
    {
        Move();
    }




    // MOVEMENT HANDLING -----------------------------------------------------
    public void Move()
    {
        HandleCameraRelativeMovement();
        HandleMovement();
        rb.AddForce(MovementForce * Time.deltaTime * cameraRelativeMovement);
    }


    void HandleCameraRelativeMovement()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;


        Vector3 forwardRelativeInput = _input.y * forward;
        Vector3 rightRelativeInput = _input.x * right;

        Debug.Log("Forward: " + forwardRelativeInput);
        Debug.Log("Right: " + rightRelativeInput);


        cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;
        cameraRelativeMovement.y = _currentMovement.y;
    }


    void HandleMovement()
    {
        // Get Joystick Input
        _input = InputManager.instance.MoveVector;

        _currentMovement.x = _input.x;
        _currentMovement.z = _input.y;
    }









    // ROTATION HANDLING -----------------------------------------------------
    private void RotateTowards(Vector3 targetDirection)
    {
        // if (targetDirection == Vector3.zero)
        //     return;

        // // Calculate the target rotation
        // Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // // Smoothly interpolate between current and target rotation
        // Quaternion smoothedRotation = Quaternion.Slerp(
        //     transform.rotation,             // Current rotation
        //     targetRotation,          // Target rotation
        //     rotateSpeed * Time.deltaTime // Interpolation factor
        // );


        // // Apply the smooth rotation to the Rigidbody
        // transform.rotation = smoothedRotation; 

        Quaternion _lookRotation = 
		    Quaternion.LookRotation((targetDirection - transform.position).normalized);

        
        rotateDifference = Quaternion.Angle(gameObject.transform.rotation, _lookRotation);

        // find out if its rotating left or right based on the sign
        rotateDirection = Vector3.Dot(targetDirection, transform.right);

        //over time
        transform.rotation = 
            Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);
        
        //instant
        // transform.rotation = _lookRotation;
    }


    void HandleRotation()
    {
        if (_input.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(cameraRelativeMovement.x, cameraRelativeMovement.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }












    // ANIMATION HANDLING ---------------------------------------------------
    public void HandleAnimations()
    {
        if (animator == null)
            return;

        float finalRollAmount = rotateDifference;

        if (rotateDirection > 0)
        {
            finalRollAmount *= -1;
        }

        // Set the speed parameter in the animator
        animator.SetFloat("RollAmount", ScaleValue(finalRollAmount), dampTime, Time.deltaTime);
    }

    public float ScaleValue(float value)
    {
        float min = -45f;
        float max = 45f;

        // Ensure the value is clamped within the original range
        value = Mathf.Clamp(value, min, max);

        // Scale the value to the range -1 to 1
        return value / max; // Equivalent to (value - min) / (max - min) * 2 - 1
    }
}
