using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField] private float currentMoveSpeed = 50f;
    [SerializeField] private float rotSpeed = 90f;

    [SerializeField] private float boostMoveSpeedMult = 1.5f;

    private float baseMoveSpeed = 50f;

    private void Awake() {
        currentMoveSpeed = baseMoveSpeed;
    }

    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentMoveSpeed * Time.deltaTime;
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

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            SpeedBoost();
        else currentMoveSpeed = baseMoveSpeed;
    }

    private void SpeedBoost() {
        currentMoveSpeed = baseMoveSpeed * boostMoveSpeedMult;
    }
}
