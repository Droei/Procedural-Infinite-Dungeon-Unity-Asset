
using UnityEngine;

public static class RoomCreationHandler
{

    public static Room CreateRoom(Vector2Int gridPos, float roomSize, RoomSpawnData spawnData, ref int roomCount)
    {
        Vector3 worldPos = new(gridPos.x * roomSize, 0, gridPos.y * roomSize);
        GameObject roomObject = Object.Instantiate(spawnData.RoomObject.gameObject, worldPos, Quaternion.identity);

        roomObject.name = $"Room ({roomCount++})";
        return new(gridPos.x, gridPos.y, roomObject);
    }
}
