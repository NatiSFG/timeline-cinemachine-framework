using UnityEngine;
using UnityEngine.UI;

public class ControlsWindow : MonoBehaviour {
    [SerializeField] private GameObject controlsWindow;
    [SerializeField] private Button closeWindowButton;

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

    private void TogglePause() {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    private void PauseGame() {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
