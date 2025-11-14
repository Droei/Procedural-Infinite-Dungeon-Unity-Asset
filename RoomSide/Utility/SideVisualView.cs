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
        foreach (DirectionEnum dir in System.Enum.GetValues(typeof(DirectionEnum)))
        {
            if (dir == DirectionEnum.None) continue;

            Transform door = doorsParent.Find(dir.ToString() + "Door");
            Transform wall = wallsParent.Find(dir.ToString() + "Wall");

            bool hasDoor = room.HasDoor(dir);

            door.gameObject.SetActive(hasDoor);
            wall.gameObject.SetActive(!hasDoor);
        }
    }
}
