using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatisticObserver : MonoBehaviour
{
    [Serializable]
    public class Statistic
    {
        [SerializeField] private ulong _playTime;
        [SerializeField] private int _killedZombies;
        [SerializeField] private int _deaths;
        [SerializeField] private int _explodedFuelBarrels;
        [SerializeField] private int _killedAnimals;
        [SerializeField] private int _destroyedPhysicalObjects;
        [SerializeField] private int _totalUsedAmmo;
        [SerializeField] private int _changedWeapons;
        [SerializeField] private int _appliedBandages;
        [SerializeField] private int _appliedAmmoBonuses;
        
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

    public static GameStatisticObserver Instance;

    public Statistic statistic = new Statistic();

    private const string KEY = "Statistic";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        statistic = GameDataProvider.Load<Statistic>(KEY, new Statistic());
    }

    private void Start()
    {
        StartCoroutine(PlayTimeCounterRoutine());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AddListeners;
        SceneManager.sceneUnloaded += RemoveListeners;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AddListeners;
        SceneManager.sceneUnloaded -= RemoveListeners;
        
        GameDataProvider.Save(KEY, statistic);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            GameDataProvider.Save(KEY, statistic);
        }
    }

    private void AddListeners(Scene scene, LoadSceneMode mode)
    {
        Player.onDie += statistic.IncPlayerDeaths;
        Zombie.onDeath += statistic.IncKilledZombies;
        FuelBarrel.onExplode += statistic.IncExplodedBarrels;
        Animal.onDeath += statistic.IncKilledAnimals;
        PhysicalObject.onDestroy += statistic.IncDestroyedPhysicalObjects;
        WeaponCore.onShoot += statistic.IncUsedAmmo;
        WeaponBonusItem.onTook += statistic.IncChangedWeapons;
        HealthBonusItem.onApply += statistic.IncAppliedBandages;
        AmmoBonusItem.onApply += statistic.IncAppliedAmmoBonuses;
    }

    private void RemoveListeners(Scene scene)
    {
        Player.onDie -= statistic.IncPlayerDeaths;
        Zombie.onDeath -= statistic.IncKilledZombies;
        FuelBarrel.onExplode -= statistic.IncExplodedBarrels;
        Animal.onDeath -= statistic.IncKilledAnimals;
        PhysicalObject.onDestroy -= statistic.IncDestroyedPhysicalObjects;
        WeaponCore.onShoot -= statistic.IncUsedAmmo;
        WeaponBonusItem.onTook -= statistic.IncChangedWeapons;
        HealthBonusItem.onApply -= statistic.IncAppliedBandages;
        AmmoBonusItem.onApply -= statistic.IncAppliedAmmoBonuses;
    }

    private IEnumerator PlayTimeCounterRoutine()
    {
        while (true)
        {
            statistic.IncPlayTime();

            yield return new WaitForSeconds(1f);
        }
    }
}