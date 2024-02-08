using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour {

    [SerializeField] private PlayableDirector[] timelines;
    [SerializeField] private Timeline2SwitchCameras switchCams;
    [SerializeField] private InactivityDetector inactivityDetector;
    [SerializeField] private GameObject secsUntilIdle;
    [SerializeField] private ShipController shipController;

    public bool IsTimeline1Done { get; set; }
    public bool IsTimeline2Done { get; set; }
    public bool IsTimeline3Done {  get; set; }

    private void Awake() {
        IsTimeline1Done = IsTimeline2Done = IsTimeline3Done = false;
    }

    private void Start () {
        DisableTimeline2CamsAndCounter();
        StartCoroutine(PlayTimelines());
    }

    private IEnumerator PlayTimelines() {
        PlayTimeline1();
        yield return new WaitForSeconds((float)timelines[0].duration);
        timelines[0].Stop();
        IsTimeline1Done = true;

        PlayTimeline2();
        yield return new WaitForSeconds((float) timelines[1].duration);
        Debug.Log("timeline 2 is done");
        timelines[1].Stop();
        IsTimeline2Done = true;
    }

    private void PlayTimeline1() {
        shipController.enabled = false;
        timelines[0].Play();
    }

    private void PlayTimeline2() {
        timelines[0].gameObject.SetActive(false);
        timelines[1].gameObject.SetActive(true);
        shipController.enabled = true;
        EnableTimeline2CamsAndCounter();
    }

    //timeline 3 only plays when warp ring is triggered
    private void PlayTimeline3() {
        timelines[1].gameObject.SetActive(false);
        timelines[2].gameObject.SetActive(true);
        shipController.enabled = false;
        DisableTimeline2CamsAndCounter();
        timelines[2].Play();
        IsTimeline3Done = true; //to move outside of method bc waiting duration in coroutine
    }

    private void DisableTimeline2CamsAndCounter() {
        switchCams.enabled = false;
        inactivityDetector.enabled = false;
        secsUntilIdle.SetActive(false);
    }

    private void EnableTimeline2CamsAndCounter() {
        switchCams.enabled = true;
        inactivityDetector.enabled = true;
        secsUntilIdle.SetActive(true);
    }
}