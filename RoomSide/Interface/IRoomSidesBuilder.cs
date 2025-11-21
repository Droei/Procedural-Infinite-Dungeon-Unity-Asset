public interface IRoomSidesBuilder
{
    IRoomSidesBuilder ForRoom(Room room);
    IRoomSidesBuilder AsStartRoom();
    IRoomSidesBuilder ProcessCollection();
    void Build();
}
