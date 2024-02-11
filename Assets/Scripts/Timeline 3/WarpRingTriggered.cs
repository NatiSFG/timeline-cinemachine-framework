using UnityEngine;

public class WarpRingTriggered : MonoBehaviour {
    [SerializeField] private TimelineManager timelineManager;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            timelineManager.IsTimeline2Done = true;
        }
    }
}