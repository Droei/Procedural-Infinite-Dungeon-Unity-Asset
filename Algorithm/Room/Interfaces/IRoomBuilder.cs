using UnityEngine;

public interface IRoomBuilder
{
    IRoomBuilder WithPosition(Vector2Int gridPos);
    IRoomBuilder ConnectedFrom(Room fromRoom, DirectionEnum fromDir);
    Room Build();
    IRoomBuilder StartRoom();
}
