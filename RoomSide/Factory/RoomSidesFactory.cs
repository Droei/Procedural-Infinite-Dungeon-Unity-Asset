using UnityEngine;

public class RoomSidesFactory : IRoomSidesFactory
{
    private readonly Dungeon dungeon;
    private readonly SidesRandomiser doorRandomiser;

    public RoomSidesFactory(Dungeon dungeon)
    {
        this.dungeon = dungeon;
        doorRandomiser = new SidesRandomiser(dungeon);
    }

    public void AddRandomSides(Room room)
    {
        var builder = new RoomSidesBuilder(dungeon, doorRandomiser)
            .WithRoom(room)
            .AddRandomDoors();
        builder.Build();
    }

    public void SyncSidesWithNeighbors(Room room, bool removeInvalid = true)
    {
        var builder = new RoomSidesBuilder(dungeon, doorRandomiser)
            .WithRoom(room)
            .SyncWithNeighbors(removeInvalid);
        builder.Build();
    }

    public void Built2x2Room(Room room, Vector2Int[] sides)
    {

    }
}
