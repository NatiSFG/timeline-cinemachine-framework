using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline2;
    [SerializeField] private SecondsUntilIdle secUntilIdle;
    [SerializeField] private TimelineManager timelineManager;
    [SerializeField] private float lastInputTime;
    [SerializeField] private ShipController shipController;
    
    public float inactivityThreshold = 5;

    private ChangeViews changeViews;
    private KeyCode[] movementKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Q,
        KeyCode.E, KeyCode.LeftShift, KeyCode.RightShift, KeyCode.UpArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow };

    private bool isActive => AnyMovementKeyPressed() || MouseMoved();

    private void Awake() {
        changeViews = GetComponent<ChangeViews>();
    }

    private void Start() {
        lastInputTime = Time.time;
    }

    private void Update() {
        if (timelineManager.ActiveIndex == 1) {
            if (isActive) {
                lastInputTime = Time.time;
                timeline2.time = 0;
                changeViews.EnableShipController();
                secUntilIdle.ResetCountdown();
            }
            else if (Time.time - lastInputTime > inactivityThreshold) {
                changeViews.DeactivateCamera(changeViews.currentCamIndex);
                if (!shipController.enabled) {
                    changeViews.EnableShipController();
                    changeViews.currentCamIndex = 0;
                }
                timeline2.Play();
            }
        }
    }

    private bool AnyMovementKeyPressed() {
        foreach (KeyCode key in movementKeys)
            if (Input.GetKeyDown(key) || Input.GetKey(key))
                return true;
        return false;
    }

    private bool MouseMoved() {
        return Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
    }
}
