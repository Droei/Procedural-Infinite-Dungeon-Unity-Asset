using System.Collections.Generic;
using UnityEngine;

public static class SpawnPositionGenerator
{
    public static (Vector3 pos, RoomBounds bounds) GetRoomBounds(Room room, DungeonSettingsData dSD)
    {
        Vector3 pos = room.GetRoomGameObject.transform.position;
        RoomBounds bounds = SpawnAreaCalculator.Calculate(pos, dSD.RoomSize);
        return (pos, bounds);
    }

    public static (Vector3 pos, RoomBounds bounds)[] GetRoomAndChildBounds(Room room, DungeonSettingsData dSD)
    {
        List<(Vector3 pos, RoomBounds bounds)> result = new();

        result.Add(GetRoomBounds(room, dSD));

        foreach (Room child in room.GetChildRooms)
        {
            result.Add(GetRoomBounds(child, dSD));
        }

        return result.ToArray();
    }

    public static Vector3 GetRandomPosition(float baseY, RoomBounds bounds, float margin)
        => bounds.RandomSpawnPosition(baseY, margin);

    public static Vector3 GetCenteredPosition(float baseY, RoomBounds bounds)
        => bounds.Center(baseY);
}
