using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float m;

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
        m = context.ReadValue<float> ();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        m = 0;
    }

    void Update()
    {
        float movement = m * Speed * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -Boundary, Boundary);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
