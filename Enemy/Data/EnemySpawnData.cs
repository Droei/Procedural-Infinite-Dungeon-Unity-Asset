using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Dungeon/EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private DungeonEnemyMonoBehaviour enemyObject;
    [SerializeField] private int spawnWeight = 1;

    [Header("Wave Settings")]
    [SerializeField] private int minWave = 0;
    [SerializeField] private int maxWave = int.MaxValue;

    [Header("Batch Settings")]
    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = int.MaxValue;
    [SerializeField] private int spawnInterval = 0;

    [Header("Spawn Placement Rules")]
    [SerializeField] private bool isCentered = false;

    [Header("Difficulty Scaling")]
    [SerializeField] private float difficultyMultiplier = 1f;

    private int spawnIntervalTicker = 0;

    public GameObject EnemyObject
    {
        get
        {
            if (enemyObject == null)
            {
                Debug.LogWarning("EnemySpawnData: enemyObject is null! Reassign in inspector.");
                return null;
            }
            return enemyObject.gameObject;
        }
    }
    public int SpawnWeight => spawnWeight;
    public int MinWave => minWave;
    public int MaxWave => maxWave;
    public int MinCount => minCount;
    public int MaxCount => maxCount;
    public int SpawnInterval => spawnInterval;
    public bool IsCentered => isCentered;
    public float DifficultyMultiplier => difficultyMultiplier;

    public bool CanSpawn()
    {
        spawnIntervalTicker++;

        if (spawnIntervalTicker >= spawnInterval)
        {
            return true;
        }

        return false;
    }

    public void ResetSpawn()
    {
        spawnIntervalTicker = 0;
    }
}
