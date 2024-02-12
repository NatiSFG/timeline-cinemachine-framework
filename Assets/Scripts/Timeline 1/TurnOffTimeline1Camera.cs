using UnityEngine;

public class TurnOffTimeline1Camera : MonoBehaviour {

    [SerializeField] private TimelineManager timelineManager;

    private void Update() {
        if (timelineManager.isTimeline1Done)
            gameObject.SetActive(false);
    }
}