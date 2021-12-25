using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthBonusItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private HealthBonusItemData _data;

    private Player _player;
    private ObjectPooler _objectPooler;
    private float _healthValue;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        _healthValue = Random.Range(_data.MINHealth, _data.MAXHealth);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.playerLayerMask.ContainsLayer(other.gameObject.layer) == false) return;

        if (_player == null) other.gameObject.TryGetComponent(out _player);

        if (_player && _player.gameObject.activeSelf &&
            Mathf.Approximately(_player.Health, _player.MAXHealth) == false)
        {
            _player.SetHealth(_player.Health + _healthValue);

            SpawnHealEffects(_player);

            gameObject.SetActive(false);
        }
    }

    private void SpawnHealEffects(Player player)
    {
        _objectPooler.GetFromPool(_data.applyEffect,
            player.transform.position, Quaternion.identity);

        var healthPopup = _objectPooler.GetFromPool(_data.healthPopup, player.transform.position,
            Quaternion.identity);

        if (healthPopup.TryGetComponent(out DamagePopup popup)) popup.Init("+" + ((int)_healthValue).ToString(), _data.popupColor);
    }
}
