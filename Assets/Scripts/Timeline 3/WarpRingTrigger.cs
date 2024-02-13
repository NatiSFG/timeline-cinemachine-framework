using UnityEngine;
using System.Collections;

public class WarpRingTrigger : MonoBehaviour {
    [SerializeField] private TimelineManager timelineManager;

    private BoxCollider col;

    private void Start() {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            StartCoroutine(TriggerEffect());
    }

    private IEnumerator TriggerEffect() {
        // Ensure the trigger effect doesn't last forever
        yield return new WaitForSeconds(1);
        timelineManager.ActiveIndex = 2;
        col.enabled = false;
    }
}