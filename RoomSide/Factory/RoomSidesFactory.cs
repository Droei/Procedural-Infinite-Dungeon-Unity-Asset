using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO make this into a correct factory implementation (Pls)
public class RoomSidesFactory
{
    private readonly SidesGenerator sidesGenerator;

    public RoomSidesFactory(DungeonSettingsData dSD)
    {
        sidesGenerator = new SidesGenerator(dSD);
    }

    public void AddRandomSides(ref Room room, bool startRoom = false)
    {
        sidesGenerator.GenerateSides(room, startRoom);
        SideVisualView.UpdateRoomVisual(room);
    }


    public void ProcessRoomCollection(ref Room parent)
    {
        var rooms = new List<Room> { parent }.Concat(parent.GetChildRooms).ToList();

        var roomPositions = new HashSet<Vector2Int>(rooms.Select(r => r.GetGridPosition));

        foreach (Room room in rooms)
        {
            Vector2Int pos = room.GetGridPosition;

            foreach (DirectionEnum dir in Enum.GetValues(typeof(DirectionEnum)))
            {
                if (dir == DirectionEnum.None) continue;

                Vector2Int neighborPos = dir switch
                {
                    DirectionEnum.North => pos + Vector2Int.up,
                    DirectionEnum.South => pos + Vector2Int.down,
                    DirectionEnum.East => pos + Vector2Int.right,
                    DirectionEnum.West => pos + Vector2Int.left,
                    _ => pos
                };
                if (roomPositions.Contains(neighborPos))
                {
                    room.AddOpenArea(dir);
                }
            }
            sidesGenerator.GenerateSides(room);
            SideVisualView.UpdateRoomVisual(room);
        }
    }



}
