using UnityEngine;

public interface IRoomFactory
{
    Room CreateInitialRoom(Vector2Int zero);
    Room CreateRoom(Vector2Int gridPos);
    Room SpawnConnectedRoom(DirectionEnum fromDir, Room fromRoom);
}
