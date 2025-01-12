using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInput playerInput;
    [HideInInspector] public static Vector2 RawInput;
    [HideInInspector] public Vector2 MoveVector;
    public enum ControllerType { XBOX, PSN, KB};
    public ControllerType currentDevice;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Input Manager object, destroying new one");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    
    void Start()
    {
        
    }

    
    void Update()
    {
        DetectDeviceType();
    }

    public void WorldCursor(InputAction.CallbackContext context)
    {
        RawInput = context.ReadValue<Vector2>();
    }


    public void Move(InputAction.CallbackContext context)
    {
        MoveVector = context.ReadValue<Vector2>();
    }








    // DEVICE DETECTION -----------------------------------------------------
    public void DetectDeviceType()
    {
        var deviceConnected = playerInput.devices[0];

        if (deviceConnected.displayName.Contains("Keyboard"))
        {
            currentDevice = ControllerType.KB;
        }
        else if (deviceConnected.description.manufacturer == "Sony Interactive Entertainment")
        {
            currentDevice = ControllerType.PSN;
        }
        else
        {
            currentDevice = ControllerType.XBOX;
        }
    }
}
