using UnityEngine;

public interface IRoomFactory
{
    Room CreateRoom(Vector2Int gridPos);
    Room SpawnConnectedRoom(DirectionEnum fromDir, Room fromRoom);
}
