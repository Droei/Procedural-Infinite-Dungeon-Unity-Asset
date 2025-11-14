using UnityEngine;

public class RoomSidesBuilder : IRoomSidesBuilder
{
    private readonly Dungeon dungeon;
    private Room room;
    private bool doRandomDoors;
    private bool doSync;
    private bool removeInvalid;

    private DirectionEnum forcedOpening;

    public RoomSidesBuilder(Dungeon dungeon)
    {
        this.dungeon = dungeon;
    }

    public void Build()
    {
        if (room == null) return;

    }

    public IRoomSidesBuilder WithRoom(Room room)
    {
        this.room = room;
        return this;
    }

    public IRoomSidesBuilder AddRandomDoors()
    {
        doRandomDoors = true;
        return this;
    }

    public IRoomSidesBuilder SyncWithNeighbors(bool removeInvalid = true)
    {
        doSync = true;
        this.removeInvalid = removeInvalid;
        return this;
    }

    public IRoomSidesBuilder ForcedOpeningDirection(DirectionEnum dir)
    {
        forcedOpening = dir;
        Debug.Log(dir);
        return this;
    }
}
