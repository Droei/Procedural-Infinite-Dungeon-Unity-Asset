using System.Collections.Generic;
using UnityEngine;

public class SidesRandomiser
{
    readonly Dungeon dungeon;
    public SidesRandomiser(Dungeon dungeon)
    {
        this.dungeon = dungeon;
    }
    public void AddRandomSides(Room newRoom)
    {
        List<DirectionEnum> possibleDirs = new();

        GetWallFreeAreas(newRoom, possibleDirs);

        if (possibleDirs.Count == 0)
            return;

        FisherYatesShuffle(possibleDirs);
        AddRandomNumberOfSides(newRoom, possibleDirs);
    }

    void GetWallFreeAreas(Room newRoom, List<DirectionEnum> possibleDirs)
    {
        foreach (DirectionEnum dir in System.Enum.GetValues(typeof(DirectionEnum)))
        {
            if (dir == DirectionEnum.None) continue;

            if (newRoom.HasDoor(dir))
                continue;

            Vector2Int neighborPos = newRoom.GetGridPosition + RoomHelper.DirectionToOffset(dir);
            Room neighbor = dungeon.GetRoom(neighborPos.x, neighborPos.y);

            if (neighbor == null)
                possibleDirs.Add(dir);
        }
    }

    static void FisherYatesShuffle(List<DirectionEnum> possibleDirs)
    {
        for (int i = 0; i < possibleDirs.Count; i++)
        {
            int j = RandomService.Range(i, possibleDirs.Count);
            var temp = possibleDirs[i];
            possibleDirs[i] = possibleDirs[j];
            possibleDirs[j] = temp;
        }
    }

    static void AddRandomNumberOfSides(Room newRoom, List<DirectionEnum> possibleDirs)
    {
        int doorsToAdd = RandomService.Range(1, Mathf.Min(3, possibleDirs.Count) + 1);

        for (int i = 0; i < doorsToAdd; i++)
        {
            DirectionEnum dir = possibleDirs[i];
            if (RandomService.Range(0f, 1f) < 0.8f)
            {
                newRoom.AddDoor(dir);
            }
            else
            {
                newRoom.AddRoomOpening(dir);
            }
        }
    }

}
