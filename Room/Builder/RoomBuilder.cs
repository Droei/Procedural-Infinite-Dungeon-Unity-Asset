using UnityEngine;

public class RoomBuilder : IRoomBuilder
{
    private readonly float roomSize;
    private readonly DungeonManager dungeonManager;
    private readonly IEnemySpawnFactory enemyFactory;
    private readonly Dungeon dungeon;

    private Vector2Int gridPos;
    private RoomSpawnData spawnData;

    private Room fromRoom;
    private DirectionEnum fromDir;
    private bool isConnectedBuild;

    private RoomSidesFactory roomSidesFactory;

    private int roomCount = 0;

    public RoomBuilder(float roomSize, DungeonManager dungeonManager, IEnemySpawnFactory enemyFactory, Dungeon dungeon)
    {
        this.roomSize = roomSize;
        this.dungeonManager = dungeonManager;
        this.enemyFactory = enemyFactory;
        this.dungeon = dungeon;
        roomSidesFactory = new(dungeon);
    }

    public Room Build()
    {
        if (isConnectedBuild && fromRoom != null)
            gridPos = fromRoom.GetGridPosition + SideDirectionHelper.DirectionToOffset(fromDir);

        if (dungeon.RoomExists(gridPos))
            return dungeon.GetRoom(gridPos.x, gridPos.y);

        Room room = RoomCreationHandler.CreateRoom(gridPos, roomSize, spawnData, ref roomCount);

        // Spawning room for debugging
        //Room room2 = RoomCreationHandler.CreateRoom(new(gridPos.x + 1, gridPos.y), roomSize, spawnData, ref roomCount);
        //dungeon.AddRoom(room2);
        //RoomViewHandler.InitView(room2, dungeonManager);

        //DetermineBiggerShape(room);

        roomSidesFactory.AddRandomSides(ref room);
        RoomEnemyHandler.SpawnEnemies(room, enemyFactory, roomSize, dungeonManager.GetCurrentWave, spawnData);
        RoomViewHandler.InitView(room, dungeonManager);

        dungeon.AddRoom(room);

        if (room.GetParent == null)
        {
            dungeonManager.NextWave();
        }

        ResetBuilderState();

        return room;
    }

    private void DetermineBiggerShape(Room room)
    {
        //If conditions are met for the algoritm to think about adding a new room
        if (true)
        {
            var freeSpaces = dungeon.GetNeighborFreeSpaces(room);

            foreach (var kvp in freeSpaces)
            {
                DirectionEnum dir = kvp.Key;
                Vector2Int EmptyLocation = kvp.Value;
                room.AddChild(RoomCreationHandler.CreateRoom(EmptyLocation, roomSize, spawnData, ref roomCount));
                Debug.Log($"No neighbor to the {dir}: {EmptyLocation}");
            }
        }
    }

    private void ResetBuilderState()
    {
        gridPos = default;
        spawnData = null;
        fromRoom = null;
        fromDir = default;
        isConnectedBuild = false;
    }


    public IRoomBuilder WithPosition(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
        return this;
    }

    public IRoomBuilder WithSpawnData(RoomSpawnData data)
    {
        this.spawnData = data;
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
