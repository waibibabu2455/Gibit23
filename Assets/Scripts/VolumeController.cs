using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject volumcontroller;
    public GameObject Canvas;

    public void OnVolumeChanged()
    {
        float volume = volumeSlider.value; 
        AudioListener.volume = volume;
    }

    public void exit() {
        volumcontroller.gameObject.SetActive(false);
        Canvas.SetActive(true);
    }
    public void setcanvas(GameObject canvas) { 
        Canvas = canvas;
    }
}






