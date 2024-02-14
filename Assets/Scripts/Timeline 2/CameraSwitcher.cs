using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

/// <summary>
/// This script represents the ability for the player to cycle through Cinemachine virtual cameras in a
/// Cinemachine by pressing the R key.
/// </summary>
/// <remarks>
/// Note that this uses intervals in a Timeline.
/// </remarks>
public class CameraSwitcher : MonoBehaviour {
    [SerializeField] private ShipController shipController;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private float viewDuration = 5;

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
            Switch();
    }

    private void Switch() {
        //makes each camera start at the beginning of its time and sets the ship controller
        //to be enabled only on the first view
        if (director.time >= 0 && director.time < director.duration) {
            float nextTime = viewDuration * Mathf.Floor(1f / 5 * (float) director.time) + viewDuration;
            nextTime %= 30;
            director.time = nextTime;
            director.Evaluate();
            director.Pause();

            //[0, 30) / 5 will give you a number in range [0, 5]
            //      that represents the index of the clip you're going to be playing.
            float index = nextTime / viewDuration;

            shipController.enabled = index == 0;
        }
    }
}