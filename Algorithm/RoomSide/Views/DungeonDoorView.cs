using UnityEngine;

public class DungeonDoorView : MonoBehaviour
{
    [SerializeField] DirectionEnum direction;
    [SerializeField] GameObject gate;

    private DungeonRoom roomView;
    private bool triggered = false;

    public void Init(bool triggered)
    {
        this.triggered = triggered;
    }

    private void Start()
    {
        roomView = GetComponentInParent<DungeonRoom>();

        if (roomView.GetDebugMode)
        {
            SpawnRoom();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SpawnRoom();
    }

    private void SpawnRoom()
    {
        if (triggered) return;
        triggered = true;

        roomView.SpawnRoom(direction);
    }

    public DirectionEnum GetDirection()
    {
        return direction;
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
