
using UnityEngine;

public static class RoomCreationHandler
{

    public static Room CreateRoom(Vector2Int gridPos, DungeonSettingsData dSD)
    {
        Vector3 worldPos = new(gridPos.x * dSD.GetRoomSize, 0, gridPos.y * dSD.GetRoomSize);
        GameObject roomObject = Object.Instantiate(dSD.GetRandomRoomSpawnData.RoomObject.gameObject, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({dSD.GetDungeon.IncrementWaveCount()})";

        Room room = new(gridPos.x, gridPos.y, roomObject);

        dSD.GetDungeon.AddRoom(room);
        RoomViewHandler.InitView(room, dSD.GetDungeon.GetDungeonManager);

        return room;
    }
}
