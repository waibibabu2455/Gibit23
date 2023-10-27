using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToGame : MonoBehaviour {
    public void skip() {
        SceneManager.LoadScene("Scene1");
    }
}

