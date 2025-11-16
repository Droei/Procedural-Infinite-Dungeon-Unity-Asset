using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawnData", menuName = "Dungeon/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private DungeonRoom roomObject;
    [SerializeField] private EnemySpawnData specificEnemy;

    [Header("Room Specific")]
    [SerializeField] private bool is2x2 = false;
    [SerializeField] private bool roomLocks = false;

    public DungeonRoom RoomObject => roomObject;
    public EnemySpawnData SpecificEnemy => specificEnemy;
    public bool Is2x2 => is2x2;
    public bool RoomLocks => roomLocks;
}
