public class RoomSidesFactory : IRoomSidesFactory
{
    private readonly IRoomSidesBuilder builder;

    public RoomSidesFactory(IRoomSidesBuilder builder)
    {
        this.builder = builder;
    }

    public void AddRandomSides(Room room, bool startRoom = false)
    {
        if (startRoom)
            builder.ForRoom(room).AsStartRoom().Build();
        else
            builder.ForRoom(room).Build();
    }

    public void ProcessRoomCollection(Room room)
    {
        builder.ForRoom(room).ProcessCollection().Build();
    }
}
