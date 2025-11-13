using UnityEngine;

public class RoomVisualView
{
    public static void UpdateRoomVisual(Room room)
    {
        if (room == null || room.GetRoomGameObject == null) return;

        Transform doorsParent = room.GetRoomGameObject.transform.Find("Doors");
        Transform wallsParent = room.GetRoomGameObject.transform.Find("Walls");

        if (doorsParent == null || wallsParent == null)
        {
            Debug.LogWarning($"Room prefab is missing Doors or Walls parent for room {room.GetGridPosition}");
            return;
        }

        SetWallsAndDoors(room, doorsParent, wallsParent);
    }

    private static void SetWallsAndDoors(Room room, Transform doorsParent, Transform wallsParent)
    {
        foreach (DirectionEnum dir in System.Enum.GetValues(typeof(DirectionEnum)))
        {
            if (dir == DirectionEnum.None) continue;

            Transform door = doorsParent.Find(dir.ToString() + "Door");
            Transform wall = wallsParent.Find(dir.ToString() + "Wall");

            bool hasOpening = room.HasRoomOpening(dir);
            bool hasDoor = room.HasDoor(dir);

            //Debug.Log($"Direction: {dir}, HasOpening: {hasOpening}, HasDoor: {hasDoor}");

            if (door != null) door.gameObject.SetActive(hasDoor && !hasOpening);
            if (wall != null) wall.gameObject.SetActive(!hasDoor && !hasOpening);
        }
    }
}
