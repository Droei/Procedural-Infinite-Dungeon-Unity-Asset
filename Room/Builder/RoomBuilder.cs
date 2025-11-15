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
        roomSidesFactory = new(dSD);
        roomCreationHandler = new(dSD);
        this.enemySpawnFactory = enemySpawnFactory;
    }

    public Room Build()
    {
        roomSpawnData = dSD.GetRandomRoomSpawnData;
        roomCreationHandler.SetRoomSpawnData(roomSpawnData);

        if (isConnectedBuild && fromRoom != null)
            gridPos = fromRoom.GetGridPosition + SideDirectionHelper.DirectionToOffset(fromDir);

        if (dSD.GetDungeon.RoomExists(gridPos))
            return dSD.GetDungeon.GetRoom(gridPos.x, gridPos.y);

        Room room = roomCreationHandler.CreateRoom(gridPos);

        if (startRoom)
            roomSidesFactory.AddRandomSides(ref room, true);
        else if (roomSpawnData.Is2x2)
        {
            List<Vector2Int[]> roomsToSpawn = dSD.GetDungeon.GetFree2x2Triplets(room);
            if (roomsToSpawn.Count > 0)
                foreach (Vector2Int loc in roomsToSpawn[RandomService.Range(0, roomsToSpawn.Count - 1)])
                    roomCreationHandler.CreateRoom(loc, room);

            roomSidesFactory.ProcessRoomCollection(ref room);
        }
        else if (dSD.GetCrossGenMode || RandomService.Chance(dSD.GetRoomChainLikelyhood))
        {
            DetermineBiggerShape(room);
            roomSidesFactory.ProcessRoomCollection(ref room);
        }
        else
        {
            roomSidesFactory.AddRandomSides(ref room);
        }

        RoomEnemyHandler.SpawnEnemies(room, enemySpawnFactory, dSD);
        ResetBuilderState();

        return room;
    }

    private void DetermineBiggerShape(Room room)
    {
        var freeSpaces = dSD.GetDungeon.GetNeighborFreeSpaces(room);

        foreach (var fS in freeSpaces)
        {
            if (RandomService.Chance(dSD.GetChancePerDirection)) continue;

            Room newRoom = roomCreationHandler.CreateRoom(fS.Value, room);

            if (RandomService.Chance(dSD.GetExtendedRoomChainLikelyhood))
            {
                var extendedFreeSpaces = dSD.GetDungeon.GetNeighborFreeSpaces(newRoom);

                foreach (var eFS in extendedFreeSpaces)
                {
                    if (RandomService.Chance(dSD.GetChancePerDirection)) continue;

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
