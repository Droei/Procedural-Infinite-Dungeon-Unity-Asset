using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] List<DungeonSettingsData> dSDList;
    [SerializeField] int RoomSize = 25;

    DungeonSettingsData dSD;

    private IRoomFactory roomFactory;
    private IRoomBuilder roomBuilder;
    private NavMeshSurface navSurface;
    private void Awake()
    {
        dSDList = dSDList.OrderBy(d => d.TakeOverAtWave).ToList();
        dSD = dSDList[0].CreateRuntimeInstance();
        if (dSD.DebugMode && dSD.UseStaticSeed) RandomService.Initialize(dSD.Seed);
    }

    private void Start()
    {
        navSurface = GetComponent<NavMeshSurface>();
        dSD.SetDungeon(new Dungeon(this));
        dSD.SetRoomSize(RoomSize);

        IEnemySpawnBuilder builder = new EnemySpawnBuilder(dSD);
        IEnemySpawnFactory enemyFactory = new EnemySpawnFactory(builder);

        roomBuilder = new RoomBuilder(dSD, dSDList, enemyFactory);
        roomFactory = new RoomFactory(roomBuilder);

        roomFactory.CreateInitialRoom(Vector2Int.zero);
    }

    // TODO: Find a cleaner solution for this later
    public void SpawnRoom(DirectionEnum direction, Room room)
    {
        roomFactory.SpawnConnectedRoom(direction, room);
    }

    public void UpdateNavMesh()
    {
        if (navSurface != null)
            navSurface.BuildNavMesh();
    }

    public bool GetMaxDebugRoomsReached()
    {
        if (dSD.Dungeon.GetParentCount > dSD.DebugRoomCount) return false;

        return dSD.DebugMode && dSD.UseBatchSpawning;
    }
    public bool GetDebugMode => dSD.DebugMode;
}
