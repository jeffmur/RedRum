using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Transform muteToggle;
    private GameObject volOn;
    private GameObject volOff;

    void Mute() { audioMixer.SetFloat("MasterVolume", -80); } // MUTE 
    public float savedVolume = 0;

    private void Start()
    {
        volOn = muteToggle.GetChild(0).gameObject;
        volOff = muteToggle.GetChild(1).gameObject;
    }

    // Used by slider
    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);
        adjustTrigger(vol);
        if (vol <= -40)
            Mute();
    }

    // adjusts icon from slider value
    private void adjustTrigger(float vol)
    {
        // toggle off
        if (vol <= - 40)
        {
            // Volume
            volOn.gameObject.SetActive(false);
            volOff.SetActive(true);
        }
        // stay on
        else
        {
            savedVolume = vol;
            volOn.SetActive(true);
            volOff.SetActive(false);
        }
    }
    
    // toggle icons
    public void VolumeToggle()
    {
        // Trigger Volume Off
        if (volOn.activeSelf)
        {
            volOn.SetActive(false);
            volOff.SetActive(true);
            volumeSlider.value = -40f;
            Mute();
        }
        // Trigger Volume On
        else
        {
            volOn.SetActive(true);
            volOff.SetActive(false);
            volumeSlider.value = savedVolume;
            audioMixer.SetFloat("MasterVolume", savedVolume);
        }
    }
}
