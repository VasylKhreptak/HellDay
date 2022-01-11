using UnityEngine;

[CreateAssetMenu(fileName = "GameStatisticsData", menuName = "ScriptableObjects/GameStatisticsData")]
public class GameStatisticsData : ScriptableObject
{
    public ulong playTime;
    public int killedZombies;
    public int deaths;
    public int explodedFuelBarrels;
    public int killedAnimals;
    public int destroyedPhysicalObjects;
    public int totalUsedAmmo;
    public int changedWeapons;
    public int appliedBandages;
    public int appliedAmmoBonuses;
}