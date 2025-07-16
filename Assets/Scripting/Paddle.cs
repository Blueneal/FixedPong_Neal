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
    private float move;

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

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        move = context.ReadValue<float> ();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        move = 0;
    }

    void Update()
    {
        float movement = move * Speed * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -Boundary, Boundary);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
