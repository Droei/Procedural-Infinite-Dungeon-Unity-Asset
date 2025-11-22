using UnityEngine;

public static class SpawnAreaCalculator
{
    public static RoomBoundsHelper Calculate(Vector3 roomPos, float roomSize)
    {
        float minX = roomPos.x;
        float maxX = roomPos.x + roomSize;
        float minZ = roomPos.z;
        float maxZ = roomPos.z + roomSize;

        RoomBoundsHelper b = new RoomBoundsHelper(minX, maxX, minZ, maxZ);
        return b;
    }
}
