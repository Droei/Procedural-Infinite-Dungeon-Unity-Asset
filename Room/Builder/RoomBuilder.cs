using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IRoomBuilder
{
    private readonly float roomSize;
    private readonly DungeonManager dungeonManager;
    private readonly IEnemySpawnFactory enemyFactory;
    private readonly Dungeon dungeon;
    private readonly RoomSidesFactory doorFactory;

    private Vector2Int gridPos;
    private RoomSpawnData spawnData;

    private Room fromRoom;
    private DirectionEnum fromDir;
    private bool isConnectedBuild;

    private int roomCount = 0;

    public RoomBuilder(float roomSize, DungeonManager dungeonManager, IEnemySpawnFactory enemyFactory, Dungeon dungeon)
    {
        this.roomSize = roomSize;
        this.dungeonManager = dungeonManager;
        this.enemyFactory = enemyFactory;
        this.dungeon = dungeon;
        doorFactory = new(dungeon);
    }

    public Room Build()
    {
        if (isConnectedBuild && fromRoom != null)
            gridPos = fromRoom.GetGridPosition + RoomHelper.DirectionToOffset(fromDir);

        if (dungeon.RoomExists(gridPos))
            return dungeon.GetRoom(gridPos.x, gridPos.y);

        Room room = RoomCreationHandler.CreateRoom(gridPos, roomSize, spawnData, ref roomCount);

        if (spawnData != null && spawnData.Is2x2)
        {
            var combinations = Dungeon2x2Helper.Find2x2CombinationsWithRoom(dungeon, room);

            if (combinations.Count > 0)
            {
                var firstCombo = combinations[0];

                List<string> parts = new();
                foreach (var kv in firstCombo)
                {
                    parts.Add($"{kv.Key} ({kv.Value})");
                }

                Debug.Log("First combination: " + string.Join(", ", parts));
            }
            RoomConnectionHandler.Setup2x2RoomSides(room, doorFactory);
        }
        else
        {
            RoomConnectionHandler.SetupRoomSides(room, doorFactory);
        }

        RoomEnemyHandler.SpawnEnemies(room, enemyFactory, roomSize, dungeonManager.GetCurrentWave, spawnData);
        RoomViewHandler.InitView(room, dungeonManager);



        if (isConnectedBuild && fromRoom != null)
            RoomConnectionHandler.HandleConnections(room, fromRoom, fromDir);

        RoomVisualView.UpdateRoomVisual(room);

        dungeon.AddRoom(room);
        if (room.GetParent == null)
        {
            dungeonManager.NextWave();
        }

        ResetBuilderState();

        return room;
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
