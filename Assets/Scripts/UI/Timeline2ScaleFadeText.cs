using UnityEngine;

public class Timeline2ScaleFadeText : MonoBehaviour {

    [SerializeField] private TimelineManager timelineManager;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    private void Update() {
        if (timelineManager.IsTimeline1Done) {
            anim.enabled = true;
            anim.Play("Timeline 2 Text");
        }
    }
}