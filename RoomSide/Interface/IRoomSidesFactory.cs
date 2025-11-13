public interface IRoomSidesFactory
{
    void AddRandomSides(Room room);
    void SyncSidesWithNeighbors(Room room, bool removeInvalid = true);
}
