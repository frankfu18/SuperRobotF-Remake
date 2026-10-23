using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider), typeof(EventTrigger))]
public class SceneButton : MonoBehaviour
{
    public System.Action<GameObject> onClick;

    private EventTrigger eventTrigger;

    private void Awake()
    {
        eventTrigger = this.GetComponent<EventTrigger>();

        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) =>
        {
            onClick?.Invoke(this.gameObject);
        });
        eventTrigger.triggers.Add(entry);
    }

    private void OnDestroy()
    {
        eventTrigger.triggers.Clear();
        eventTrigger = null;
        onClick = null;
    }
}