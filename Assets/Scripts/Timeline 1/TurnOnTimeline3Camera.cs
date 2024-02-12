using UnityEngine;

public class TurnOnTimeline3Camera : MonoBehaviour {
    [SerializeField] private TimelineManager timelineManager;
    [SerializeField] private new GameObject camera;

    private void Update() {
        if (timelineManager.isTimeline2Done)
            camera.SetActive(true);
    }
}