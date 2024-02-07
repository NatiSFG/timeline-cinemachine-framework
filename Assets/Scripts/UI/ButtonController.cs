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

    private void Start() {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        windowObj.SetActive(false);
        LayoutRebuilder.MarkLayoutForRebuild(button.transform as RectTransform);
        controlsWindow.ResumeGame();
    }
}