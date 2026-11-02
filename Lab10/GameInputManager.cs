using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    [Header("Manager References")]
    public ScoreManager scoreManager;
    public LocationSaveManager locationSaveManager;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Save.performed += OnSavePerformed;
        inputActions.Player.Load.performed += OnLoadPerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Save.performed -= OnSavePerformed;
        inputActions.Player.Load.performed -= OnLoadPerformed;

        inputActions.Player.Disable();
    }

    private void OnSavePerformed(InputAction.CallbackContext context)
    {
        if (scoreManager != null)
        {
            scoreManager.SaveScore();
        }
        if (locationSaveManager != null)
        {
            locationSaveManager.SaveLocations();
            Debug.Log("Game Saved Successfully");
        }
    }

    private void OnLoadPerformed(InputAction.CallbackContext context)
    {
        if (scoreManager != null)
        {
            scoreManager.LoadScore();
        }

        if (locationSaveManager != null)
        {
            locationSaveManager.LoadLocations();
            Debug.Log("Game Loaded Successfully");
        }
    }
}