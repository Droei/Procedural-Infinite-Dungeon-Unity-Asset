public static class RoomEnemyHandler
{
    public static void SpawnEnemies(Room room, EnemySpawnFactory enemyFactory, DungeonSettingsData dSD, EnemySpawnData enemySpawnData)
    {
        if (enemySpawnData != null)
            enemyFactory.SpawnSpecific(room, enemySpawnData);
        else
            enemyFactory.SpawnForWave(room);
    }
}
