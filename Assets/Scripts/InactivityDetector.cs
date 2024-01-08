using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline; 

    private float inactivityThreshold = 5f;
    private float lastInputTime;

    private bool isActive => Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

    void Start() {
        lastInputTime = Time.time;
    }

    void Update() {
        if (isActive) {
            Debug.Log("active");
            lastInputTime = Time.time;
            timeline.Pause();
        }
        else {
            // If no input for inactivityThreshold seconds, do something
            if (Time.time - lastInputTime > inactivityThreshold) {
                Debug.Log("no activity for 5 secs");
                timeline.Resume();
                //if (isActive)
                //    timeline.Stop();
            }
        }
    }
}
