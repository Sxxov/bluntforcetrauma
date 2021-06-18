using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update() {
        if (Input.GetButtonDown("Cancel")) {
            this.OnExit();
        }
    }

    public void OnExit() {
        SceneManager.LoadScene("NewGameScene");
    }
}
