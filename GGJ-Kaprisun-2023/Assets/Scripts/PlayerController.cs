using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputMap input;
    public Rigidbody2D rb;

    public float initialMaxSpeed;
    public float initialAcceleration;
    public float rotationSpeed;
    public float speed;
    public float acceleration;

    
    private InputAction Move;
    public Vector2 move;
    private Vector2 velocity;
    private float smoothXVelocity;
    private float smoothYVelocity;
    private float smoothRotation;

    private float moveRotation;



    void Awake()
    {
        speed = initialMaxSpeed;
        acceleration = initialAcceleration;

       input = new InputMap();
       input.Enable();
       Move = input.Map.Move;
       Move.Enable();
    }

    private void OnEnable()
    {
        Move.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
    }

    void Update()
    {
        move = Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        speed *= acceleration;
        move *= speed;
        move *= Time.deltaTime;
        velocity.x = Mathf.SmoothDamp(velocity.x, move.x, ref smoothXVelocity, 0.06f); 
        velocity.y = Mathf.SmoothDamp(velocity.y, move.y, ref smoothYVelocity, 0.06f);
        rb.velocity = velocity;


        if (move.magnitude > 0)
        {
            SetRotationToMove();
        }
        //Add rooting animation when the player decides to no longer move
        else speed = 0f;
    }

    

    private void SetRotationToMove()
    {
        moveRotation = Mathf.SmoothDamp(moveRotation, Vector2.SignedAngle(new Vector2(0, 1), move), ref smoothRotation, rotationSpeed);
        rb.MoveRotation(Quaternion.Euler(new Vector3(0, 0, moveRotation)));
    }
}
