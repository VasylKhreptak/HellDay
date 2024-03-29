using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioPooler : MonoBehaviour
{
    [Serializable]
    private class TrackInfo
    {
        public string name;
        public AudioMixerGroup group;
        public IEnumerator trackFader;
    }

    [Serializable]
    private class AudioPoolItem
    {
        public GameObject go;
        public AudioSource audioSource;
        public float unimportance = float.MaxValue;
        public bool isPlaying;
        public IEnumerator coroutine;
        public uint ID;
    }

    public static AudioPooler Instance;

    [Header("References")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Preferences")]
    [SerializeField] private int _maxSounds = 30;
    [SerializeField] private float _maxSoundDistance = 25f;
    [SerializeField] private float _minSoundDistance = 1f;
    [SerializeField] private AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;

    private Dictionary<string, TrackInfo> _tracks = new Dictionary<string, TrackInfo>();
    private List<AudioPoolItem> _pool = new List<AudioPoolItem>();
    private Dictionary<uint, AudioPoolItem> _activePool = new Dictionary<uint, AudioPoolItem>();
    private uint _idGiver;
    private Transform _listenerTransform;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        FillTrackInfo();

        FillPool();
    }

    private void FillTrackInfo()
    {
        var groups = _audioMixer.FindMatchingGroups(string.Empty);

        foreach (var group in groups)
        {
            var trackInfo = new TrackInfo();
            trackInfo.name = group.name;
            trackInfo.@group = group;
            trackInfo.trackFader = null;
            _tracks[group.name] = trackInfo;
        }
    }

    private void FillPool()
    {
        for (var i = 0; i < _maxSounds; i++)
        {
            var go = new GameObject("Pool Item");
            var audioSource = go.AddComponent<AudioSource>();
            go.transform.parent = transform;

            var poolItem = new AudioPoolItem();
            poolItem.go = go;
            poolItem.audioSource = audioSource;
            poolItem.isPlaying = false;
            poolItem.audioSource.rolloffMode = rolloffMode;
            poolItem.audioSource.maxDistance = _maxSoundDistance;
            poolItem.audioSource.minDistance = _minSoundDistance;

            go.SetActive(false);

            _pool.Add(poolItem);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _listenerTransform = GameAssets.Instance.listenerTransform;
    }

    private IEnumerator SetTrackVolumeInternal(string track, float volume, float fadeTime)
    {
        var startVolume = 0f;

        _audioMixer.GetFloat(track, out startVolume);

        for (float i = 0; i < 1; i += Time.deltaTime / fadeTime)
        {
            _audioMixer.SetFloat(track, Mathf.Lerp(startVolume, volume, i));

            yield return null;
        }

        _audioMixer.SetFloat(track, volume);
    }

    private uint ConfigurePoolObject(int poolIndex, string track, AudioClip clip, Vector3 position,
        float volume, float spatialBlend, float unimportance)
    {
        if (poolIndex < 0 || poolIndex >= _pool.Count) return 0;

        var poolItem = _pool[poolIndex];

        _idGiver++;

        var source = poolItem.audioSource;
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = spatialBlend;
        source.outputAudioMixerGroup = _tracks[track].@group;
        source.transform.position = position;

        poolItem.isPlaying = true;
        poolItem.unimportance = unimportance;
        poolItem.ID = _idGiver;
        poolItem.go.SetActive(true);
        poolItem.audioSource.Play();

        poolItem.coroutine = StopSoundDelayed(_idGiver, source.clip.length);
        StartCoroutine(poolItem.coroutine);

        _activePool[_idGiver] = poolItem;

        return _idGiver;
    }

    private IEnumerator StopSoundDelayed(uint id, float duration)
    {
        yield return new WaitForSeconds(duration);

        AudioPoolItem activeSound;

        if (_activePool.TryGetValue(id, out activeSound)) KillAudioPoolItem(activeSound, id);
    }

    private void KillAudioPoolItem(AudioPoolItem activeSound, uint id)
    {
        activeSound.audioSource.Stop();
        activeSound.audioSource.clip = null;
        activeSound.audioSource.gameObject.SetActive(false);
        activeSound.isPlaying = false;
        _activePool.Remove(id);
    }

    public void StopOneShootSound(uint id)
    {
        AudioPoolItem activeSound;

        if (_activePool.TryGetValue(id, out activeSound))
        {
            StopCoroutine(activeSound.coroutine);

            KillAudioPoolItem(activeSound, id);
        }
    }

    public uint PlayOneShootSound(string track, AudioClip clip, Vector3 position, float volume,
        float spatialBlend, int priority = 128)
    {
        if (CanPlayAudio(position) == false || _tracks.ContainsKey(track) == false ||
            clip == null || volume.Equals(0f)) return 0;

        var unimportance = (_listenerTransform.position - position).sqrMagnitude /
                           Mathf.Max(1, priority);

        var leastImportantIndex = -1;
        var leastImportanceValue = float.MaxValue;

        for (var i = 0; i < _pool.Count; i++)
        {
            var poolItem = _pool[i];

            if (poolItem.isPlaying == false)
            {
                return ConfigurePoolObject(i, track, clip, position, volume, spatialBlend, unimportance);
            }
            else if (poolItem.unimportance > leastImportanceValue)
            {
                leastImportanceValue = poolItem.unimportance;
                leastImportantIndex = i;
            }
        }

        if (leastImportanceValue > unimportance)
            return ConfigurePoolObject(leastImportantIndex, track, clip, position,
                volume, spatialBlend, unimportance);

        return 0;
    }

    private bool CanPlayAudio(Vector3 spawnPosition)
    {
        if (_listenerTransform == null)
            return false;
        
        return Vector3.Distance(spawnPosition, _listenerTransform.position) < _maxSoundDistance;
    }
}