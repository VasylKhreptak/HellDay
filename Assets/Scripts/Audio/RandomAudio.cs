using UnityEngine;

public static class RandomAudio
{
    public static void Play(Vector3 position, AudioClip[] audioClips, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClips.Random(), position, volume);
    }
    
    public static void Play(AudioSource audioSource, AudioClip[] audioClips)
    {
        audioSource.clip = audioClips.Random();
        audioSource.Play();
    }
}
