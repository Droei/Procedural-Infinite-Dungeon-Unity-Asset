
using UnityEngine;

public class RoomCreationHandler
{
    DungeonSettingsData dSD;
    RoomSpawnData roomSpawnData;
    public RoomCreationHandler(DungeonSettingsData dSD)
    {
        this.dSD = dSD;
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
        Vector3 worldPos = new(gridPos.x * dSD.RoomSize, 0, gridPos.y * dSD.RoomSize);
        GameObject roomObject = Object.Instantiate(roomSpawnData.RoomObject.gameObject, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({dSD.Dungeon.IncrementWaveCount})";

        Room room = new(gridPos.x, gridPos.y, roomObject);

        parent?.AddChild(room);

        dSD.Dungeon.AddRoom(room);

        RoomViewHandler.InitView(room, dSD, roomSpawnData);
        return room;
    }
}
