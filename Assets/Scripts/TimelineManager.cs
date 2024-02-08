using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour {

    [SerializeField] private PlayableDirector[] timelines;
    [SerializeField] private InactivityDetector inactivityDetector;

    public bool IsTimeline1Done { get; set; }
    public bool IsTimeline2Done { get; set; }
    public bool IsTimeline3Done {  get; set; }

    private void Awake() {
        IsTimeline1Done = false;
        IsTimeline2Done = false;
        IsTimeline3Done = false;
    }

    private void Start () {
        for (int i = 0; i < timelines.Length; i++) {
            timelines[i].Play();
            if (i > 0) IsTimeline1Done = true;
            if (i > 1) IsTimeline2Done = true;
            if (i > 2) IsTimeline3Done = true;
        }

        //start with getting timeline manager working
        //each timeline needs to play out fully before continuing to
        //the next one. if timeline[0] was played, set IsTimeline1Done to true
    }
}