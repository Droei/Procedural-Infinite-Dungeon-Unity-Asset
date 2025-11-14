using System.Collections.Generic;

public class SidesGenerator
{
    readonly Dungeon dungeon;
    public SidesGenerator(Dungeon dungeon)
    {
        this.dungeon = dungeon;
    }
    public void GenerateSides(Room room)
    {
        HandleNeighbors(room);
        HandleFreeSpaces(room);
    }

    private void HandleFreeSpaces(Room room)
    {
        var freeSpaces = dungeon.GetNeighborFreeSpaces(room);

        foreach (var kvp in freeSpaces)
        {
            DirectionEnum dir = kvp.Key;
            SidesRandomiser.PlaceDoorOrWallInRoom(room, dir);
        }
    }

    private void HandleNeighbors(Room room)
    {
        Dictionary<DirectionEnum, Room> neighbors = dungeon.GetNeighbors(room);

        foreach (var kvp in neighbors)
        {
            DirectionEnum dir = kvp.Key;
            Room neighbor = kvp.Value;
            SidesSyncer.SyncSidesWithNeighbor(room, neighbor, dir);
        }
    }
}
