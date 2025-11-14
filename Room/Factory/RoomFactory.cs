using System.Collections.Generic;
using UnityEngine;

public class RoomFactory : IRoomFactory
{
    private readonly List<RoomSpawnData> roomSpawnDatas;
    private readonly IRoomBuilder roomBuilder;
    private readonly bool crossGenMode;

    public RoomFactory(List<RoomSpawnData> roomSpawnData, IRoomBuilder roomBuilder, bool crossGenMode)
    {
        this.roomSpawnDatas = roomSpawnData;
        this.roomBuilder = roomBuilder;
        this.crossGenMode = crossGenMode;
    }

    private RoomSpawnData GetRandomSpawnData() =>
        roomSpawnDatas[RandomService.Range(0, roomSpawnDatas.Count)];

    public Room CreateRoom(Vector2Int gridPos) =>
        BuildRoom(builder => builder.WithPosition(gridPos).WithSpawnData(GetRandomSpawnData()).CrossGenMode(crossGenMode));

    //TODO make this cleaner
    public Room SpawnConnectedRoom(DirectionEnum fromDir, Room fromRoom) =>
        BuildRoom(builder => builder.ConnectedFrom(fromRoom, fromDir).WithSpawnData(GetRandomSpawnData()));

    private Room BuildRoom(System.Func<IRoomBuilder, IRoomBuilder> configure)
    {
        return configure(roomBuilder).Build();
    }
}
