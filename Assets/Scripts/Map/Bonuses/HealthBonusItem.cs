using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthBonusItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnCollisionWithPlayerEvent _onCollisionWithPlayerEvent;

    [Header("Data")]
    [SerializeField] private HealthBonusItemData _data;

    private Player _player;
    private ObjectPooler _objectPooler;
    private float _healthValue;

    public static Action onApply;

    private bool _canApply = true;

    private Tween _waitTween;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        _healthValue = Random.Range(_data.MINHealth, _data.MAXHealth);

        _onCollisionWithPlayerEvent.onCollision += ProcessCollisionWithPlayer;
    }

    private void OnDisable()
    {
        _onCollisionWithPlayerEvent.onCollision -= ProcessCollisionWithPlayer;
    }

    private void ProcessCollisionWithPlayer(Collision2D col)
    {
        if (_canApply == false)
            return;

        col.gameObject.TryGetComponent(out _player);

        if (_player &&
            _player.gameObject.activeSelf &&
            Mathf.Approximately(_player.Health, _player.MAXHealth) == false)
        {
            onApply?.Invoke();

            _player.SetHealth(_player.Health + _healthValue);

            SpawnHealEffects(_player);

            ControlApplySpeed();

            gameObject.SetActive(false);
        }
    }

    private void ControlApplySpeed()
    {
        _canApply = false;
        _waitTween.Kill();
        _waitTween = this.DOWait(_data.ApplyDelay).OnComplete(() => { _canApply = true; });
    }

    private void SpawnHealEffects(Player player)
    {
        _objectPooler.GetFromPool(_data.applyEffect,
            player.transform.position, Quaternion.identity);

        var healthPopup = _objectPooler.GetFromPool(_data.healthPopup, player.transform.position,
            Quaternion.identity);

        if (healthPopup.TryGetComponent(out DamagePopup popup))
            popup.Init("+" + ((int)_healthValue).ToString(), _data.popupColor);
    }
}