using UnityEngine;

public static class SpawnAreaCalculator
{
    public static RoomBounds Calculate(Vector3 roomPos, float roomSize)
    {
        float minX = roomPos.x;
        float maxX = roomPos.x + roomSize;
        float minZ = roomPos.z;
        float maxZ = roomPos.z + roomSize;

        RoomBounds b = new RoomBounds(minX, maxX, minZ, maxZ);
        Debug.Log(b.ToString() );
        return b;
    }



}
