public static class RoomConnectionHandler
{
    public static void SetupRoomSides(Room room, RoomSidesFactory doorFactory)
    {
        doorFactory.AddRandomSides(room);
        doorFactory.SyncSidesWithNeighbors(room);
    }

    public static void Setup2x2RoomSides(Room room, RoomSidesFactory doorFactory)
    {
        doorFactory.SyncSidesWithNeighbors(room);
    }

    public static void HandleConnections(Room room, Room fromRoom, DirectionEnum fromDir)
    {
        var view = room.GetRoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();

        if (fromRoom.HasDoor(fromDir))
        {
            fromRoom.AddDoor(fromDir);
            room.AddDoor(RoomHelper.Opposite(fromDir));
            view.DisableDoorTrigger(RoomHelper.Opposite(fromDir));
        }
        else if (fromRoom.HasRoomOpening(fromDir))
        {
            fromRoom.SetAsChildOfThisRoom(room);
            room.AddRoomOpening(RoomHelper.Opposite(fromDir));
            view.PreventNewConnectedSpawnInPrevious(RoomHelper.Opposite(fromDir));
        }
    }
}
