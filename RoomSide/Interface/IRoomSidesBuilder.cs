public interface IRoomSidesBuilder
{
    IRoomSidesBuilder WithRoom(Room room);
    IRoomSidesBuilder AddRandomDoors();
    IRoomSidesBuilder SyncWithNeighbors(bool removeInvalid = true);
    void Build();
}
