using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlsWindow : MonoBehaviour {
    [SerializeField] private GameObject controlsWindow;

    private bool isPaused = false;

    private void Update() {
        EnableDisableWindow();
    }

    private void EnableDisableWindow() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            controlsWindow.SetActive(!controlsWindow.activeInHierarchy);
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
        //when playing the game again, the x button is unselected
        EventSystem.current.SetSelectedGameObject(null);
    }
}
