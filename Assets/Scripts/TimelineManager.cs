using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour {
    /// <summary>
    /// Represents a <see cref="PlayableDirector"/>, and any associated <see cref="GameObject"/>s and <see cref="Component"/>s that should be active/enabled during its play session.
    /// </summary>
    [Serializable]
    private struct TimelineSequence {
        public PlayableDirector director;
        public GameObject[] gameObjects;
        public Behaviour[] components;
        public Renderer[] renderers;

        public bool AutoFinish => director != null && director.extrapolationMode == DirectorWrapMode.None;

        public void Play() {
            if (director != null)
                director.Play();
            SetObjectsEnabled(true);
        }

        public void Stop() {
            if (director != null)
                director.Stop();
            SetObjectsEnabled(false);
        }

        public void SetObjectsEnabled(bool value) {
            foreach (GameObject g in gameObjects)
                g.SetActive(value);
            foreach (Behaviour b in components)
                b.enabled = value;
            foreach (Renderer r in renderers)
                r.enabled = value;
        }
    }

    [SerializeField] private TimelineSequence[] timelines = { };

    private int activeIndex = -1;
    private bool activeIndexIsDirty = false;

    public event Action onTimelineStarted;
    public event Action onTimelineStopped;

    private bool IsValidIndex(int index) => index >= 0 && index < timelines.Length;

    /// <summary>
    /// The index of the current timeline that is being played, from <see cref="timelines"/>.
    /// </summary>
    public int ActiveIndex {
        get { return activeIndex; }
        set {
            activeIndexIsDirty = true;
            if (IsValidIndex(activeIndex))
                timelines[activeIndex].Stop();

            Debug.Log("activeIndex = " + value);
            activeIndex = value;
            for (int i = 0; i < timelines.Length; i++)
                if (i != activeIndex)
                    timelines[i].SetObjectsEnabled(false);

            if (IsValidIndex(activeIndex))
                timelines[activeIndex].Play();
        }
    }

    private void OnEnable() {
        StartCoroutine(PlayAllTimelines());
    }

    private IEnumerator WaitFor(PlayableDirector director) {
        if (director == null)
            yield break;
        bool isPlaying = director.state == PlayState.Playing;

        if (director.extrapolationMode != DirectorWrapMode.None)
            Debug.LogError("This code is only setup for " + nameof(DirectorWrapMode) + "." + nameof(DirectorWrapMode.None));

        while (director.time >= 0 && director.state == PlayState.Playing)
            yield return null;
    }

    private IEnumerator PlayAllTimelines() {
        for (int i = 0; i < timelines.Length; i++) {
            ActiveIndex = i;
            activeIndexIsDirty = false; //NOTE: Maybe restructure later to NOT have to do this

            OnTimelineStarted();
            onTimelineStarted?.Invoke();

            if (timelines[i].AutoFinish) {
                //NOTE: The PlayableDirector is set to DirectorWrapMode.None:
                //  so we'll wait for 1 iteration of the timeline to complete:
                yield return WaitFor(timelines[i].director);
            } else {
                //NOTE: The PlayableDirector is set to DirectorWrapMode.Looping, so we can't auto-finish,
                //  but we'll wait for someone else to set the next ActiveIndex:
                while (!activeIndexIsDirty)
                    yield return null;
                activeIndexIsDirty = false;
                i = activeIndex;
            }


            OnTimelineStopped();
            onTimelineStopped?.Invoke();
        }
    }

    private void OnTimelineStarted() {

    }

    private void OnTimelineStopped() {

    }
}
