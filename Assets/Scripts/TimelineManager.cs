using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour {

    [SerializeField] private PlayableDirector[] timelines;

    [Header("Timeline 2")]
    [SerializeField] private Timeline2SwitchCameras switchCams;
    [SerializeField] private InactivityDetector inactivityDetector;
    [SerializeField] private GameObject secsUntilIdle;
    [SerializeField] private ShipController shipController;

    [Header("Timeline 3")]
    [SerializeField] private GameObject warpRing;
    [SerializeField] private GameObject mothership;

    public int timelineIndex;

    private void Start () {
        DisableTimeline2CamsAndCounter();
        StartCoroutine(PlayTimelines());
    }

    private IEnumerator PlayTimelines() {
        PlayTimeline1();
        yield return new WaitForSeconds((float)timelines[0].duration);
        timelines[0].Stop();
        isTimeline1Done = true;

        PlayTimeline2();

        WaitForSeconds wait = new WaitForSeconds(1);
        while (!isTimeline2Done)
            yield return wait;
        timelines[1].Stop();
        Debug.Log("should start playing timeline 3");
        PlayTimeline3();
        isTimeline3Done = true;
        //when timeline 3 done, hold frame and have restart option appear in HUD
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
        Debug.Log("playing timeline 3");
        timelines[1].gameObject.SetActive(false);
        timelines[2].gameObject.SetActive(true);
        shipController.enabled = false;
        DisableTimeline2CamsAndCounter();
        timelines[2].Play();
        while (!isTimeline3Done)
            timelines[2].time = 0;
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