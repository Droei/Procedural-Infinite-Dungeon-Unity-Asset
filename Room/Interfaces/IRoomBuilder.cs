using UnityEngine;

public interface IRoomBuilder
{
    IRoomBuilder WithPosition(Vector2Int gridPos);
    IRoomBuilder WithSpawnData(RoomSpawnData data);
    IRoomBuilder ConnectedFrom(Room fromRoom, DirectionEnum fromDir);
    Room Build();
}
