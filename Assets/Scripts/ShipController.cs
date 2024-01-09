using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float moveSpeedBoostMultiplier = 1.5f;
    [SerializeField] private float rotSpeed = 90f;

    private float baseMoveSpeed;

    private void Awake() {
        baseMoveSpeed = moveSpeed;
    }

    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Yaw Adjustment (Rotation around Y-axis)
        float yawInput = Input.GetAxis("Yaw");
        float yawRotation = yawInput * rotSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yawRotation);

        // Pitch Adjustment (Rotation around X-axis)
        float pitchInput = Input.GetAxis("Pitch");
        float pitchRotation = -pitchInput * rotSpeed * Time.deltaTime; // Inverted for natural input
        transform.Rotate(Vector3.right, pitchRotation);

        // Roll Adjustment (Rotation around Z-axis)
        if (Input.GetKey(KeyCode.Q)) {
            float rollRotation = rotSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rollRotation);
        } else if (Input.GetKey(KeyCode.E)) {
            float rollRotation = -rotSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rollRotation);
        }

        if (Input.GetKey(KeyCode.LeftShift))
            SpeedBoost();
        else moveSpeed = baseMoveSpeed;
    }

    private void SpeedBoost() {
        moveSpeed *= moveSpeedBoostMultiplier;
    }
}
