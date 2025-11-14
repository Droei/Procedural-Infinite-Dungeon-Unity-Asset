//using System.Collections.Generic;
//using UnityEngine;

//public static class RoomConnectionHandler
//{
//    public static void SetupRoomSides(Room room, RoomSidesFactory sideFactory)
//    {
//        // Wtf are you doing, why is this all needed?
//        sideFactory.GenerateSides(room);
//        sideFactory.SyncSidesWithNeighbors(room);
//    }

//    public static void Setup2x2RoomSides(Room room, RoomSidesFactory sideFactory, Dictionary<Vector2Int, DirectionEnum> parts)
//    {
//        //Idem
//        sideFactory.Built2x2Room(room, parts);
//        sideFactory.SyncSidesWithNeighbors(room);
//    }

//    //Shouldn't be here
//    public static void HandleConnections(Room room, Room fromRoom, DirectionEnum fromDir)
//    {
//        var view = room.GetRoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();

//        if (fromRoom.HasDoor(fromDir))
//        {
//            fromRoom.AddDoor(fromDir);
//            room.AddDoor(RoomHelper.Opposite(fromDir));
//            view.DisableDoorTrigger(RoomHelper.Opposite(fromDir));
//        }
//        else if (fromRoom.HasRoomOpening(fromDir))
//        {
//            fromRoom.AddChild(room);
//            room.AddRoomOpening(RoomHelper.Opposite(fromDir));
//            view.PreventNewConnectedSpawnInPrevious(RoomHelper.Opposite(fromDir));
//        }
//    }
//}
