using UnityEngine;

public static class SpawnPositionGenerator
{
    public static Vector3 GetRandomPosition(GameObject prefab, float baseY, (float minX, float maxX, float minZ, float maxZ) bounds)
    {
        float x = RandomService.Range(bounds.minX, bounds.maxX);
        float z = RandomService.Range(bounds.minZ, bounds.maxZ);
        float y = GetSpawnY(prefab, baseY);
        return new Vector3(x, y, z);
    }

    public static Vector3 GetCenteredPosition(GameObject prefab, (float minX, float maxX, float minZ, float maxZ) bounds, float baseY)
    {
        float centerX = (bounds.minX + bounds.maxX) / 2f;
        float centerZ = (bounds.minZ + bounds.maxZ) / 2f;
        float y = GetSpawnY(prefab, baseY);
        return new Vector3(centerX, y, centerZ);
    }


    private static float GetSpawnY(GameObject prefab, float baseY)
    {
        if (prefab == null) return baseY;
        var col = prefab.GetComponent<Collider>();
        if (col != null) return baseY + col.bounds.extents.y;
        return baseY;
    }
}
