using UnityEngine;

public class RoomCreationHandler
{
    private readonly DungeonSettingsData dSD;
    private RoomSpawnData roomSpawnData;

    public RoomCreationHandler(DungeonSettingsData dSD)
    {
        this.dSD = dSD;
    }

    public void SetRoomSpawnData(RoomSpawnData spawnData)
    {
        roomSpawnData = spawnData;
    }

    public Room CreateRoom(Vector2Int gridPos)
    {
        return CreateRoom(gridPos, null);
    }

    public Room CreateRoom(Vector2Int gridPos, Room parent)
    {
        GameObject roomObject = InstantiateRoomObject(gridPos);
        Room room = InitializeRoom(gridPos, roomObject);

        parent?.AddChild(room);
        dSD.Dungeon.AddRoom(room);

        RoomViewHandler.InitView(room, dSD, roomSpawnData);

        return room;
    }

    private GameObject InstantiateRoomObject(Vector2Int gridPos)
    {
        Vector3 worldPos = new Vector3(
            gridPos.x * dSD.RoomSize,
            0,
            gridPos.y * dSD.RoomSize
        );

        GameObject prefab = roomSpawnData.RoomObject.gameObject;
        GameObject roomObject = Object.Instantiate(prefab, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({dSD.Dungeon.IncrementWaveCount})";

        return roomObject;
    }

    private Room InitializeRoom(Vector2Int gridPos, GameObject roomObject)
    {
        return new Room(gridPos.x, gridPos.y, roomObject);
    }

    public void AddLootChest(Room room)
    {
        var (pos, bounds) = SpawnPositionGenerator.GetRoomBounds(room, dSD);
        Vector3 spawnPos = SpawnPositionGenerator.GetCenteredPosition(pos.y, bounds);

        Object.Instantiate(roomSpawnData.SpecificLootChest ? roomSpawnData.SpecificLootChest : dSD.GeneralLootChest, spawnPos, Quaternion.identity);

        room.SetAsLootRoom();
    }
}
