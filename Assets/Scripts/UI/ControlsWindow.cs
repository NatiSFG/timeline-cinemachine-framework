using UnityEngine;

public class ControlsWindow : MonoBehaviour {
    [SerializeField] private GameObject controlsWindow;

    private bool isPaused = false;

    private void Update() {
        EnableDisableWindow();
    }

    private void EnableDisableWindow() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            controlsWindow.SetActive(!controlsWindow.activeSelf);
            TogglePause();
        }
    }

    void TogglePause() {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    void PauseGame() {
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame() {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
