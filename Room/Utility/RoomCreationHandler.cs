
using UnityEngine;

public static class RoomCreationHandler
{

    public static Room CreateRoom(Vector2Int gridPos, DungeonSettingsData dSD, RoomSpawnData roomSpawnData, EnemySpawnFactory enemySpawnFactory)
    {
        Vector3 worldPos = new(gridPos.x * dSD.GetRoomSize, 0, gridPos.y * dSD.GetRoomSize);
        GameObject roomObject = Object.Instantiate(roomSpawnData.RoomObject.gameObject, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({dSD.GetDungeon.IncrementWaveCount()})";

        Room room = new(gridPos.x, gridPos.y, roomObject);

        dSD.GetDungeon.AddRoom(room);
        RoomViewHandler.InitView(room, dSD.GetDungeon.GetDungeonManager);

        RoomEnemyHandler.SpawnEnemies(room, enemySpawnFactory, dSD);


        return room;
    }
}
