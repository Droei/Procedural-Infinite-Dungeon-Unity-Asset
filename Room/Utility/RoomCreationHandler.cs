
using UnityEngine;

public class RoomCreationHandler
{
    DungeonSettingsData dSD;
    RoomSpawnData roomSpawnData;
    EnemySpawnFactory enemySpawnFactory;
    public RoomCreationHandler(DungeonSettingsData dSD, EnemySpawnFactory enemySpawnFactory)
    {
        this.dSD = dSD;
        this.enemySpawnFactory = enemySpawnFactory;
    }

    public void SetRoomSpawnData(RoomSpawnData roomSpawnData)
    {
        this.roomSpawnData = roomSpawnData;
    }

    public Room CreateRoom(Vector2Int gridPos)
    {
        return CreateRoom(gridPos, null);
    }

    public Room CreateRoom(Vector2Int gridPos, Room parent)
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
