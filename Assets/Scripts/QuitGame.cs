using UnityEngine;

public class QuitGame : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
