
using UnityEngine;

public static class RoomCreationHandler
{

    public static Room CreateRoom(Vector2Int gridPos, float roomSize, RoomSpawnData spawnData, ref int roomCount, DungeonManager dungeonManager, Dungeon dungeon)
    {
        Vector3 worldPos = new(gridPos.x * roomSize, 0, gridPos.y * roomSize);
        GameObject roomObject = Object.Instantiate(spawnData.RoomObject.gameObject, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({roomCount++})";

        Room room = new(gridPos.x, gridPos.y, roomObject);

        dungeon.AddRoom(room);
        RoomViewHandler.InitView(room, dungeonManager);

        return room;
    }
}
