using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;



    /*
     * fixed problem of volume slider linearly changing log values (dB) (which made sound barely noticeable at halfway point)
     * volume locked to 0.0001-1
     * kudos: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
    */

    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("master", 20f * Mathf.Log10(volume));
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("music", 20f*Mathf.Log10(volume));
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("sfx", 20f * Mathf.Log10(volume));
    }
}
