public static class RoomEnemyHandler
{
    public static void SpawnEnemies(Room room, IEnemySpawnFactory enemyFactory, float roomSize, int currentWave, RoomSpawnData spawnData)
    {
        if (spawnData.SpecificEnemy != null)
            enemyFactory.SpawnSpecific(room, roomSize, currentWave, spawnData.SpecificEnemy);
        else
            enemyFactory.SpawnForWave(room, roomSize, currentWave);
    }
}
