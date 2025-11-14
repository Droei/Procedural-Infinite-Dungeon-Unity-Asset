using UnityEngine;

public class RoomFactory : IRoomFactory
{
    private readonly IRoomBuilder roomBuilder;
    public RoomFactory(IRoomBuilder roomBuilder)
    {
        this.roomBuilder = roomBuilder;
    }

    public Room CreateRoom(Vector2Int gridPos) =>
        BuildRoom(builder => builder.WithPosition(gridPos));

    public Room SpawnConnectedRoom(DirectionEnum fromDir, Room fromRoom) =>
        BuildRoom(builder => builder.ConnectedFrom(fromRoom, fromDir));

    private Room BuildRoom(System.Func<IRoomBuilder, IRoomBuilder> configure)
    {
        return configure(roomBuilder).Build();
    }
}
