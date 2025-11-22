using UnityEngine;

public class DungeonEnemy : MonoBehaviour
{
    protected Room room;
    public float difficultyIncrement = 1.0f;

    public void InitForDungeon(Room room, float difficultyIncrement)
    {
        this.room = room;
        this.difficultyIncrement = difficultyIncrement;
    }

    public void OnDestroy()
    {
        room.EnemyDied(gameObject);
    }
}
