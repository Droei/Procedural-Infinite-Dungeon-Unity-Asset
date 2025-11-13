using UnityEngine;

public static class SpawnAreaCalculator
{
    public static (float minX, float maxX, float minZ, float maxZ) Calculate(Vector3 roomPos, float roomSize)
    {
        const float inset = 1f;
        return (
            roomPos.x + inset,
            roomPos.x + roomSize - inset,
            roomPos.z + inset,
            roomPos.z + roomSize - inset
        );
    }
}
