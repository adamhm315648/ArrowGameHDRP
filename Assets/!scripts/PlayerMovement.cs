using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float lookYLimit = 45f;

    private float rotationX;
    private float rotationY;

    public bool canLook;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if(!canLook) return;
        HandleCameraLook();
    }

    // Separate function to handle camera look
    void HandleCameraLook()
    {
        // Handle vertical (up-down) camera rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        // Handle horizontal (left-right) rotation
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -lookYLimit, lookYLimit); // Limit left-right rotation

        // Apply vertical rotation to the camera (only affecting the up-down view)
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Apply clamped horizontal rotation to the player body (affecting left-right view)
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
}
