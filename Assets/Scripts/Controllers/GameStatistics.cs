using System;
using System.Collections;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameStatisticsData _data;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(PlayTimeCounterRoutine());
    }

    private void OnEnable()
    {
        Player.onDie += () => { _data.deaths++; };
        Zombie.onDeath += () => { _data.killedZombies++; };
        FuelBarrel.onExplode += () => { _data.explodedFuelBarrels++; };
        Animal.onDeath += () => { _data.killedAnimals++; };
        PhysicalObject.onDestroy += () => { _data.destroyedPhysicalObjects++; };
        WeaponCore.onShoot += () => { _data.totalUsedAmmo++; };
        WeaponBonusItem.onTook += () => { _data.changedWeapons++; };
        HealthBonusItem.onApply += () => { _data.appliedBandages++; };
        AmmoBonusItem.onApply += () => { _data.appliedAmmoBonuses++; };
    }

    private void OnDisable()
    {
        Player.onDie -= () => { _data.deaths++; };
        Zombie.onDeath -= () => { _data.killedZombies++; };
        FuelBarrel.onExplode -= () => { _data.explodedFuelBarrels++; };
        Animal.onDeath -= () => { _data.killedAnimals++; };
        PhysicalObject.onDestroy -= () => { _data.destroyedPhysicalObjects++; };
        WeaponCore.onShoot -= () => { _data.totalUsedAmmo++; };
        WeaponBonusItem.onTook -= () => { _data.changedWeapons++; };
        HealthBonusItem.onApply -= () => { _data.appliedBandages++; };
        AmmoBonusItem.onApply += () => { _data.appliedAmmoBonuses++; };
    }

    private IEnumerator PlayTimeCounterRoutine()
    {
        while (true)
        {
            _data.playTime += 1;

            yield return new WaitForSeconds(1f);
        }
    }
}