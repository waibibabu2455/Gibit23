using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; 

    public void OnVolumeChanged()
    {
        float volume = volumeSlider.value; 
        AudioListener.volume = volume; 
    }
}






