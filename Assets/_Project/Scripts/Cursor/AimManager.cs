using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AimManager : MonoBehaviour
{
    public GameObject worldCursor;
    public LayerMask cursorLayerMask;
    public RectTransform aimCursor;
    public float cursorSpeed = 10;
    public static AimManager instance;



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Skill Manager object, destroying new one");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }






    void Update()
    {
        HandleCursor();
        GetWorldPositionFromCursor();
    }







    // CURSOR HANDLING -----------------------------------------------------
    public void GetWorldPositionFromCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimCursor.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cursorLayerMask))
        {
            worldCursor.transform.position = hit.point;
        }
    }


    public void HandleCursor()
    {
        Vector3 cursorInput = InputManager.RawInput;

        if (InputManager.instance.currentDevice == InputManager.ControllerType.KB)
        {
            // MOUSE INPUT
            

            // clamp the cursor to the screen bounds
            cursorInput.x = Mathf.Clamp(cursorInput.x, 0, Screen.width);
            cursorInput.y = Mathf.Clamp(cursorInput.y, 0, Screen.height);

            aimCursor.transform.position = cursorInput;
        }
        else
        {
            // GAMEPAD INPUT


            // make sure the cursor doesn't go off screen
            Vector3 cursorPos = aimCursor.position;

            cursorPos.x += cursorInput.x * cursorSpeed;
            cursorPos.y += cursorInput.y * cursorSpeed;

            // clamp the cursor to the screen bounds
            cursorPos.x = Mathf.Clamp(cursorPos.x, 0, Screen.width);
            cursorPos.y = Mathf.Clamp(cursorPos.y, 0, Screen.height);

            aimCursor.position = cursorPos;
        }
    }








    // Utility Functions -----------------------------------------------------
    public Vector3 GetWorldCursorPosition() => worldCursor.transform.position;

    bool IsMouseOverGameWindow
    {
        get
        {
            Vector3 mp = Input.mousePosition;
            return !( 0>mp.x || 0>mp.y || Screen.width<mp.x || Screen.height<mp.y );
        }
    }

}
