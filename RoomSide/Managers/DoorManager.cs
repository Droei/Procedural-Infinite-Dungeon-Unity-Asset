using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public DirectionEnum direction;
    private Room parentRoom;
    private DungeonManager roomManager;

    [SerializeField] GameObject gate;

    private bool triggered = false;

    public void Init(Room room, DungeonManager manager)
    {
        parentRoom = room;
        roomManager = manager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
    }

    public void MarkTriggered()
    {
        triggered = true;
    }

    public void OpenDoor()
    {
        gate.SetActive(false);
    }
    public void CloseDoor()
    {
        gate.SetActive(true);
    }
}
