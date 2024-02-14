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
/// <list type="bullet">
/// <item>Note that this uses intervals in a Timeline.</item>
/// <item>This script only sets the time, and keeps the timeline paused.</item>
/// </list>
/// </remarks>
public class CameraSwitcher : MonoBehaviour {
    [SerializeField] private ShipController shipController;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private float viewDuration = 5;
    [SerializeField] private int viewCount = 6;

    /// <summary>
    /// Calculates what view index the <see cref="director"/> is currently at.
    /// </summary>
    public int CurrentViewIndex => Mathf.FloorToInt((float) director.time / viewDuration);

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R))
            AutoSwitch();
    }

    private void AutoSwitch() {
        int nextIndex;
        if (director.time >= 0 && director.time < director.duration)
            nextIndex = (CurrentViewIndex + 1) % viewCount;
        else
            nextIndex = 0;

        SwitchTo(nextIndex, false);
    }

    /// <summary>
    /// This switches the camera to the view, given by <paramref name="viewIndex"/>.
    /// </summary>
    /// <param name="viewIndex">The index of the view to enter.</param>
    /// <param name="play">Should the <see cref="PlayableDirector"/> continue playing after entering the given view?</param>
    public void SwitchTo(int viewIndex, bool play) {
        director.time = viewIndex * viewDuration;
        if (play) {
            director.Play();
        } else {
            director.Evaluate();
            director.Pause();
        }
        shipController.enabled = viewIndex == 0;
    }
}