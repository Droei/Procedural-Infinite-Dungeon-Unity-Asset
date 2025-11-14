using UnityEngine;

public class RoomSidesBuilder : IRoomSidesBuilder
{
    private readonly Dungeon dungeon;
    private readonly SidesRandomiser doorRandomiser;
    private Room room;
    private bool doRandomDoors;
    private bool doSync;
    private bool removeInvalid;

    private DirectionEnum forcedOpening;

    public RoomSidesBuilder(Dungeon dungeon, SidesRandomiser doorRandomiser)
    {
        this.dungeon = dungeon;
        this.doorRandomiser = doorRandomiser;
    }

    public void Build()
    {
        if (room == null) return;

        if (doRandomDoors)
            doorRandomiser.AddRandomSides(room);

        if (doSync)
            SyncDoors();
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

    // This whole syncing system shouldn't happen man, the room should be created with all sides defined before it spawns in this is such a fckn mess
    private void SyncDoors()
    {
        DungeonRoomMonoBehaviour roomView = room.GetRoomView;

        foreach (DirectionEnum dir in System.Enum.GetValues(typeof(DirectionEnum)))
        {
            if (dir == DirectionEnum.None) continue;

            Vector2Int neighborPos = room.GetGridPosition + RoomHelper.DirectionToOffset(dir);
            Room neighbor = dungeon.GetRoom(neighborPos.x, neighborPos.y);

            bool roomCanHave = room.CanHaveDoor(dir);

            if (neighbor != null)
            {
                bool neighborHasDoor = neighbor.HasDoor(RoomHelper.Opposite(dir));

                if (neighborHasDoor && !room.HasDoor(dir) && roomCanHave)
                {
                    room.AddDoor(dir);
                    roomView.SetDoorState(dir, true);
                }

                if (!neighborHasDoor && room.HasDoor(dir) && removeInvalid && roomCanHave)
                {
                    room.RemoveDoor(dir);
                    roomView.SetDoorState(dir, false);
                }
            }
        }
    }

    public IRoomSidesBuilder ForcedOpeningDirection(DirectionEnum dir)
    {
        forcedOpening = dir;
        Debug.Log(dir);
        return this;
    }
}
