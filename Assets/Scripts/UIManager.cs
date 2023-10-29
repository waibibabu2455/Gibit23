using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject canvas;
    public VolumeController optionpanel;
    // Update is called once per frame
    private void Start()
    {
        canvas.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            canvas.gameObject.SetActive(true);
        }

    }

    public void esc() {
        SceneManager.LoadScene("Main Menu");
    }
    public void option() {

        VolumeController panel=Instantiate(optionpanel,new Vector3(0,0,0),Quaternion.Euler(0,0,0));
        panel.gameObject.SetActive(true);
        panel.setcanvas(canvas);
        panel.GetComponentInChildren<Button>().GetComponent<VolumeController>().setcanvas(canvas);

    }
    public void returntogame(){
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}