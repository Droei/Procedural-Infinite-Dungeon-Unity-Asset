using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    private Dictionary<Vector2Int, Room> rooms = new Dictionary<Vector2Int, Room>();
    private static readonly Dictionary<DirectionEnum, Vector2Int> directionOffsets = new()
    {
        { DirectionEnum.North, Vector2Int.up },
        { DirectionEnum.South, Vector2Int.down },
        { DirectionEnum.East,  Vector2Int.right },
        { DirectionEnum.West,  Vector2Int.left }
    };


    private DungeonManager dungeonManager;
    private int waveCount = 0;

    public Dungeon(DungeonManager dungeonManager)
    {
        this.dungeonManager = dungeonManager;
    }

    public DungeonManager GetDungeonManager => dungeonManager;

    public int IncrementWaveCount()
    {
        return waveCount++;
    }
    public int GetWaveCount => waveCount;

    public void AddRoom(Room room)
    {
        rooms[room.GetGridPosition] = room;
    }

    public Room GetRoom(int x, int y)
    {
        rooms.TryGetValue(new Vector2Int(x, y), out Room room);
        return room;
    }

    public bool RoomExists(Vector2Int pos)
    {
        return rooms.ContainsKey(pos);
    }

    public Dictionary<DirectionEnum, Room> GetNeighbors(Room room)
    {
        var neighbors = new Dictionary<DirectionEnum, Room>();
        Vector2Int pos = room.GetGridPosition;

        foreach (var pair in directionOffsets)
        {
            Vector2Int checkPos = pos + pair.Value;

            if (rooms.TryGetValue(checkPos, out Room neighbor))
                neighbors.Add(pair.Key, neighbor);
        }

        return neighbors;
    }
    public Dictionary<DirectionEnum, Vector2Int> GetNeighborFreeSpaces(Room room)
    {
        var freeSpaces = new Dictionary<DirectionEnum, Vector2Int>();
        Vector2Int pos = room.GetGridPosition;

        foreach (var pair in directionOffsets)
        {
            Vector2Int checkPos = pos + pair.Value;

            if (!rooms.ContainsKey(checkPos))
                freeSpaces.Add(pair.Key, checkPos);
        }

        return freeSpaces;
    }
}
