using UnityEngine;

public class RandomAudioCore : MonoBehaviour
{
    public void PlayRandomAudio(Vector3 position, AudioClip[] audioClips)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], 
            position);
    }
}
