using UnityEngine;
using TMPro;

public class SecondsUntilIdle : MonoBehaviour {
    [SerializeField] private InactivityDetector detector;

    private TextMeshProUGUI text;
    private float countdownTimer;
    private bool isCountingDown;

    void Start() {
        text = GetComponent<TextMeshProUGUI>();
        ResetCountdown();
    }

    void Update() {
        if (isCountingDown) {
            countdownTimer -= Time.deltaTime;

            int secondsRemaining = Mathf.RoundToInt(countdownTimer);
            text.text = "Seconds Until Idle: " + secondsRemaining;

            if (countdownTimer <= 0f) {
                text.text = "You are Idle";
                isCountingDown = false;
            }
        }
    }

    public void ResetCountdown() {
        countdownTimer = detector.inactivityThreshold;
        isCountingDown = true;
    }
}
