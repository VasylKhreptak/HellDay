using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions
{
    public static bool TryGetSurfaceType(this Tilemap tilemap, out SurfaceTypes? surfaceType, Vector3 position)
    {
        var surfaceTypeTile = tilemap.GetTile<SurfaceTypeTile>(tilemap.WorldToCell(position));

        if (surfaceTypeTile == null) surfaceType = null;
        else surfaceType = surfaceTypeTile.surfaceType;

        return surfaceTypeTile != null;
    }
}
