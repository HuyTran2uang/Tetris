using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance { get; private set; }
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    private float _volMusic = 0.5f;
    private float _volSound = 0.5f;
    public float VolumeMusic => _volMusic;
    public float VolumeSound => _volSound;

    bool musicIsOpen = true;
    bool soundIsOpen = true;

    public void OnMusicClicked()
    {
        if (musicIsOpen)
        {
            _musicSource.volume = 0;
            musicIsOpen = false;
        }
        else
        {
            _musicSource.volume = _volMusic;
            musicIsOpen = true;
        }
    }

    public void OnSoundClicked()
    {
        if (soundIsOpen)
        {
            _soundSource.volume = 0;
            soundIsOpen = false;
        }
        else
        {
            _soundSource.volume = _volSound;
            soundIsOpen = true;
        }
    }

    public void SetVolumeMusic(float value)
    {
        _volMusic = value;
    }

    public void SetVolumeSound(float value)
    {
        _volMusic = value;
    }

    public void PlayMusicWithClip(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySoundWithClip(AudioClip clip)
    {
        _soundSource.clip = clip;
        _soundSource.Play();
    }

    public void PlaySoundOnce(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        Instance = this;
    }
}
