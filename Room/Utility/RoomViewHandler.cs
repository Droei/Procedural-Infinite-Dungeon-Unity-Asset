public static class RoomViewHandler
{
    public static void InitView(Room room, DungeonSettingsData dSD, RoomSpawnData roomSpawnData)
    {
        DungeonRoomMonoBehaviour view = room.GetRoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();
        view.Init(room, dSD, roomSpawnData);
    }
}
