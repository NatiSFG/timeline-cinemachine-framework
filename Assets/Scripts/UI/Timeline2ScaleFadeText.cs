using UnityEngine;

public class Timeline2ScaleFadeText : MonoBehaviour {

    [SerializeField] private TimelineManager timelineManager;
    [SerializeField] private GameObject changeViewsObj;
    [SerializeField] private GameObject passThruRingObj;
    [SerializeField] private Animator anim;

    private void Update() {
        if (timelineManager.IsTimeline1Done) {
            //still have to activate obj through code so animator plays
            changeViewsObj.SetActive(true);
            anim.Play("Change Views");
            
            //set this active once change views anim is done playing
            //passThruRingObj.SetActive(true);
        }
    }

    private void PlayChangeViewsAnim() {

    }
}