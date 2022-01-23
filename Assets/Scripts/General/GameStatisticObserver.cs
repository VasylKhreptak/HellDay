using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatisticObserver : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameStatisticData _data;

    private static GameStatisticObserver Instance;

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
    }

    private void AddListeners(Scene scene, LoadSceneMode mode)
    {
        Player.onDie += _data.IncPlayerDeaths;
        Zombie.onDeath += _data.IncKilledZombies;
        FuelBarrel.onExplode += _data.IncExplodedBarrels;
        Animal.onDeath += _data.IncKilledAnimals;
        PhysicalObject.onDestroy += _data.IncDestroyedPhysicalObjects;
        WeaponCore.onShoot += _data.IncUsedAmmo;
        WeaponBonusItem.onTook += _data.IncChangedWeapons;
        HealthBonusItem.onApply += _data.IncAppliedBandages;
        AmmoBonusItem.onApply += _data.IncAppliedAmmoBonuses;
    }

    private void RemoveListeners(Scene scene)
    {
        Player.onDie -= _data.IncPlayerDeaths;
        Zombie.onDeath -= _data.IncKilledZombies;
        FuelBarrel.onExplode -= _data.IncExplodedBarrels;
        Animal.onDeath -= _data.IncKilledAnimals;
        PhysicalObject.onDestroy -= _data.IncDestroyedPhysicalObjects;
        WeaponCore.onShoot -= _data.IncUsedAmmo;
        WeaponBonusItem.onTook -= _data.IncChangedWeapons;
        HealthBonusItem.onApply -= _data.IncAppliedBandages;
        AmmoBonusItem.onApply -= _data.IncAppliedAmmoBonuses;
    }

    private IEnumerator PlayTimeCounterRoutine()
    {
        while (true)
        {
            _data.IncPlayTime();

            yield return new WaitForSeconds(1f);
        }
    }
}