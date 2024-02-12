using UnityEngine;

public class Mothership : MonoBehaviour {

    [SerializeField] private TimelineManager timelineManager;

    private Animator anim;
    private MeshRenderer meshRend;

    private void Start() {
        meshRend = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
        meshRend.enabled = false;
        anim.enabled = false;
    }

    private void Update() {
        if (timelineManager.isTimeline2Done) {
            meshRend.enabled = true;
            anim.enabled = true;
        }
    }
}