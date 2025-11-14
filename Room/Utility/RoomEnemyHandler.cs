public static class RoomEnemyHandler
{
    public static void SpawnEnemies(Room room, EnemySpawnFactory enemyFactory, DungeonSettingsData dSD)
    {
        //if (dSD.spawnData.SpecificEnemy != null)
        //    enemyFactory.SpawnSpecific(room, dSD);
        //else
        enemyFactory.SpawnForWave(room);
    }
}
