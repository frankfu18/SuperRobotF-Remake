using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimeCoundown : MonoBehaviour{


    public Text Text;

    private Slider Slider;

    private float totalTime;

    private float remainingTime = 0;


    public bool autoCountdown = true;

    private Action Done;

    private void Awake() {
        Slider = GetComponent<Slider>();
    }


    private void Update() {
        if (!autoCountdown || remainingTime == 0) {
            return;
        }
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0) {
            ToDone();
        }
    }

    private void FixedUpdate() {
        if (!autoCountdown) {
            return;
        }
        RefreshUI();
    }

    private void OnEnable() {
        RefreshUI();
        // 如果不是自动计数，且总时间为0.则直接通过
        if (!autoCountdown && totalTime == 0) {
            Text.text = "00:00:00";
            Slider.value = 1f;
        }
    }

    private void RefreshUI() {
        if (remainingTime <= 0 || totalTime <= 0 || Slider == null || Text == null) {
            return;
        }
        int hour = (int)remainingTime / TimeSystem.HourToSeconds;
        int minute = ((int)remainingTime % TimeSystem.HourToSeconds) / TimeSystem.MinuteToSeconds;
        int seconds = (int)(Mathf.Min(remainingTime + 1, totalTime)) % TimeSystem.MinuteToSeconds;

        Text.text = $"{hour:00}:{minute:00}:{seconds:00}";
        Slider.value = 1f - Mathf.Max(0f, remainingTime / totalTime);
    }

    private void ToDone() {
        Text.text = "00:00:00";
        remainingTime = 0;
        totalTime = 0;
        Slider.value = 1f;
        Done?.Invoke();
    }

    public  void Init(float _total, float _remainingTime, Action done = null) {
        totalTime = _total;
        remainingTime = _remainingTime;
        Done = done;
        RefreshUI();
    }

}
