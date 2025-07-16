using System;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// declares the enum that are the two players in the game that will be accessing the script
/// </summary>
public enum PlayerType 
{ 
    Player1, 
    Player2 
}

public class Paddle : MonoBehaviour
{
    public PlayerType playerType;
    public float Speed = 10f;
    public float Boundary = 4f;

    [SerializeField] private InputAction inputAction;
    private float moveDirection;

    void Awake()
    {
        inputAction.performed += OnMovePerformed;
        inputAction.canceled += OnMoveCanceled;
    }

    void OnEnable()
    {
        inputAction.Enable();
    }
    
    void OnDisable()
    {
        inputAction.Disable();
    }

    /// <summary>
    /// Looking for input from a player pressing the correct move keys
    /// </summary>
    /// <param name="context"></param>
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<float> ();
    }

    /// <summary>
    /// Looking to see when the player stops any input
    /// </summary>
    /// <param name="context"></param>
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveDirection = 0;
    }

    void Update()
    {
        float movement = moveDirection * Speed * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -Boundary, Boundary);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
