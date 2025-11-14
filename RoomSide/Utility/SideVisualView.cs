using System;
using UnityEngine;

public class SideVisualView
{
    public static void UpdateRoomVisual(Room room)
    {
        if (room == null || room.GetRoomGameObject == null) return;

        Transform doorsParent = room.GetRoomGameObject.transform.Find("Doors");
        Transform wallsParent = room.GetRoomGameObject.transform.Find("Walls");

        if (doorsParent == null || wallsParent == null)
        {
            Debug.LogWarning(
                $"Room prefab is missing Doors or Walls parent for room {room.GetGridPosition}"
            );
            return;
        }

        SetWallsAndDoors(room, doorsParent, wallsParent);
    }
    private static void SetWallsAndDoors(Room room, Transform doorsParent, Transform wallsParent)
    {
        foreach (DirectionEnum dir in Enum.GetValues(typeof(DirectionEnum)))
        {
            if (dir == DirectionEnum.None) continue;

            Transform door = doorsParent.Find(dir.ToString() + "Door");
            Transform wall = wallsParent.Find(dir.ToString() + "Wall");

            if (door == null && wall == null) continue;

            if (room.GetOpenAreas.Contains(dir))
            {
                if (door != null) door.gameObject.SetActive(false);
                if (wall != null) wall.gameObject.SetActive(false);
                continue;
            }

            bool hasDoor = room.HasDoor(dir);

            if (door != null) door.gameObject.SetActive(hasDoor);
            if (wall != null) wall.gameObject.SetActive(!hasDoor);
        }
    }
}
