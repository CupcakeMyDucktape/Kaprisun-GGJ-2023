using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputMap input;
    private InputAction Reset;
    void Awake()
    {
        input = new InputMap();
        input.Enable();
        Reset = input.Map.Reset;
        Reset.Enable();
        Reset.performed += OnReset;
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
