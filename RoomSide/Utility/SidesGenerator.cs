using System.Collections.Generic;

public class SidesGenerator
{
    DungeonSettingsData dSD;
    bool startRoom;

    public SidesGenerator(DungeonSettingsData dSD)
    {
        this.dSD = dSD;
    }

    public void GenerateSides(Room room, bool startRoom = false)
    {
        this.startRoom = startRoom;
        HandleNeighbors(room);
        HandleFreeSpaces(room);
    }

    private void HandleFreeSpaces(Room room)
    {
        var freeSpaces = dSD.GetDungeon.GetNeighborFreeSpaces(room);

        foreach (var kvp in freeSpaces)
        {
            DirectionEnum dir = kvp.Key;
            SidesRandomiser.PlaceDoorOrWallInRoom(room, dir, startRoom ? 1f : dSD.GetDoorSpawnChance);
        }
    }

    private void HandleNeighbors(Room room)
    {
        Dictionary<DirectionEnum, Room> neighbors = dSD.GetDungeon.GetNeighbors(room);

        foreach (var kvp in neighbors)
        {
            DirectionEnum dir = kvp.Key;
            Room neighbor = kvp.Value;
            SidesSyncer.SyncSidesWithNeighbor(room, neighbor, dir);
        }
    }
}
