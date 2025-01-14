using System.Collections;
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


    [Header("Animation")]
    public Animator animator;
    public float dampTime;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;
    

    [Header("Movement")]
    private Vector3 cameraRelativeMovement = new Vector3(0, 0, 0);
    public float MovementForce = 10;
    private Vector2 _input = new Vector2(0, 0);
    private Vector3 _currentMovement = new Vector3(0, 0, 0);


    [Header("Dash")]
    public float dashForce = 10;
    public bool isDashing = false;
    public float dashCooldown = 1f;
    public float dashDuration = 0.5f;
    





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

        // split the movement into two parts - the forward movement and the right movement
        // this is so we can add more force to the y axis

        // X movement
        Vector3 xMovement = new Vector3(cameraRelativeMovement.x, 0, 0);
        rb.AddForce(MovementForce * Time.deltaTime * xMovement);

        // Z movement
        Vector3 zMovement = new Vector3(0, 0, cameraRelativeMovement.z);
        rb.AddForce(MovementForce * Time.deltaTime * zMovement);
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






    // DASH HANDLING -----------------------------------------------------
    public void Dash()
    {
        // we don't want to dash if we're not moving in a direction
        if (cameraRelativeMovement.magnitude == 0) return;

        // we don't want to dash if we're already dashing
        if (isDashing) return;

        StartCoroutine(PerformDash());
    }

    public IEnumerator PerformDash()
    {
        // show in the UI that the skill was used
        GlobalDataStore.instance.dashUI.UseSkill();

        isDashing = true;

        // stop all momentum
        rb.velocity = Vector3.zero;

        // screen shake
        ScreenshakeManager.instance.ShakeCamera(ScreenshakeManager.instance.DashProfile);

        // apply the dash force
        rb.velocity = cameraRelativeMovement * dashForce;

        // wait for the dash duration - decrease for a shorter dash
        yield return new WaitForSeconds(dashDuration);

        // stop the dash
        rb.velocity = Vector3.zero;

        // do cooldown stuff here for UI
        GlobalDataStore.instance.dashUI.BeginCooldown(
            GlobalDataStore.instance.statModule.GetDashCooldownTime()
        );
    }




    public void CreateGhostMesh()
    {
        // create a ghost mesh by instantiating a sphere

        // set the position to the player's position

        Debug.Log("Ghost");
    }




    // ROTATION HANDLING -----------------------------------------------------
    private void RotateTowards(Vector3 targetDirection)
    {
        Quaternion _lookRotation = 
		    Quaternion.LookRotation((targetDirection - transform.position).normalized);

        
        rotateDifference = Quaternion.Angle(gameObject.transform.rotation, _lookRotation);

        // find out if its rotating left or right based on the sign
        rotateDirection = Vector3.Dot(targetDirection, transform.right);

        // Debug.Log("Rotation: " + rotateDirection);
        // Debug.Log("Ship Rotation: " + transform.rotation);
        // Debug.Log("Look Rotation: " + _lookRotation);  

        //over time
        transform.rotation = 
            Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);
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
