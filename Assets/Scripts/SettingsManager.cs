using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public TMP_Text resolutionText;
    public TMP_Text fullscreenText;
    private const string RESOLUTION_SETTING_KEY = "resolution";
    private const string FULLSCREEN_SETTING_KEY = "fullscreen";
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;
    private Resolution currentResolution;
    private bool currentIsFullscreen = false;

    public void Start() {
        Debug.Log("start");
        Cursor.lockState = CursorLockMode.None;

        this.resolutions = Screen.resolutions;
        this.currentIsFullscreen = PlayerPrefs.GetInt(FULLSCREEN_SETTING_KEY) == 1;
        this.currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_SETTING_KEY);

        if (this.currentResolutionIndex >= this.resolutions.Length
            || this.currentResolutionIndex < 0) {
            this.currentResolutionIndex = 0;
        }

        this.ApplySettingsTexts();
        this.ApplySettings();
    }

    private void ApplySettingsTexts() {
        this.ApplyFullscreenText(this.currentIsFullscreen);
        this.ApplyResolutionText(this.currentResolutionIndex);
    }

    public void ApplySettings() {
        this.ApplyResolution(this.currentResolutionIndex);
        this.ApplyFullscreen(this.currentIsFullscreen);
    }

    public static void DryApplySettings() {
        Resolution[] resolutions = Screen.resolutions;
        bool isFullscreen = PlayerPrefs.GetInt(FULLSCREEN_SETTING_KEY) == 1;
        int resolutionIndex = PlayerPrefs.GetInt(RESOLUTION_SETTING_KEY);
        Resolution resolution = resolutions[resolutionIndex];

        if (!Screen.currentResolution.Equals(resolution)) {
            Screen.SetResolution(
                resolution.width, 
                resolution.height, 
                isFullscreen
            );
        }
    }

    public void OnNextResolution() {
        if (this.currentResolutionIndex >= this.resolutions.Length - 1) {
            this.currentResolutionIndex = 0;
        } else {
            ++this.currentResolutionIndex;
        }

        this.ApplyResolutionText(this.currentResolutionIndex);
        this.ApplyResolution(this.currentResolutionIndex);
    }

    public void OnPreviousResolution() {
        if (this.currentResolutionIndex <= 0) {
            this.currentResolutionIndex = this.resolutions.Length - 1;
        } else {
            --this.currentResolutionIndex;
        }

        this.ApplyResolutionText(this.currentResolutionIndex);
        this.ApplyResolution(this.currentResolutionIndex);
    }

    public void OnToggleFullscreen() {
        this.ApplyFullscreenText(!this.currentIsFullscreen);
        this.ApplyFullscreen(!this.currentIsFullscreen);
    }

    public void OnBack() {
        SceneManager.LoadScene("NewGameScene");
    }

    private void ApplyFullscreenText(bool isFullscreen) {
        this.fullscreenText.text = isFullscreen ? "fullscreen" : "windowed";
    }

    public void ApplyFullscreen(bool isFullscreen) {
        this.currentIsFullscreen = isFullscreen;

        Screen.SetResolution(this.currentResolution.width, this.currentResolution.height, isFullscreen);

        PlayerPrefs.SetInt(FULLSCREEN_SETTING_KEY, isFullscreen ? 1 : 0);
    }

    private void ApplyResolutionText(int index) {
        this.resolutionText.text = this.SerializeResolution(this.resolutions[index]);
    }

    private void ApplyResolution(int index) {
        Resolution resolution = this.resolutions[index];

        this.currentResolution = resolution;

        if (!Screen.currentResolution.Equals(this.currentResolution)) {
            Screen.SetResolution(resolution.width, resolution.height, this.currentIsFullscreen);
        }

        PlayerPrefs.SetInt(RESOLUTION_SETTING_KEY, index);
    }

    private string SerializeResolution(Resolution resolution) {
        return resolution.width + "x" + resolution.height + "@" + resolution.refreshRate + "hz";
    }
}
