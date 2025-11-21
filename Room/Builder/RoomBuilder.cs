using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IRoomBuilder
{
    private DirectionEnum fromDir;

    private Vector2Int gridPos;
    private Room fromRoom;
    private bool isConnectedBuild;
    private bool startRoom;

    private RoomSidesFactory roomSidesFactory;
    private DungeonSettingsData dSD;
    private RoomSpawnData roomSpawnData;
    private RoomCreationHandler roomCreationHandler;
    private EnemySpawnFactory enemySpawnFactory;

    public RoomBuilder(DungeonSettingsData dSD, EnemySpawnFactory enemySpawnFactory)
    {
        this.dSD = dSD;
        this.enemySpawnFactory = enemySpawnFactory;

        roomSidesFactory = new(dSD);
        roomCreationHandler = new(dSD);
    }

    public Room Build()
    {
        roomSpawnData = dSD.GetRandomRoomSpawnData;
        roomCreationHandler.SetRoomSpawnData(roomSpawnData);

        if (isConnectedBuild && fromRoom != null)
            gridPos = fromRoom.GetGridPosition + SideDirectionHelper.DirectionToOffset(fromDir);

        if (dSD.Dungeon.RoomExists(gridPos))
            return dSD.Dungeon.GetRoom(gridPos.x, gridPos.y);

        Room room = roomCreationHandler.CreateRoom(gridPos);

        if (roomSpawnData.IsLootRoom)
            BuildLootRoom(room);

        ApplySideGeneration(room);
        dSD.Dungeon.GetDungeonManager.UpdateNavMesh();
        if (!roomSpawnData.IsLootRoom)
            RoomEnemyHandler.SpawnEnemies(room, enemySpawnFactory, dSD, roomSpawnData.SpecificEnemy);

        ResetBuilderState();

        return room;
    }

    private void BuildLootRoom(Room room)
    {
        var (pos, bounds) = SpawnPositionGenerator.GetRoomBounds(room, dSD);

        var spawnPos = SpawnPositionGenerator.GetCenteredPosition(pos.y, bounds);
        Object.Instantiate(dSD.GeneralLootChest, spawnPos, Quaternion.identity);

        room.SetAsLootRoom();
    }

    private void ApplySideGeneration(Room room)
    {
        if (startRoom)
            roomSidesFactory.AddRandomSides(ref room, true);
        else if (roomSpawnData.IsLootRoom)
            roomSidesFactory.AddRandomSides(ref room);
        else if (roomSpawnData.Is2x2)
        {
            List<Vector2Int[]> groups = dSD.Dungeon.GetFree2x2Triplets(room);
            if (groups.Count > 0)
            {
                Vector2Int[] selection = groups[RandomService.Range(0, groups.Count - 1)];
                foreach (Vector2Int cell in selection)
                    roomCreationHandler.CreateRoom(cell, room);
            }

            roomSidesFactory.ProcessRoomCollection(ref room);
        }
        else if ((dSD.CrossGenMode || RandomService.Chance(dSD.RoomChainLikelyhood)) && !roomSpawnData.Is1x1)
        {
            DetermineBiggerShape(room);
            roomSidesFactory.ProcessRoomCollection(ref room);
        }
        else
            roomSidesFactory.AddRandomSides(ref room);
    }

    private void DetermineBiggerShape(Room room)
    {
        var freeSpaces = dSD.Dungeon.GetNeighborFreeSpaces(room);

        foreach (var fS in freeSpaces)
        {
            if (RandomService.Chance(dSD.ChancePerDirection))
                continue;

            Room newRoom = roomCreationHandler.CreateRoom(fS.Value, room);

            // Larger extended shapes
            if (RandomService.Chance(dSD.ExtendedRoomChainLikelyhood))
            {
                var extendedFreeSpaces = dSD.Dungeon.GetNeighborFreeSpaces(newRoom);

                foreach (var eFS in extendedFreeSpaces)
                {
                    if (RandomService.Chance(dSD.ChancePerDirection))
                        continue;

                    roomCreationHandler.CreateRoom(eFS.Value, room);
                }
            }
        }
    }

    private void ResetBuilderState()
    {
        startRoom = false;
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

    public IRoomBuilder StartRoom()
    {
        startRoom = true;
        return this;
    }
}
