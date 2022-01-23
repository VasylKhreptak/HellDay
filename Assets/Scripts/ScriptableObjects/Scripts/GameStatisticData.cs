using UnityEngine;

[CreateAssetMenu(fileName = "GameStatisticsData", menuName = "ScriptableObjects/GameStatisticsData")]
public class GameStatisticData : ScriptableObject
{
    private ulong _playTime;
    private int _killedZombies;
    private int _deaths;
    private int _explodedFuelBarrels;
    private int _killedAnimals;
    private int _destroyedPhysicalObjects;
    private int _totalUsedAmmo;
    private int _changedWeapons;
    private int _appliedBandages;
    private int _appliedAmmoBonuses;

    public ulong PlayTime => _playTime;
    public int KilledZombies => _killedZombies;
    public int Deaths => _deaths;
    public int ExplodedFuelBarrels => _explodedFuelBarrels;
    public int KilledAnimals => _killedAnimals;
    public int DestroyedPhysicalObjects => _destroyedPhysicalObjects;
    public int TotalUsedAmmo => _totalUsedAmmo;
    public int ChangedWeapons => _changedWeapons;
    public int AppliedBandages => _appliedBandages;
    public int AppliedAmmoBonuses => _appliedAmmoBonuses;

    public void IncPlayTime() => _playTime++;
    public void IncKilledZombies() => _killedZombies++;
    public void IncPlayerDeaths() => _deaths++;
    public void IncExplodedBarrels() => _explodedFuelBarrels++;
    public void IncKilledAnimals() => _killedAnimals++;
    public void IncDestroyedPhysicalObjects() => _destroyedPhysicalObjects++;
    public void IncUsedAmmo() => _totalUsedAmmo++;
    public void IncChangedWeapons() => _changedWeapons++;
    public void IncAppliedBandages() => _appliedBandages++;
    public void IncAppliedAmmoBonuses() => _appliedAmmoBonuses++;
}