using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private SecondsUntilIdle secUntilIdle;
    [SerializeField] private float lastInputTime;
    //[SerializeField] private float sequenceLength = 5;
    
    public float inactivityThreshold = 5;

    private SwitchCameras switchCams;
    //private float sequenceCount = 6;
    //private float currentTime = 123;

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
            //switchCams.enabled = true;
            timeline.Stop();
            // If no input for inactivityThreshold seconds
            //This math makes a pattern that rounds up to the next sequence's start time,
            //Wrapping back around to t = 0 after we've passed all the sequences.
            //      __      __
            //    __      __
            //  __      __
            //__      __
            //timeline.time = (sequenceLength * Mathf.Ceil(currentTime / sequenceLength)) % (sequenceLength * (sequenceCount + 1));
            secUntilIdle.ResetCountdown();
        }
        else if (Time.time - lastInputTime > inactivityThreshold) {
            switchCams.DeactivateCamera(switchCams.currentCamIndex);
            //switchCams.enabled = false;
            timeline.Play();
        }
    }
}
