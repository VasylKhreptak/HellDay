using UnityEngine;
using UnityEngine.EventSystems;

public class UI_OnPointerDownSound : MonoBehaviour, IPointerDownHandler
{
    [Header("Preferences")]
    [SerializeField] private AudioClip[] _audioClips;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlaySound(eventData.pointerCurrentRaycast.worldPosition);
    }

    private void PlaySound(Vector3 pos)
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _audioClips.Random(), pos,
            1f, 0f);
    }
}