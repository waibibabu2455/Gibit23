using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject canvas;
    public VolumeController optionpanel;
    public bool canmute;
    public bool canshow;
    public GameObject guideimg;
    public GameObject img;
    // Update is called once per frame
    private void Start()
    {
        canvas.gameObject.SetActive(false);
        canshow = true;
    }
    void Update()
    {


    }

    public void esc() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
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
    public void showguide() {
        if (canshow)
        {
             img=Instantiate(guideimg);
            canshow = false;
        }
        else { 
            Destroy(img);
            canshow=true;

        }
    }
}
