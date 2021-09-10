using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon type")] [SerializeField]
    private Weapons _weapon;

    [Header("Weapon")] [SerializeField] private GameObject[] _weapnos;

    private void Start()
    {
        SetWeapon(_weapon);
    }

    private void EnableWeapons()
    {
        foreach (var weapon in _weapnos)
        {
            weapon.SetActive(true);
        }
    }
    
    public void SetWeapon(Weapons weapon)
    {
        EnableWeapons();
        
        Messenger<Weapons>.Broadcast(GameEvent.SET_WEAPON, _weapon);
    }

    public void StartShooting()
    {
        Messenger.Broadcast(GameEvent.START_SHOOTING);
    }

    public void StopShooting()
    {
        Messenger.Broadcast(GameEvent.STOP_SHOOTING);
    }
}