using System.Collections.Generic;
using UnityEngine;

public static class Dungeon2x2Helper
{
    private static DirectionEnum GetSideDirection(Vector2Int from, Vector2Int to)
    {
        Vector2Int offset = to - from;

        foreach (DirectionEnum dir in new[] { DirectionEnum.North, DirectionEnum.South, DirectionEnum.East, DirectionEnum.West })
        {
            if (RoomHelper.DirectionToOffset(dir) == offset)
                return dir;
        }

        return DirectionEnum.North; // default if not exactly cardinal
    }

    // Returns all valid 2x2 combinations with the initial room,
    // mapping each position to its side direction relative to the initial room
    public static List<Dictionary<Vector2Int, DirectionEnum>> Find2x2CombinationsWithRoom(Dungeon dungeon, Room room)
    {
        Vector2Int origin = Vector2Int.RoundToInt(room.GetGridPosition);

        Vector2Int[] offsets =
        {
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(1, 1)
        };

        Vector2Int[] potentialAnchors =
        {
            new Vector2Int(0, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, -1)
        };

        List<Dictionary<Vector2Int, DirectionEnum>> validCombinations = new();

        foreach (var anchor in potentialAnchors)
        {
            int baseX = origin.x + anchor.x;
            int baseY = origin.y + anchor.y;

            Dictionary<Vector2Int, DirectionEnum> combination = new();
            bool canFit = true;

            foreach (var offset in offsets)
            {
                int checkX = baseX + offset.x;
                int checkY = baseY + offset.y;

                if (checkX == origin.x && checkY == origin.y) continue;

                Room checkRoom = dungeon.GetRoom(checkX, checkY);
                if (checkRoom != null)
                {
                    canFit = false;
                    break;
                }

                Vector2Int pos = new(checkX, checkY);
                DirectionEnum dir = GetSideDirection(origin, pos);
                combination[pos] = dir;
            }

            if (canFit && combination.Count == 3)
                validCombinations.Add(combination);
        }

        return validCombinations;
    }

    public static void Log2x2CombinationsWithRoom(Dungeon dungeon, Room room)
    {
        var combinations = Find2x2CombinationsWithRoom(dungeon, room);

        if (combinations.Count == 0)
        {
            Debug.Log($"No valid 2x2 combinations found with room {room.GetRoomGameObject.name}.");
            return;
        }

        Debug.Log($"Found {combinations.Count} valid 2x2 combinations with room {room.GetRoomGameObject.name}:");

        int index = 1;
        foreach (var combo in combinations)
        {
            List<string> parts = new();
            foreach (var kv in combo)
            {
                parts.Add($"{kv.Key} ({kv.Value})");
            }

            Debug.Log($"Combination {index}: {string.Join(", ", parts)}");
            index++;
        }
    }
}
