using UnityEngine;
using Random = UnityEngine.Random;

public class HealthBonusItem : BonusItemCore
{
    [Header("Preferences")] 
    [SerializeField] private float _minHealth = 10f;
    [SerializeField] private float _maxHealth = 40f;

    private Player _player;

    protected override void OnCollisionWithPlayer(Collision2D player)
    {
        if (_player == null)
        {
            player.gameObject.TryGetComponent(out _player);
        }

        if (_player && _player.gameObject.activeSelf)
        {
            _player.SetHealth(_player.Health + Random.Range(_minHealth, _maxHealth));
        }

        gameObject.SetActive(false);
    }
}
