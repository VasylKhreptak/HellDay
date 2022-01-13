using UnityEngine;
using UnityEngine.Tilemaps;

public class GameAssets : MonoBehaviour
{
    #region singleton

    public static GameAssets Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public Transform playerTransfrom;
    public Tilemap mainTilemap;
    public Transform listenerTransform;
    public PlayerWeaponControl playerWeaponControl;
}