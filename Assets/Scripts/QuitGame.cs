using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
