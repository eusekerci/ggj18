using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GGJEvent { }

public static class MessageBus
{
    public static void Publish<T>(T evnt) where T : GGJEvent
    {
        UniRx.MessageBroker.Default.Publish(evnt);
    }

    public static UniRx.IObservable<T> OnEvent<T>() where T : GGJEvent
    {
        return UniRx.MessageBroker.Default.Receive<T>();
    }
}