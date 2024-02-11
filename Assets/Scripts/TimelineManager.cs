using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour {

    [SerializeField] private PlayableDirector[] timelines;
    [SerializeField] private Timeline2SwitchCameras switchCams;
    [SerializeField] private InactivityDetector inactivityDetector;
    [SerializeField] private GameObject secsUntilIdle;
    [SerializeField] private ShipController shipController;

    [SerializeField] private GameObject warpRing;

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
        //when entering the warp ring trigger, timeline 2 is done
        if (OnTriggerEnter.Equals(warpRing, GameObject.FindGameObjectWithTag("Player"))) {
            timelines[1].Stop();
            Debug.Log("timeline 2 is done");
            IsTimeline2Done = true;

            PlayTimeline3();
            yield return new WaitForSeconds((float)timelines[2].duration);
            IsTimeline3Done = true;
        }

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