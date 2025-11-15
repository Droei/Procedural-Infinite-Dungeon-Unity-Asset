
using UnityEngine;

public static class RoomCreationHandler
{



    public static Room CreateRoom(Vector2Int gridPos, DungeonSettingsData dSD, RoomSpawnData roomSpawnData, EnemySpawnFactory enemySpawnFactory)
    {
        return CreateRoom(gridPos, dSD, roomSpawnData, enemySpawnFactory, null);
    }

    public static Room CreateRoom(Vector2Int gridPos, DungeonSettingsData dSD, RoomSpawnData roomSpawnData, EnemySpawnFactory enemySpawnFactory, Room parent)
    {
        Vector3 worldPos = new(gridPos.x * dSD.GetRoomSize, 0, gridPos.y * dSD.GetRoomSize);
        GameObject roomObject = Object.Instantiate(roomSpawnData.RoomObject.gameObject, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({dSD.GetDungeon.IncrementWaveCount()})";

        Room room = new(gridPos.x, gridPos.y, roomObject);

        parent?.AddChild(room);

        dSD.GetDungeon.AddRoom(room);

        RoomViewHandler.InitView(room, dSD, roomSpawnData);
        RoomEnemyHandler.SpawnEnemies(room, enemySpawnFactory, dSD);


        return room;
    }
}
