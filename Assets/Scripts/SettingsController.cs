using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public TMP_Dropdown displayModeDropdown;
    public TMP_Dropdown refreshRateDropdown;

    public Slider VolumeSlider;
    public Slider Sensitivity;

    private void Start()
    {
        FullScreenMode mode = FullScreenMode.ExclusiveFullScreen;
        int refreshRate = 60;

        int displayModeIndex = 0;
        int refreshRateIndex = 0;

        if (PlayerPrefs.GetString("displayMode") != null) 
        {
            switch (PlayerPrefs.GetString("displayMode"))
            {
                case "ExclusiveFullScreen": mode = FullScreenMode.ExclusiveFullScreen; displayModeIndex = 0; break;
                case "Windowed": mode = FullScreenMode.Windowed; displayModeIndex = 1; break;
                case "FullScreenWindow": mode = FullScreenMode.FullScreenWindow; displayModeIndex = 2; break;
            }

            switch (PlayerPrefs.GetInt("refreshRate"))
            {
                case 60: refreshRate = 60; refreshRateIndex = 0; break;
                case 120: refreshRate = 120; refreshRateIndex = 1; break;
                case 144: refreshRate = 144; refreshRateIndex = 2; break;
                case 165: refreshRate = 165; refreshRateIndex = 3; break;
                case 200: refreshRate = 200; refreshRateIndex = 4; break;
            }
        }

        displayModeDropdown.value = displayModeIndex;
        refreshRateDropdown.value = refreshRateIndex;

        if (mode == FullScreenMode.Windowed) Screen.SetResolution(1280, 720, mode, refreshRate);
        else Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, mode, refreshRate);

 

        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            VolumeSlider.value = PlayerPrefs.GetFloat("volumeLevel");
            AudioListener.volume = VolumeSlider.value;
        }
        else 
        {
            VolumeSlider.value = 0.4f;
            AudioListener.volume = VolumeSlider.value;
        }

        if (PlayerPrefs.HasKey("sensitivity")) Sensitivity.value = PlayerPrefs.GetFloat("sensitivity");
        else Sensitivity.value = 0.4f;
    }
    public void ApplySettings()
    {
        FullScreenMode mode = FullScreenMode.ExclusiveFullScreen;

        switch (displayModeDropdown.value)
        {
            case 0: mode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: mode = FullScreenMode.Windowed; break;
            case 2: mode = FullScreenMode.FullScreenWindow; break;
        }

        int refreshRate = 60;
        switch (refreshRateDropdown.value)
        {
            case 0: refreshRate = 60; break;
            case 1: refreshRate = 120; break;
            case 2: refreshRate = 144; break;
            case 3: refreshRate = 165; break;
            case 4: refreshRate = 200; break;
        }

        if (mode == FullScreenMode.Windowed) Screen.SetResolution(1280, 720, mode, refreshRate);
        else Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, mode, refreshRate);

        AudioListener.volume = VolumeSlider.value;

        PlayerPrefs.SetFloat("volumeLevel",VolumeSlider.value);
        PlayerPrefs.SetString("displayMode", mode.ToString());
        PlayerPrefs.SetInt("refreshRate", refreshRate);
        PlayerPrefs.SetFloat("sensitivity", Sensitivity.value);
    }
}
