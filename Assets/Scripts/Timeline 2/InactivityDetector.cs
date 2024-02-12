using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline2;
    [SerializeField] private SecondsUntilIdle secUntilIdle;
    [SerializeField] private TimelineManager timelineManager;
    [SerializeField] private float lastInputTime;
    
    public float inactivityThreshold = 5;


    private bool isActive => Input.anyKey || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
    private Timeline2SwitchCameras switchCams;

    private void Awake() {
        switchCams = GetComponent<Timeline2SwitchCameras>();
    }

    private void Start() {
        lastInputTime = Time.time;
    }

    private void Update() {
        if (timelineManager.ActiveIndex > 0) {
            if (isActive) {
                lastInputTime = Time.time;
                timeline2.time = 0;
                secUntilIdle.ResetCountdown();
            }
            else if (Time.time - lastInputTime > inactivityThreshold) {
                switchCams.DeactivateCamera(switchCams.currentCamIndex);
                timeline2.Play();
            }
        }
    }
}
