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
    public bool TouchedControls;
    // public bool FiringBlaster;
    


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
        // UseBlaster();
    }

    public void WorldCursor(InputAction.CallbackContext context)
    {
        if (context.performed)
            RawInput = context.ReadValue<Vector2>();
    }


    public void Move(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        //     MoveVector = context.ReadValue<Vector2>().normalized;
        //     TouchedControls = true;
        // }
    }


    public void Dash(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        //     Debug.Log("Dash");
        //     GlobalDataStore.instance.playerController.Dash();
        // }
    }


    public void UseBlaster()
    {
        // if (playerInput.actions["Blaster"].IsPressed())
        // {
        //     FiringBlaster = true;
        // }
        // else if (playerInput.actions["Blaster"].WasReleasedThisFrame())
        // {
        //     FiringBlaster = false;
        // }
    }

    public void Skill1(InputAction.CallbackContext context)
    {

        // if (context.performed)
        // {
        //     Debug.Log("Skill 1");
        //     SkillManager.instance.UseSkill(1);
        // }
    }

    public void Skill2(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        //     Debug.Log("Skill 2");
        //     SkillManager.instance.UseSkill(2);
        // }
    }

    public void Skill3(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        //     Debug.Log("Skill 3");
        //     SkillManager.instance.UseSkill(3);
        // }
    }


    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GlobalDataStore.instance.kitUI.TurnOnKitUI();
        }
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
