using System.Collections.Generic;
using Game_Essentials.Event_Bus;
using JetBrains.Annotations;

public static class EventBus<TEvent>
{
    private static readonly List<EventListener<TEvent>> Listeners = new();
    
    public static void AddListener(EventListener<TEvent> listener)
    {
        Listeners.Add(listener);
    }

    public static void RemoveListener(EventListener<TEvent> listener)
    {
        Listeners.Remove(listener);
    }

    public static void Emit(object sender, TEvent e)
    {
        var listenersCount = Listeners.Count;
        for (var i = 0; i < listenersCount; i++)
        {
            var listener = Listeners[i];
            listener.Invoke(sender, e);
        }
    }
        
    public static int GetListenerCount()
    {
        return Listeners.Count;
    }
}