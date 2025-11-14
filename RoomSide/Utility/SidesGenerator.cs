using System.Collections.Generic;
using UnityEngine;

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
            Vector2Int EmptyLocation = kvp.Value;
            Debug.Log($"No neighbor to the {dir}: {EmptyLocation}");
        }
    }

    private void HandleNeighbors(Room room)
    {
        Dictionary<DirectionEnum, Room> neighbors = dungeon.GetNeighbors(room);

        foreach (var kvp in neighbors)
        {
            DirectionEnum dir = kvp.Key;
            Room neighbor = kvp.Value;

            Debug.Log($"Neighbor to the {dir}: {neighbor.GetRoomGameObject.name}");
        }
    }
}
