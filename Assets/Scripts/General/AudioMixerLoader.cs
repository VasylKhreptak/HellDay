using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerLoader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer _audioMixer;

    private const string MUSIC_PARAMETER = "Music";
    private const string SOUND_PARAMETER = "Sound";
    
    private const string MUSIC_KEY = "MusicVolumeValue";
    private const string SOUND_KEY = "SoundVolumeValue";

    private void Start()
    {
        _audioMixer.SetFloat(MUSIC_PARAMETER, PlayerPrefsSafe.GetFloat(MUSIC_KEY, 0));
        _audioMixer.SetFloat(SOUND_PARAMETER, PlayerPrefsSafe.GetFloat(SOUND_KEY, 0));
    }
}
