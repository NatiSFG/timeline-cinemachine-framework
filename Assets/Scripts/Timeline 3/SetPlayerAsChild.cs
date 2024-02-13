using UnityEngine;

public class SetPlayerAsChild : MonoBehaviour {
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform mothership;

    #region Public Inspector Methods
    public void InspectorSetChild() {
        playerAnim.enabled = false;
        transform.SetParent(mothership, true);
    }
    #endregion
}