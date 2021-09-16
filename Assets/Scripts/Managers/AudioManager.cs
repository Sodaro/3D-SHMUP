using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public enum AudioGroup { Master, Music, SFX };


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private Dictionary<AudioGroup, string> _audioMixerGroups;

    private void Awake()
	{
        _audioMixerGroups = new Dictionary<AudioGroup, string>()
        {
            {AudioGroup.Master, "master"},
            {AudioGroup.Music, "music"},
            {AudioGroup.SFX, "sfx" }
        };
	}
	private void Start()
	{
        float masterVolume = PlayerPrefs.GetFloat(_audioMixerGroups[AudioGroup.Master], 1);
        float musicVolume = PlayerPrefs.GetFloat(_audioMixerGroups[AudioGroup.Music], 1);
        float sfxVolume = PlayerPrefs.GetFloat(_audioMixerGroups[AudioGroup.SFX], 1);

        SetVolumeDB(AudioGroup.Master, masterVolume);
        SetVolumeDB(AudioGroup.Music, musicVolume);
        SetVolumeDB(AudioGroup.SFX, sfxVolume);
    }


	public string GetMixerGroupKey(AudioGroup audioGroup)
	{
        return _audioMixerGroups[audioGroup];
	}
    
    public void SetVolumeLinear(AudioGroup audioGroup, float volumeLinear)
    {
        _audioMixer.SetFloat(_audioMixerGroups[audioGroup], 20f * Mathf.Log10(volumeLinear));
        PlayerPrefs.SetFloat(_audioMixerGroups[audioGroup], 20f * Mathf.Log10(volumeLinear));
    }
    private void SetVolumeDB(AudioGroup audioGroup, float volumeDB)
	{
        _audioMixer.SetFloat(_audioMixerGroups[audioGroup], volumeDB);
    }
}
