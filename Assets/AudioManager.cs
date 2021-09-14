using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public enum AudioGroup { Master, Music, SFX };


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    /*
     * fixed problem of volume slider linearly changing log values (dB) (which made sound barely noticeable at halfway point)
     * volume locked to 0.0001-1
     * kudos: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
    */
    private Dictionary<AudioGroup, string> _audioMixerGroups = new Dictionary<AudioGroup, string>() 
    {
        {AudioGroup.Master, "master"},
        {AudioGroup.Music, "music"},
        {AudioGroup.SFX, "sfx" } 
    };
    
    public void SetVolume(AudioGroup audioGroup, float volume)
    {
        _audioMixer.SetFloat(_audioMixerGroups[audioGroup], 20f * Mathf.Log10(volume));
    }
}
