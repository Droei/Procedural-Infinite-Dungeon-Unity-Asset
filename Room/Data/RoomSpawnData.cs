using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawnData", menuName = "Dungeon/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private DungeonRoom roomObject;
    [SerializeField] private EnemySpawnData specificEnemy;

    [Header("Room Specific")]
    [SerializeField] private bool is1x1 = false;
    [SerializeField] private bool is2x2 = false;
    [SerializeField] private bool isLootRoom = false;
    [SerializeField] private bool canHaveLootroomInSecondarySpawn = true;
    [SerializeField] private GameObject SpecificLootChest;
    [SerializeField] private bool roomLocks = false;

    public DungeonRoom RoomObject => roomObject;
    public EnemySpawnData SpecificEnemy => specificEnemy;
    public bool Is2x2 => is2x2;
    public bool Is1x1 => is1x1;
    public bool IsLootRoom => isLootRoom;
    public bool CanHaveLootRoomInSecondarySpawn;
    public bool RoomLocks => roomLocks;
}
