public static class SidesSyncer
{
    public static void SyncSidesWithNeighbor(Room room, Room neighbor, DirectionEnum directionFromRoom)
    {
        if (neighbor.HasDoor(DirectionHelper.Opposite(directionFromRoom)))
            room.AddDoor(directionFromRoom);
    }
}