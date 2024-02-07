using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private SecondsUntilIdle secUntilIdle;
    [SerializeField] private float lastInputTime;
    
    public float inactivityThreshold = 5;

    private SwitchCameras switchCams;

    private bool isActive => Input.anyKeyDown || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

    private void Awake() {
        switchCams = GetComponent<SwitchCameras>();
    }

    void Start() {
        lastInputTime = Time.time;
    }

    void Update() {
        if (isActive) {
            lastInputTime = Time.time;
            timeline.Stop();
            secUntilIdle.ResetCountdown();
        }
        else if (Time.time - lastInputTime > inactivityThreshold) {
            switchCams.DeactivateCamera(switchCams.currentCamIndex);
            timeline.Play();
        }
    }
}
