using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    [SerializeField] private GameObject windowObj;
    [SerializeField] private GameObject canvas;

    private ControlsWindow controlsWindow;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        controlsWindow = canvas.GetComponent<ControlsWindow>();
    }
}