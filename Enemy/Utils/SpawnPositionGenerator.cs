using System.Collections.Generic;
using UnityEngine;

public static class SpawnPositionGenerator
{
    public static (Vector3 pos, RoomBounds bounds)[] GetRoomAndChildBounds(Room room, DungeonSettingsData dSD)
    {
        List<(Vector3, RoomBounds)> result = new();

        Vector3 parentPos = room.GetRoomGameObject.transform.position;
        var parentBounds = SpawnAreaCalculator.Calculate(parentPos, dSD.RoomSize);
        result.Add((parentPos, parentBounds));

        foreach (Room child in room.GetChildRooms)
        {
            Vector3 childPos = child.GetRoomGameObject.transform.position;
            var childBounds = SpawnAreaCalculator.Calculate(childPos, dSD.RoomSize);
            result.Add((childPos, childBounds));
        }

        return result.ToArray();
    }

    public static Vector3 GetRandomPosition(float baseY, RoomBounds bounds, float margin)
        => bounds.RandomSpawnPosition(baseY, margin);

    public static Vector3 GetCenteredPosition(float baseY, RoomBounds bounds)
        => bounds.Center(baseY);
}
