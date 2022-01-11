using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoBonusItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private AmmoBonusItemData _data;

    public static Action onApply;

    private PlayerWeaponControl _playerWeaponControl;

    private void Start()
    {
        _playerWeaponControl = GameAssets.Instance.playerWeaponControl;
    }

    private void OnCollisionEnter2D(Collision2D player)
    {
        if (_data.playerLayerMask.ContainsLayer(player.gameObject.layer) == false ||
            _playerWeaponControl.transform.parent.gameObject.activeSelf == false) return;

        onApply?.Invoke();

        Debug.Log("Took Ammo");

        AssignAmmo(_playerWeaponControl.currentWeapon);

        gameObject.SetActive(false);
    }

    private void AssignAmmo(Weapon currentWeapon)
    {
        foreach (var preference in _data.bonusPreferences)
            if (currentWeapon.weaponType == preference.weaponType)
                currentWeapon.playerAmmo.SetAmmoWithTextUpdate(currentWeapon.playerAmmo.Ammo +
                                                               Random.Range(preference.minAmmo, preference.maxAmmo));
    }
}