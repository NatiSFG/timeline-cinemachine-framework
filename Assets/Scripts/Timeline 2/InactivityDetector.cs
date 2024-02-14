using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

/// <summary>
/// This script plays a <see cref="PlayableDirector"/> after the player has been inactive
/// for a duration, given by <see cref="InactivityDetector.secUntilIdle"/>.
/// </summary>
public class InactivityDetector : MonoBehaviour {
    [SerializeField] private PlayableDirector director;
    [SerializeField] private SecondsUntilIdle secUntilIdle;
    [SerializeField] private TimelineManager timelineManager;
    [SerializeField] private ShipController shipController;
    
    public float inactivityThreshold = 5;

    private float lastInputTime = float.NegativeInfinity;
    private bool wasUserActive = false; //so we can use the previous frame's data so we can
                                        //check when the user activity status changes

    private KeyCode[] movementKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Q,
        KeyCode.E, KeyCode.LeftShift, KeyCode.RightShift, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    /// <summary>
    /// Did the user interact via input this frame?
    /// </summary>
    private bool InputThisFrame {
        get { 
            foreach (KeyCode key in movementKeys)
                if (Input.GetKey(key))
                    return true;
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                return true;
            return false;
        }
    }

    /// <summary>
    /// Is the user considered active from recent input?
    /// </summary>
    private bool IsUserActive => Time.time - lastInputTime <= inactivityThreshold;

    private void Update() {
        if (timelineManager.ActiveIndex == 1) {
            secUntilIdle.ResetCountdown();
            if (InputThisFrame)
                lastInputTime = Time.time;

            bool isUserActive = IsUserActive;
            if (isUserActive != wasUserActive) {
                if (isUserActive) {

                }
                else {
                    
                }
                wasUserActive = isUserActive;
            }
        }
    }
}
