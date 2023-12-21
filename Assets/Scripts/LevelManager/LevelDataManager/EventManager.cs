using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;
    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!EventManager.eventManager)
            {
                EventManager.eventManager = (Instantiate(MyGeneric.Load<GameObject>(StringsData.EVENT_MANGER)) as GameObject).GetComponent<EventManager>();

                if (!EventManager.eventManager)
                {
                    UnityEngine.Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    EventManager.eventManager.Init();
                }
            }
            return EventManager.eventManager;
        }
    }

    private void Init()
    {
        if (this.eventDictionary == null)
        {
            this.eventDictionary = new Dictionary<string, UnityEvent>();
            DontDestroyOnLoad(gameObject);
            Debug.Log("Init Event Manager");
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent unityEvent = null;
        if (EventManager.instance.eventDictionary.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.AddListener(listener);
        }
        else
        {
            unityEvent = new UnityEvent();
            unityEvent.AddListener(listener);
            EventManager.instance.eventDictionary.Add(eventName, unityEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (EventManager.eventManager == null)
        {
            return;
        }
        UnityEvent unityEvent = null;
        if (EventManager.instance.eventDictionary.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent unityEvent = null;
        if (EventManager.instance.eventDictionary.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.Invoke();
        }
    }
}
