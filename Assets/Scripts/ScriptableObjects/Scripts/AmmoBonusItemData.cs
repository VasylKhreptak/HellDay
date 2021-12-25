using UnityEngine;

[CreateAssetMenu(fileName = "AmmoBonusItemData", menuName = "ScriptableObjects/AmmoBonusItemData")]
public class AmmoBonusItemData : BonusItemData
{
    [System.Serializable]
    public class BonusPreference
    {
        public Weapons weaponType;
        public int minAmmo;
        public int maxAmmo;
    }

    public BonusPreference[] bonusPreferences;

}
