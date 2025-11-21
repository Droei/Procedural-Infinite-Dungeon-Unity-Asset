using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    protected Room room;
    protected DoorView[] doors;
    protected RoomOpening[] roomOpening;
    DungeonSettingsData dSD;

    protected DirectionEnum sourceDirection;

    public void Init(Room room, DungeonSettingsData dungeonSettingsData, RoomSpawnData roomSpawnData)
    {
        dSD = dungeonSettingsData;
        this.room = room;
        doors = GetComponentsInChildren<DoorView>();
        OpenDoors();
        if (roomSpawnData.RoomLocks)
        {
            RoomEnterTrigger trigger = GetComponentInChildren<RoomEnterTrigger>();
            if (trigger != null)
            {
                trigger.OnPlayerEntered.AddListener(() => CloseDoors());
            }
            else
            {
                Debug.LogWarning("No RoomEnterTrigger found in children!");
            }
        }
    }


    public void SpawnRoom(DirectionEnum direction)
    {
        dSD.Dungeon.GetDungeonManager.SpawnRoom(direction, room);
    }

    public void CloseDoors()
    {
        Room targetRoomData = room.GetParent ?? room;

        if (targetRoomData.Enemies.Count > 0)
        {
            DungeonRoom targetMono =
                targetRoomData.GetRoomGameObject.GetComponent<DungeonRoom>();

            targetMono.ApplyToRoomAndChildren(dv => dv.CloseDoor());
        }
    }



    public void OpenDoors()
    {
        if (room.GetParent == null)
            ApplyToRoomAndChildren(dv => dv.OpenDoor());
        else
            room.GetParent.GetRoomGameObject
                .GetComponent<DungeonRoom>()
                .ApplyToRoomAndChildren(dv => dv.OpenDoor());
    }

    private void ApplyToRoomAndChildren(Action<DoorView> doorAction)
    {
        DoorView[] doorViews = GetComponentsInChildren<DoorView>();
        foreach (DoorView dv in doorViews)
        {
            doorAction(dv);
        }

        List<Room> childRooms = room.GetChildRooms;
        foreach (Room childRoom in childRooms)
        {
            doorViews = childRoom.GetRoomGameObject.GetComponentsInChildren<DoorView>();
            foreach (DoorView dv in doorViews)
            {
                doorAction(dv);
            }
        }
    }

    public bool GetDebugMode => dSD.Dungeon.GetDungeonManager.GetMaxDebugRoomsReached();
}
