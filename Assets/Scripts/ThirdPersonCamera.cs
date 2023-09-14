using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float mouseSensitivity = 10f;

    private float verticalRotation;
    private float horizontalRotation;

    private void LateUpdate()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        
        if (target == null)
        {
            return;
        }

        if (cam != null)
        {
            cam.transform.position = target.position;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            verticalRotation -= mouseY * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

            horizontalRotation += mouseX * mouseSensitivity;

            cam.transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        }
    }
}
