using UnityEngine;

public static class SpawnAreaCalculator
{
    public static RoomBounds Calculate(Vector3 roomPos, float roomSize)
    {
        const float inset = 1f;
        return new RoomBounds(
            roomPos.x + inset,
            roomPos.x + roomSize - inset,
            roomPos.z + inset,
            roomPos.z + roomSize - inset
        );
    }
}
