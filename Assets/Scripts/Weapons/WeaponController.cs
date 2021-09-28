using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapons _weaponToSelect;

    [System.Serializable]
    public struct weapon
    {
        public Weapons weaponType;
        public GameObject weaponObject;
    }

    [SerializeField] private weapon[] _weapons;

    public static readonly int defaultBulletDamage = 10;

    private void OnValidate()
    {
        SetWeapon(_weaponToSelect);
    }

    public void SetWeapon(Weapons weaponToSelect)
    {
        foreach (var weapon in _weapons)
        {
            weapon.weaponObject.SetActive(weapon.weaponType == weaponToSelect);
        }
    }

    public void StartShooting()
    {
        if (CanShoot() == false)
        {
            return;
        }
        
        Messenger.Broadcast(GameEvent.START_SHOOTING);

        Messenger.Broadcast(GameEvent.PLAYED_AUDIO_SOURCE, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    public void StopShooting()
    {
        if (CanShoot() == false)
        {
            return;
        }
        
        Messenger.Broadcast(GameEvent.STOP_SHOOTING);
    }

    private bool CanShoot()
    {
        return gameObject.transform.parent.gameObject.activeSelf == true;
    }

}