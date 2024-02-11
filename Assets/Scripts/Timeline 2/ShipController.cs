using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float rotSpeed = 55f;

    private float boostMoveSpeedMult = 1.5f;
    private float baseMoveSpeed = 40f;

    private void Awake() {
        currentMoveSpeed = baseMoveSpeed;
    }

    void Update() {
        CalculateMovement();
    }

    private void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentMoveSpeed * Time.deltaTime;
        transform.Translate(movement);

        //clamp rotation direction to 30 degrees each way
        // Roll Adjustment (Rotation around Z-axis) Left/Right arrows
        float rollRotation = rotSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(Vector3.forward, rollRotation, Space.World);
        else if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(Vector3.forward, -rollRotation, Space.World);

        // Pitch Adjustment (Rotation around X-axis) Up/Down arrows
        float pitchInput = Input.GetAxis("Pitch");
        float pitchRotation = -pitchInput * rotSpeed * Time.deltaTime; // Inverted for natural input
        transform.Rotate(Vector3.right, pitchRotation, Space.World);

        // Yaw Adjustment (Rotation around Y-axis) Q/E keys
        float yawInput = Input.GetAxis("Yaw");
        float yawRotation = yawInput * rotSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yawRotation, Space.World);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            SpeedBoost();
        else currentMoveSpeed = baseMoveSpeed;
    }

    private void SpeedBoost() {
        currentMoveSpeed = baseMoveSpeed * boostMoveSpeedMult;
    }
}
