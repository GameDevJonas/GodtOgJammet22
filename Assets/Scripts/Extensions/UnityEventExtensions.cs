using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UnityEventExtensions
{
    /// <summary>
    /// Add a handler to the event which will only be called once.
    /// </summary>
    /// <param name="unityEvent">The event to subscribe the handler to.</param>
    /// <param name="oneTimeHandler">The handler that will handle the event.</param>
    public static void AddOneTimeListener(this UnityEvent unityEvent, UnityAction oneTimeHandler)
    {
        UnityAction oneTimeAction = null;
        oneTimeAction = () =>
        {
            if (oneTimeHandler != null)
            {
                oneTimeHandler.Invoke();
            }

            unityEvent.RemoveListener(oneTimeAction);
        };
        unityEvent.AddListener(oneTimeAction);
    }
}
