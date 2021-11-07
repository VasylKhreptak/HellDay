using UnityEngine;

public static class RandomAudio
{
    public static void Play(Vector3 position, AudioClip[] audioClips)
    {
        AudioSource.PlayClipAtPoint(audioClips.Random(), position);
    }
    
    public static void Play(AudioSource audioSource, AudioClip[] audioClips)
    {
        audioSource.clip = audioClips.Random();
        audioSource.Play();
    }
}
