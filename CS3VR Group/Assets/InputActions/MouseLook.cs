using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #pragma warning disable 649

    [SerializeField] float sensitivityX = 0.5f;
    [SerializeField] float sensitivityY = 0.5f;
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    float mouseX, mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseX);  // Removed Time.deltaTime here
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivityX;
        mouseY = mouseInput.y * sensitivityY;
    }
}