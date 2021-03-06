using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Vector2 aimPos;

    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    Camera cam;

    // Called when the Game Object is instatiated
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;   
    }

    void FixedUpdate() {
        Debug.Log(moveInput);
        Move();
        LookDirection();
    }

    // Gets input of keys whenever player moves and stores the value in moveInput
    void OnMove(InputValue value) { 
        moveInput = value.Get<Vector2>();
    }

    // Uses the input from OnMove to adjust velocity of the rigidbody
    void Move() {     
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        rb.velocity = playerVelocity;
    }

    // Points the player to the position of the mouse
    void LookDirection() {
        aimPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDir = aimPos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

}
