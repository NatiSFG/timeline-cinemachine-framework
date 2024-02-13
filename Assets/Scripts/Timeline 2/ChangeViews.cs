using Cinemachine;
using UnityEngine;

public class ChangeViews : MonoBehaviour {
    [SerializeField] private GameObject[] cams;
    [SerializeField] private GameObject yellowCam;
    [SerializeField] private GameObject PurpleVolume;
    [SerializeField] private GameObject YellowVolume;
    [SerializeField] private ShipController shipController;

    public int currentCamIndex = 0;

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
        shipController.enabled = false;
        // Move to the next camera in the array
        currentCamIndex = (currentCamIndex + 1) % cams.Length;
        ActivateCamera(currentCamIndex);
    }

    private void ActivateCamera(int index) {
        if (index >= 0 && index < cams.Length) {
            cams[index].SetActive(true);
            if (cams[index] == yellowCam) {
                PurpleVolume.SetActive(false);
                YellowVolume.SetActive(true);
            }
            else {
                YellowVolume.SetActive(false);
                PurpleVolume.SetActive(true);
            }
        }
        if (index == 0 && !shipController.enabled)
            EnableShipController();
    }

    public void DeactivateCamera(int index) {
        if (index >= 0 && index < cams.Length)
            cams[index].SetActive(false);
    }

    public void EnableShipController() {
        shipController.enabled = true;
    }
}