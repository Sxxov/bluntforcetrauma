using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadListener : MonoBehaviour
{

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Reload")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
