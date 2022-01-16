using UnityEngine;

[CreateAssetMenu(fileName = "AmmoBonusItemData", menuName = "ScriptableObjects/AmmoBonusItemData")]
public class AmmoBonusItemData : ScriptableObject
{
    [System.Serializable]
    public class BonusPreference
    {
        public Weapons weaponType;
        public int minAmmo;
        public int maxAmmo;
    }

    public BonusPreference[] bonusPreferences;
    
    [Header("Preferences")]
    [SerializeField] private float _applyDelay = 1f;

    public float ApplyDelay => _applyDelay;
}