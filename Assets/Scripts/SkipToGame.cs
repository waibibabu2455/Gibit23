using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToGame : MonoBehaviour {
    public GameObject OptionPanel;
    public GameObject Canvas;
    public void skip() {
        SceneManager.LoadScene("Scene1");
    }
    public void option() {
        Canvas.SetActive(false);
        OptionPanel.SetActive(true);
    }
    public void exit() {
        Application.Quit();
    }
}

