using UnityEngine;

public class RoomBuilder : IRoomBuilder
{
    private readonly EnemySpawnFactory enemyFactory;

    private Vector2Int gridPos;
    private Room fromRoom;
    private DirectionEnum fromDir;
    private bool isConnectedBuild;
    private RoomSidesFactory roomSidesFactory;
    private DungeonSettingsData dSD;

    private RoomSpawnData roomSpawnData;

    public RoomBuilder(DungeonSettingsData dSD, EnemySpawnFactory enemyFactory)
    {
        this.dSD = dSD;
        this.enemyFactory = enemyFactory;
        roomSidesFactory = new(dSD.GetDungeon);
    }

    public Room Build()
    {
        roomSpawnData = dSD.GetRandomRoomSpawnData;

        if (isConnectedBuild && fromRoom != null)
            gridPos = fromRoom.GetGridPosition + SideDirectionHelper.DirectionToOffset(fromDir);

        if (dSD.GetDungeon.RoomExists(gridPos))
            return dSD.GetDungeon.GetRoom(gridPos.x, gridPos.y);

        Room room = RoomCreationHandler.CreateRoom(gridPos, dSD, roomSpawnData);
        RoomEnemyHandler.SpawnEnemies(room, enemyFactory, dSD);

        if (dSD.GetCrossGenMode || RandomService.Chance(dSD.GetRoomChainLikelyhood))
        {
            DetermineBiggerShape(room);
            roomSidesFactory.ProcessRoomCollection(ref room);
        }
        else
        {
            roomSidesFactory.AddRandomSides(ref room);
        }

        ResetBuilderState();

        return room;
    }

    private void DetermineBiggerShape(Room room)
    {

        var freeSpaces = dSD.GetDungeon.GetNeighborFreeSpaces(room);

        foreach (var fS in freeSpaces)
        {
            if (RandomService.Chance(dSD.GetChancePerDirection)) continue;

            Room newRoom = room.AddChild(RoomCreationHandler.CreateRoom(fS.Value, dSD, roomSpawnData));
            RoomEnemyHandler.SpawnEnemies(newRoom, enemyFactory, dSD);

            if (RandomService.Chance(dSD.GetExtendedRoomChainLikelyhood))
            {
                var extendedFreeSpaces = dSD.GetDungeon.GetNeighborFreeSpaces(newRoom);

                foreach (var eFS in extendedFreeSpaces)
                {
                    if (RandomService.Chance(dSD.GetChancePerDirection)) continue;

                    Room extendedRoom = room.AddChild(RoomCreationHandler.CreateRoom(eFS.Value, dSD, roomSpawnData));
                    RoomEnemyHandler.SpawnEnemies(extendedRoom, enemyFactory, dSD);
                }
            }
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
