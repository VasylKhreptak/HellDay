using UnityEngine;

[CreateAssetMenu(fileName = "WeaponBonusItemData", menuName = "ScriptableObjects/WeaponBonusItemData")]
public class WeaponBonusItemData : BonusItemData
{
    [SerializeField] private float _swapDelay = 1f;

    public float SwapDelay => _swapDelay;
}
