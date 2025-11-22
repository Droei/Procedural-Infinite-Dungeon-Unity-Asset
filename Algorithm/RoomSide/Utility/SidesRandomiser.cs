using System.Collections.Generic;
using UnityEngine;

public static class SidesRandomiser
{
    public static void PlaceDoorOrWallInRoom(Room room, DirectionEnum dir, float doorChance = 0.5f)
    {
        if (RandomService.Chance(doorChance)) room.AddDoor(dir);
    }
}
