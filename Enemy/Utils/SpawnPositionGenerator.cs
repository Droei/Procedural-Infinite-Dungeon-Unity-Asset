using System.Collections.Generic;
using UnityEngine;

public static class SpawnPositionGenerator
{
    public static (Vector3 pos, RoomBoundsHelper bounds) GetRoomBounds(Room room, DungeonSettingsData dSD)
    {
        Vector3 pos = room.GetRoomGameObject.transform.position;
        RoomBoundsHelper bounds = SpawnAreaCalculator.Calculate(pos, dSD.RoomSize);
        return (pos, bounds);
    }

    public static (Vector3 pos, RoomBoundsHelper bounds)[] GetRoomAndChildBounds(Room room, DungeonSettingsData dSD)
    {
        List<(Vector3 pos, RoomBoundsHelper bounds)> result = new();

        result.Add(GetRoomBounds(room, dSD));

        foreach (Room child in room.GetNonLootRooms)
        {
            result.Add(GetRoomBounds(child, dSD));
        }

        return result.ToArray();
    }

    public static Vector3 GetRandomPosition(float baseY, RoomBoundsHelper bounds, float margin)
        => bounds.RandomSpawnPosition(baseY, margin);

    public static Vector3 GetCenteredPosition(float baseY, RoomBoundsHelper bounds)
        => bounds.Center(baseY);
}
