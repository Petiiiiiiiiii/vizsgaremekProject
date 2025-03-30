using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class fpsCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public int frameSampleSize = 60;

    private float[] frameTimes;
    private int frameIndex = 0;
    private float fpsRefreshTime = 0f;
    private float currentFPS = 0f;

    void Start()
    {
        frameTimes = new float[frameSampleSize];

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
    }

    void Update()
    {
        frameTimes[frameIndex] = Time.unscaledDeltaTime;
        frameIndex = (frameIndex + 1) % frameTimes.Length;

        fpsRefreshTime += Time.unscaledDeltaTime;
        if (fpsRefreshTime >= 0.5f || frameIndex == 0)
        {
            float totalFrameTime = 0f;
            foreach (float t in frameTimes) totalFrameTime += t;
            float avgFrameTime = totalFrameTime / frameTimes.Length;
            currentFPS = 1f / avgFrameTime;

            fpsText.text = $"FPS: {Mathf.Round(currentFPS)}";
            fpsRefreshTime = 0f;
        }
    }

}
