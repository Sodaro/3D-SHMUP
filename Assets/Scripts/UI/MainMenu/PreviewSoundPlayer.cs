using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSoundPlayer : MonoBehaviour
{
	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayWithoutInterrupt()
	{
		if (_audioSource.isPlaying)
			return;

		_audioSource.Play();
	}
}
