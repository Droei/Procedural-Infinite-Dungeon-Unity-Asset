using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSidesBuilder : IRoomSidesBuilder
{
    private readonly SidesGenerator sidesGenerator;

    private Room room;
    private bool startRoom;
    private bool processCollection;

    public RoomSidesBuilder(DungeonSettingsData dSD)
    {
        sidesGenerator = new SidesGenerator(dSD);
    }

    public IRoomSidesBuilder ForRoom(Room room)
    {
        this.room = room;
        return this;
    }

    public IRoomSidesBuilder AsStartRoom()
    {
        startRoom = true;
        return this;
    }

    public IRoomSidesBuilder ProcessCollection()
    {
        processCollection = true;
        return this;
    }

    public void Build()
    {
        if (room == null)
            throw new InvalidOperationException("Room must be assigned before building sides.");

        if (processCollection)
        {
            ProcessRoomCollection(room);
        }
        else
        {
            sidesGenerator.GenerateSides(room, startRoom);
            SideVisualView.UpdateRoomVisual(room);
        }

        ResetState();
    }

    private void ProcessRoomCollection(Room parent)
    {
        var rooms = new List<Room> { parent }.Concat(parent.GetChildRooms).ToList();
        var roomPositions = new HashSet<Vector2Int>(rooms.Select(r => r.GetGridPosition));

        foreach (Room r in rooms)
        {
            Vector2Int pos = r.GetGridPosition;

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
                    r.AddOpenArea(dir);
            }

            sidesGenerator.GenerateSides(r);
            SideVisualView.UpdateRoomVisual(r);
        }
    }

    private void ResetState()
    {
        room = null;
        startRoom = false;
        processCollection = false;
    }
}
