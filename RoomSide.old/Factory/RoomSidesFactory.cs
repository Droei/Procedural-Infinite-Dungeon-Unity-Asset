//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class RoomSidesFactory : IRoomSidesFactory
//{
//    private readonly Dungeon dungeon;
//    private readonly SidesRandomiser doorRandomiser;

//    public RoomSidesFactory(Dungeon dungeon)
//    {
//        this.dungeon = dungeon;
//        doorRandomiser = new SidesRandomiser(dungeon);
//    }

//    public void GenerateSides(Room room)
//    {
//        var builder = new RoomSidesBuilder(dungeon, doorRandomiser)
//            .WithRoom(room)
//            .AddRandomDoors();
//        builder.Build();
//    }

//    public void SyncSidesWithNeighbors(Room room, bool removeInvalid = true)
//    {
//        var builder = new RoomSidesBuilder(dungeon, doorRandomiser)
//            .WithRoom(room)
//            .SyncWithNeighbors(removeInvalid);
//        builder.Build();
//    }

//    public void Built2x2Room(Room room, Dictionary<Vector2Int, DirectionEnum> parts)
//    {
//        var builder = new RoomSidesBuilder(dungeon, doorRandomiser)
//        .WithRoom(room)
//        .ForcedOpeningDirection(parts.Values.First());
//        builder.Build();
//    }
//}
