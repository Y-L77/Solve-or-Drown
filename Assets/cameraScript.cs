using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public Vector3 offset;          // Offset to maintain from the player
    public float smoothSpeed = 0.125f; // Smoothing factor (lower value = smoother)

    void LateUpdate()
    {
        // Desired camera position based on player position and offset
        Vector3 desiredPosition = player.position + offset;

        // Set the Z position to the camera's current Z position to keep it fixed
        desiredPosition.z = transform.position.z;

        // Smoothly interpolate between current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
