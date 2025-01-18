using UnityEngine;
using System.Collections;

public class EnvironmentSystem : MonoBehaviour 
{
    public static EnvironmentSystem instance;
    public bool isEnvironmentReady = false;


    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetEnvironment()
    {
        // Reset all environment objects
        isEnvironmentReady = false;
    }

    public IEnumerator SetupEnvironment()
    {
        Debug.Log("Setting up environment...");

        yield return new WaitForSeconds(1f);

        Debug.Log("Environment setup complete.");

        PlacePlayer();

        // Setup all environment objects
        isEnvironmentReady = true;
    }

    public void PlacePlayer()
    {
        // Place player in the environment
        Debug.Log("Placing player in the environment...");
    }
}