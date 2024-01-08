using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private SecondsUntilIdle secUntilIdle;

    public float inactivityThreshold = 5f;
    public float lastInputTime;
    private SwitchCameras switchCams;

    private bool isActive => Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

    private void Awake() {
        switchCams = GetComponent<SwitchCameras>();
    }

    void Start() {
        lastInputTime = Time.time;
    }

    void Update() {
        if (isActive) {
            lastInputTime = Time.time;
            switchCams.enabled = true;
            timeline.Stop();
            secUntilIdle.ResetCountdown();
        }
        else {
            // If no input for inactivityThreshold seconds
            if (Time.time - lastInputTime > inactivityThreshold) {
                switchCams.DeactivateCamera(switchCams.currentCamIndex);
                switchCams.enabled = false;
                timeline.Play();
            }
        }
    }
}
