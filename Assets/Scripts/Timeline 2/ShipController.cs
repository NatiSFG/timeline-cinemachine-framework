using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float rotSpeed = 55f;
    [SerializeField] private float forceMultiplier = 4f;

    private float boostMoveSpeedMult = 1.5f;
    private float baseMoveSpeed = 40f;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        currentMoveSpeed = baseMoveSpeed;
    }

    private void FixedUpdate() {
        CalculateMovement();
    }

    private void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the local directions based on player's current rotation
        Vector3 localForward = transform.forward;
        Vector3 localRight = transform.right;

        Vector3 movement = (localForward * verticalInput + localRight * horizontalInput).normalized * currentMoveSpeed * Time.deltaTime;
        rb.AddForce(movement * forceMultiplier, ForceMode.VelocityChange);

        // Roll Adjustment (Rotation around Z-axis) Left/Right arrows
        float rollRotation = rotSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, rollRotation, Space.Self);
        else if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.forward, -rollRotation, Space.Self);

        // Pitch Adjustment (Rotation around X-axis) Up/Down arrows
        float pitchInput = Input.GetAxis("Pitch");
        float pitchRotation = -pitchInput * rotSpeed * Time.deltaTime; // Inverted for natural input
        transform.Rotate(Vector3.right, pitchRotation, Space.Self);

        // Yaw Adjustment (Rotation around Y-axis) Q/E keys
        float yawInput = Input.GetAxis("Yaw");
        float yawRotation = yawInput * rotSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yawRotation, Space.Self);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            SpeedBoost();
        else currentMoveSpeed = baseMoveSpeed;
    }

    private void SpeedBoost() {
        currentMoveSpeed = baseMoveSpeed * boostMoveSpeedMult;
    }
}
