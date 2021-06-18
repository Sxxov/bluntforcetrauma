using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    public void Start() {
        Debug.Log("asjaskjas");

        Cursor.lockState = CursorLockMode.None;

        SettingsManager.DryApplySettings();
    }

    public void Update() {
        if (Input.GetButtonDown("Cancel")) {
            this.OnExit();
        }

        if (Input.GetButtonDown("Jump")) {
            this.OnStart();
        }
    }

    public void OnSettings() {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OnStart() {
        SceneManager.LoadScene("MainScene");
    }

    public void OnExit() {
        Application.Quit();
    }
}
