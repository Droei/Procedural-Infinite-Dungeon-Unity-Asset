using UnityEngine;
using UnityEngine.Events;

public class RoomEnterTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OnPlayerEntered?.Invoke();
        Debug.Log("Hey!");
    }
}
