using UnityEngine;

public class SwitchCameras : MonoBehaviour {
    [SerializeField] private GameObject[] cams;

    private int currentCamIndex = 0;

    void Start() {
        // Ensure that at least one camera is active
        if (cams.Length > 0)
            ActivateCamera(currentCamIndex);
    }

    void Update() {
        // Check for the "R" key press
        if (Input.GetKeyDown(KeyCode.R))
            ToggleCameras();
    }

    private void ToggleCameras() {
        DeactivateCamera(currentCamIndex);
        // Move to the next camera in the array
        currentCamIndex = (currentCamIndex + 1) % cams.Length;
        ActivateCamera(currentCamIndex);
    }

    private void ActivateCamera(int index) {
        if (index >= 0 && index < cams.Length)
            cams[index].SetActive(true);
    }

    private void DeactivateCamera(int index) {
        if (index >= 0 && index < cams.Length)
            cams[index].SetActive(false);
    }
}
