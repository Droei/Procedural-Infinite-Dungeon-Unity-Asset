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

    public List<Vector2Int[]> GetFree2x2Triplets(Room room)
    {
        List<Vector2Int[]> result = new List<Vector2Int[]>();
        Vector2Int c = room.GetGridPosition;

        // These 4 origins ensure the 2x2 square always contains the center room
        Vector2Int[] origins =
        {
        c,                                                              // center is bottom-left
        c - directionOffsets[DirectionEnum.East],                       // center is bottom-right
        c - directionOffsets[DirectionEnum.North],                      // center is top-left
        c - directionOffsets[DirectionEnum.North] - directionOffsets[DirectionEnum.East] // center is top-right
    };

        foreach (var origin in origins)
        {
            Vector2Int A = origin;
            Vector2Int B = origin + directionOffsets[DirectionEnum.East];
            Vector2Int C2 = origin + directionOffsets[DirectionEnum.North];
            Vector2Int D = C2 + directionOffsets[DirectionEnum.East];

            Vector2Int[] block = { A, B, C2, D };

            List<Vector2Int> empty = new();
            List<Vector2Int> filled = new();

            foreach (var pos in block)
            {
                if (RoomExists(pos)) filled.Add(pos);
                else empty.Add(pos);
            }

            // must contain center room exactly once
            if (!filled.Contains(c))
                continue;

            // must have center + 3 empty
            if (empty.Count == 3 && filled.Count == 1)
                result.Add(empty.ToArray());
        }

        return result;
    }


}
