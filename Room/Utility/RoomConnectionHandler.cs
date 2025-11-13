using System.Collections.Generic;
using UnityEngine;

public static class RoomConnectionHandler
{
    public static void SetupRoomSides(Room room, RoomSidesFactory sideFactory)
    {
        sideFactory.AddRandomSides(room);
        sideFactory.SyncSidesWithNeighbors(room);
    }

    public static void Setup2x2RoomSides(Room room, RoomSidesFactory sideFactory, Dictionary<Vector2Int, DirectionEnum> parts)
    {
        sideFactory.Built2x2Room(room, parts);
        sideFactory.SyncSidesWithNeighbors(room);
    }

    public static void HandleConnections(Room room, Room fromRoom, DirectionEnum fromDir)
    {
        var view = room.GetRoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();

        if (fromRoom.HasDoor(fromDir))
        {
            fromRoom.AddDoor(fromDir);
            room.AddDoor(RoomHelper.Opposite(fromDir));
            view.DisableDoorTrigger(RoomHelper.Opposite(fromDir));
        }
        else if (fromRoom.HasRoomOpening(fromDir))
        {
            fromRoom.SetAsChildOfThisRoom(room);
            room.AddRoomOpening(RoomHelper.Opposite(fromDir));
            view.PreventNewConnectedSpawnInPrevious(RoomHelper.Opposite(fromDir));
        }
    }
}
