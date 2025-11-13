using System.Collections.Generic;
using UnityEngine;

public class RoomFactory : IRoomFactory
{
    private readonly List<RoomSpawnData> roomSpawnDatas;
    private readonly IRoomBuilder roomBuilder;

    public RoomFactory(List<RoomSpawnData> roomSpawnData, IRoomBuilder roomBuilder)
    {
        this.roomSpawnDatas = roomSpawnData;
        this.roomBuilder = roomBuilder;
    }

    private RoomSpawnData GetRandomSpawnData() =>
        roomSpawnDatas[RandomService.Range(0, roomSpawnDatas.Count)];

    public Room CreateRoom(Vector2Int gridPos) =>
        BuildRoom(builder => builder.WithPosition(gridPos).WithSpawnData(GetRandomSpawnData()));

    public Room SpawnConnectedRoom(DirectionEnum fromDir, Room fromRoom) =>
        BuildRoom(builder => builder.ConnectedFrom(fromRoom, fromDir).WithSpawnData(GetRandomSpawnData()));

    private Room BuildRoom(System.Func<IRoomBuilder, IRoomBuilder> configure)
    {
        return configure(roomBuilder).Build();
    }
}
