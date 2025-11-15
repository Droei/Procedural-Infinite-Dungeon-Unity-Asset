using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonRoomMonoBehaviour : MonoBehaviour
{
    protected Room room;
    protected DoorView[] doors;
    protected RoomOpening[] roomOpening;
    DungeonSettingsData dSD;

    protected DirectionEnum sourceDirection;

    public virtual void Init(Room room, DungeonSettingsData dungeonSettingsData, RoomSpawnData roomSpawnData)
    {
        dSD = dungeonSettingsData;
        this.room = room;
        doors = GetComponentsInChildren<DoorView>();

        if (roomSpawnData.RoomLocks)
        {
            Debug.Log("Room with locks");
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


    public virtual void SpawnRoom(DirectionEnum direction)
    {
        dSD.GetDungeon.GetDungeonManager.SpawnRoom(direction, room);
    }

    public void CloseDoors()
    {
        if (room.GetParent == null)
            ApplyToRoomAndChildren(dv => dv.CloseDoor());
        else
            room.GetParent.GetRoomGameObject
                .GetComponent<DungeonRoomMonoBehaviour>()
                .ApplyToRoomAndChildren(dv => dv.CloseDoor());
    }

    public void OpenDoors()
    {
        if (room.GetParent == null)
            ApplyToRoomAndChildren(dv => dv.OpenDoor());
        else
            room.GetParent.GetRoomGameObject
                .GetComponent<DungeonRoomMonoBehaviour>()
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

    public virtual bool GetDebugMode => dSD.GetDungeon.GetDungeonManager.GetMaxDebugRoomsReached();
}
