using UnityEngine;
using Random = UnityEngine.Random;

public class HealthBonusItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private HealthBonusItemData _data;

    private Player _player;
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private  void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.playerLayerMask.ContainsLayer(other.gameObject.layer) == false) return;
        
        if (_player == null)
        {
            other.gameObject.TryGetComponent(out _player);
        }

        if (_player && _player.gameObject.activeSelf && 
            Mathf.Approximately(_player.Health, _player.data.MAXHealth) == false)
        {
            this._player.SetHealth(_player.Health + Random.Range(_data.MINHealth, _data.MAXHealth));

            SpawnHealthSpellEffect(_player);
            
            gameObject.SetActive(false);
        }
    }

    private void SpawnHealthSpellEffect(Player player)
    {
        GameObject healSpell = _objectPooler.GetFromPool(_data.applyEffect, 
            player.transform.position, Quaternion.identity);

        healSpell.transform.parent = player.transform;
    }
}
