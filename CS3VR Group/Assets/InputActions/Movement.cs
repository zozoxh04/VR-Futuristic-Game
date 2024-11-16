using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  #pragma warning disable 649

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    Vector2 horizontalInput;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundCheck;
    bool isGrounded;

    private void Update()
    {
      isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundCheck); // check to see if intersecting with ground to reset vertical velocity
      if (isGrounded)
      {
        verticalVelocity.y = 0f;
      }
      Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
      controller.Move(horizontalVelocity * Time.deltaTime); // frame rate independent

      verticalVelocity.y += gravity * Time.deltaTime;
      controller.Move(verticalVelocity * Time.deltaTime); // frame rate independent
    }
    
    public void ReceiveInput (Vector2 input)
    {
        horizontalInput = input;
    }
}
