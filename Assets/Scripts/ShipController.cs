using UnityEngine;

public class ShipController : MonoBehaviour {
    public float movementSpeed = 5f;
    public float rotationSpeed = 50f;

    void Update() {
        // Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Yaw Adjustment (Rotation around Y-axis)
        float yawInput = Input.GetAxis("Yaw");
        float yawRotation = yawInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yawRotation);

        // Pitch Adjustment (Rotation around X-axis)
        float pitchInput = Input.GetAxis("Pitch");
        float pitchRotation = -pitchInput * rotationSpeed * Time.deltaTime; // Inverted for natural input
        transform.Rotate(Vector3.right, pitchRotation);

        // Roll Adjustment (Rotation around Z-axis)
        if (Input.GetKey(KeyCode.Q)) {
            float rollRotation = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rollRotation);
        } else if (Input.GetKey(KeyCode.E)) {
            float rollRotation = -rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rollRotation);
        }
    }
}
