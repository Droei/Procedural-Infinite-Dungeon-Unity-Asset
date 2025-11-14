public static class SidesSyncer
{
    public static void SyncSidesWithNeighbor(Room room, Room neighbor, DirectionEnum directionFromRoom)
    {
        if (neighbor.HasDoor(SideDirectionHelper.Opposite(directionFromRoom)))
            room.AddDoor(directionFromRoom);
    }
}