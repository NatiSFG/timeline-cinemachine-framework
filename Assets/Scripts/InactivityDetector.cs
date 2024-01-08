using UnityEngine;
using UnityEngine.Playables;

public class InactivityDetector : MonoBehaviour {

    [SerializeField] private PlayableDirector timeline;

    private SwitchCameras switchCams;
    private float inactivityThreshold = 5f;
    private float lastInputTime;
    private GameObject timeObj;

    private bool isActive => Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

    private void Awake() {
        switchCams = GetComponent<SwitchCameras>();
    }

    void Start() {
        lastInputTime = Time.time;
        timeObj = timeline.gameObject;
    }

    void Update() {
        if (isActive) {
            Debug.Log("active");
            lastInputTime = Time.time;
            //timeObj.SetActive(false);
            switchCams.enabled = true;
            timeline.Stop();
        }
        else {
            // If no input for inactivityThreshold seconds, do something
            if (Time.time - lastInputTime > inactivityThreshold) {
                Debug.Log("no activity for 5 secs");
                switchCams.DeactivateCamera(switchCams.currentCamIndex);
                switchCams.enabled = false;
                timeline.Play();
            }
        }
    }
}
