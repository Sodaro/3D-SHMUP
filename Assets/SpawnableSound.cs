using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableSound : MonoBehaviour
{
    private AudioSource _audioSource;
	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}
	public void PlayAndDestroy(AudioClip clip)
	{
        _audioSource.clip = clip;
		_audioSource.pitch = Random.Range(0.4f, 1.4f);
		_audioSource.Play();
        Destroy(gameObject, _audioSource.clip.length);
    }
}
