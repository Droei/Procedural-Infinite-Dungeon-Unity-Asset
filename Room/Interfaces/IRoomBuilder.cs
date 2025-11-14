using UnityEngine;

public interface IRoomBuilder
{
    IRoomBuilder WithPosition(Vector2Int gridPos);
    IRoomBuilder WithSpawnData(RoomSpawnData data);
    IRoomBuilder ConnectedFrom(Room fromRoom, DirectionEnum fromDir);
    IRoomBuilder CrossGenMode(bool crossGenMode);
    Room Build();
}
