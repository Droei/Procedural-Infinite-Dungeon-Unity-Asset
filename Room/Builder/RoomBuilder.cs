using UnityEngine;

public class RoomBuilder : IRoomBuilder
{
    private readonly IEnemySpawnFactory enemyFactory;

    private Vector2Int gridPos;
    private Room fromRoom;
    private DirectionEnum fromDir;
    private bool isConnectedBuild;
    private RoomSidesFactory roomSidesFactory;
    private DungeonSettingsData dSD;

    public RoomBuilder(DungeonSettingsData dSD, IEnemySpawnFactory enemyFactory)
    {
        this.dSD = dSD;
        this.enemyFactory = enemyFactory;
        roomSidesFactory = new(dSD.GetDungeon);
    }

    public Room Build()
    {
        if (isConnectedBuild && fromRoom != null)
            gridPos = fromRoom.GetGridPosition + SideDirectionHelper.DirectionToOffset(fromDir);

        if (dSD.GetDungeon.RoomExists(gridPos))
            return dSD.GetDungeon.GetRoom(gridPos.x, gridPos.y);

        Room room = RoomCreationHandler.CreateRoom(gridPos, dSD);

        //Spawning room for debugging
        //Room room2 = RoomCreationHandler.CreateRoom(new(gridPos.x + 1, gridPos.y), roomSize, spawnData, ref roomCount, dungeonManager, dungeon);
        //dungeon.AddRoom(room2);
        //roomSidesFactory.AddRandomSides(ref room2);

        if (dSD.GetCrossGenMode)
        {
            DetermineBiggerShape(room);
            roomSidesFactory.ProcessRoomCollection(ref room);
        }
        else
        {
            roomSidesFactory.AddRandomSides(ref room);
        }

        RoomEnemyHandler.SpawnEnemies(room, enemyFactory, dSD);

        if (room.GetParent == null)
        {
            dSD.GetDungeon.IncrementWaveCount();
        }

        ResetBuilderState();

        return room;
    }

    private void DetermineBiggerShape(Room room)
    {

        var freeSpaces = dSD.GetDungeon.GetNeighborFreeSpaces(room);

        foreach (var kvp in freeSpaces)
        {
            DirectionEnum dir = kvp.Key;
            Vector2Int EmptyLocation = kvp.Value;
            room.AddChild(RoomCreationHandler.CreateRoom(EmptyLocation, dSD));
        }
    }

    private void ResetBuilderState()
    {
        gridPos = default;
        fromRoom = null;
        fromDir = default;
        isConnectedBuild = false;
    }


    public IRoomBuilder WithPosition(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
        return this;
    }

    public IRoomBuilder ConnectedFrom(Room fromRoom, DirectionEnum fromDir)
    {
        this.fromRoom = fromRoom;
        this.fromDir = fromDir;
        this.isConnectedBuild = true;
        return this;
    }
}
